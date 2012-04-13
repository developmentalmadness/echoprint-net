using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using echoprint_net.Data;
using Newtonsoft.Json;

namespace echoprint_net
{
    public class NCodegen : IDisposable
    {
        private Process codegen = new Process();
        private StreamWriter input = null;
        private Action<Code> callback = null;

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
            if (!file.Exists)
                throw new FileNotFoundException("File not found", file.Name);
            else if (!supportedExtensions.Any(ext => ext == file.Extension))
                return;
            input.WriteLine(file.FullName);
        }

        private static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.Data))
                return;

            throw new IOException(String.Format("A non-fatal exception occurred during process execution: \"{0}\"", e.Data));
        }

        private void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || "[]".IndexOf(e.Data) != -1)
                return;

            Code data = null;
            if (e.Data.EndsWith(","))
                data = JsonConvert.DeserializeObject<Code>(e.Data.Substring(0, e.Data.Length - 1));
            else
                data = JsonConvert.DeserializeObject<Code>(e.Data);

            if (data != null)
                callback.BeginInvoke(data, (result) => callback.EndInvoke(result), null);
        }

        bool disposed = false;
        public void Dispose()
        {
            if (disposed)
                throw new ObjectDisposedException("NCodegen");
            
            input.Close();
            codegen.WaitForExit();
            codegen.Dispose();
        }
    }
}
