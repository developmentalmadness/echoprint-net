using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using echoprint_net.Data;
using Newtonsoft.Json;

namespace echoprint_net
{
    public class NCodegen : IDisposable
    {
        private Process codegen = new Process();
        private StreamWriter input = null;
        private Action<Code> callback = null;
        private long jobCount = 0;

        public NCodegen(string workingDir, string codegenexe, int start, int end)
        {
            codegen.StartInfo.WorkingDirectory = workingDir;
            codegen.StartInfo.Arguments = String.Format("-s {0} {1} ", start, end);
            codegen.StartInfo.CreateNoWindow = true;
            codegen.StartInfo.FileName = workingDir + @"\" + codegenexe;
            codegen.StartInfo.RedirectStandardError = true;
            codegen.StartInfo.RedirectStandardOutput = true;
            codegen.StartInfo.RedirectStandardInput = true;
            codegen.StartInfo.UseShellExecute = false;

            codegen.OutputDataReceived += OutputDataReceived;
            codegen.ErrorDataReceived += ErrorDataReceived;
        }

        public void Start(Action<Code> handleResult)
        {
            if (handleResult == null)
                throw new ArgumentNullException("handleResult");

            callback = handleResult;

            codegen.Start();

            codegen.BeginOutputReadLine();
            codegen.BeginErrorReadLine();

            input = codegen.StandardInput;
        }

        public void AddFile(string path)
        {
            AddFile(new FileInfo(path));
        }

        string[] supportedExtensions = { ".mp3", ".m4a", ".mp4", ".aif", ".aiff", ".flac", ".au", ".wav", ".aac", ".flv", "" };

        public void AddFile(FileInfo file)
        {
            try
            {
                if (!file.Exists)
                    throw new FileNotFoundException("File not found", file.Name);
                else if (!supportedExtensions.Any(ext => ext == file.Extension))
                    return;

                Interlocked.Increment(ref jobCount);
                input.WriteLine(file.FullName);
            }
            catch
            {
                SignalJobCompleted();
            }
        }

        private void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(e.Data))
                    return;

                throw new IOException(String.Format("A non-fatal exception occurred during process execution: \"{0}\"", e.Data));
            }
            finally
            {
                SignalJobCompleted();
            }
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data == null || "[]".IndexOf(e.Data) != -1)
                    return;

                Code data = null;
                if (e.Data.EndsWith(","))
                    data = JsonConvert.DeserializeObject<Code>(e.Data.Substring(0, e.Data.Length - 1));
                else
                    data = JsonConvert.DeserializeObject<Code>(e.Data);

                if (data != null)
                {
                    callback.BeginInvoke(data, (result) =>
                    {
                        callback.EndInvoke(result);
                        SignalJobCompleted();
                    }, null);
                }
            }
            catch
            {
                SignalJobCompleted();
            }
        }

        ManualResetEventSlim allCompleted = new ManualResetEventSlim(false);
        private void SignalJobCompleted()
        {
            var remaining = Interlocked.Decrement(ref jobCount);
            
            if (disposed && remaining <= 0)
                allCompleted.Set();
        }

        public void WaitForAll()
        {

        }

        bool disposed = false;
        public void Dispose()
        {
            if (disposed)
                throw new ObjectDisposedException("NCodegen");

            disposed = true;
            input.Close();
            codegen.WaitForExit(); 
            codegen.Dispose();

            allCompleted.Wait();
        }
    }
}
