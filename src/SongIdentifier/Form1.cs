using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using echoprint_net;
using echoprint_net.Data;
using echoprintcli;
using Microsoft.Xna.Framework.Audio;
using Newtonsoft.Json;

namespace SongIdentifier
{
    public partial class Form1 : Form
    {
        Thread recording;
        XnaAsyncDispatcher xnadispatcher = new XnaAsyncDispatcher();

        public Form1()
        {
            InitializeComponent();
            xnadispatcher.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            ToggleControls(true);
            txtLog.Text = String.Empty;

            var mic = Microphone.Default;
            if (mic == null)
            {
                txtLog.Text += "No mic detected.";
                ToggleControls(false);
                return;
            }

            //DispatcherTimer 
            // see http://msdn.microsoft.com/library/ff842408.aspx

            ThreadStart start = new ThreadStart(Recording);
            recording = new Thread(start);
            recording.Start();
        }

        private void Recording()
        {
            var mic = Microphone.Default;
            Log(String.Format("Using '{0}' as audio input...", mic.Name));
            var buffer = new byte[mic.GetSampleSizeInBytes(TimeSpan.FromSeconds(22))];
            
            int bytesRead = 0;
            string fileName = String.Empty;

            try
            {
                mic.Start();
                try
                {
                    Log(String.Format("{0:HH:mm:ss} Start recording audio stream...", DateTime.Now));
                    while (bytesRead < buffer.Length)
                    {
                        Thread.Sleep(1000);
                        var bytes = mic.GetData(buffer, bytesRead, (buffer.Length - bytesRead));
                        Log(String.Format("{0:HH:mm:ss} Saving {1} bytes to stream...", DateTime.Now, bytes));
                        bytesRead += bytes;
                    }
                    Log(String.Format("{0:HH:mm:ss} Finished recording audio stream...", DateTime.Now));
                }
                finally
                {
                    mic.Stop();
                }

                Func<byte, float> convert = (b) => System.Convert.ToSingle(b);
                var converter = new Converter<byte, float>(convert);
                float[] pcm = Array.ConvertAll<byte, float>(buffer, converter);

                Log(String.Format("{0:HH:mm:ss} Generating audio fingerprint...", DateTime.Now));
                var codeg = new CodegenCLI();
                String code = codeg.getCodeString(pcm, (uint)pcm.Length, 0);
                
                //CONVERT TO JSON
                var data = JsonConvert.DeserializeObject<Code>(code);
                if (!String.IsNullOrEmpty(data.error))
                {
                    Log(String.Format("{0:HH:mm:ss} - {1}", DateTime.Now, data.error));
                    return;
                }

                var api = new EchonestAPI();
                string error;
                Log(String.Format("{0:HH:mm:ss} Attempting to identify audio stream...", DateTime.Now));
                var result = api.IdentifySong(data, out error);
                switch (result)
                {
                    case EchonestResult.Success:
                        if (!String.IsNullOrEmpty(data.metadata.id))
                            Log(String.Format("{0:HH:mm:ss} - {1} {2}", DateTime.Now, data.metadata.id, data.metadata.title));
                        else
                            Log(String.Format("{0:HH:mm:ss} - NOT FOUND", DateTime.Now));
                        break;
                    case EchonestResult.NotFound:
                        Log(String.Format("{0:HH:mm:ss} - NOT FOUND", DateTime.Now));
                        break;
                    case EchonestResult.Error:
                        Log(String.Format("{0:HH:mm:ss} - ERROR: {1}", DateTime.Now, error));
                        break;
                }
            }
            catch (Exception ex)
            {
                Log(String.Format("{0:HH:mm:ss} - EXCEPTION: {1}", DateTime.Now, ex.Message));
            }
            finally
            {
                //xnadispatcher.Stop();
                ToggleControls(false);
            }
        }

        private void Log(string message)
        {
            txtLog.Invoke(new MethodInvoker(() => { 
                txtLog.Text += message + "\r\n";
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
                txtLog.Refresh();
            }));
        }

        private void ToggleControls(bool recording)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                btnIdentify.Enabled = !recording;
                btnCancel.Enabled = recording;
            }));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            recording.Abort();
        }
    }
}
