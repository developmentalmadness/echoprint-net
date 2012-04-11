using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using echoprint_net.Data;
using Newtonsoft.Json.Linq;

namespace echoprint_net
{
    public enum EchonestResult
    {
        None,
        Error,
        NotFound,
        Success
    }

    class EchonestAPI
    {
        public EchonestResult IdentifySong(Code item, out String errorMessage)
        {
            EchonestResult result = EchonestResult.None;
            errorMessage = String.Empty;

            while (result == EchonestResult.None)
            {
                var api_key = ConfigurationManager.AppSettings["echonest_api_key"];
                var url = String.Format("http://developer.echonest.com/api/v4/song/identify?api_key={0}", api_key);

                var request = WebRequest.Create(url) as HttpWebRequest;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";

                JObject query = JObject.FromObject(new
                {
                    code = item.code,
                    metadata = new
                    {
                        version = decimal.Parse(item.metadata.version)
                    }
                });

                StringBuilder body = new StringBuilder();
                body.AppendFormat("version=3.15&query={0}", query);

                var buffer = Encoding.UTF8.GetBytes(body.ToString());

                try
                {
                    var output = String.Empty;
                    using (var input = request.GetRequestStream())
                    {
                        input.Write(buffer, 0, buffer.Length);

                        var response = request.GetResponse() as HttpWebResponse;
                        try
                        {
                            using (var outstream = response.GetResponseStream())
                            using (StreamReader reader = new StreamReader(outstream))
                                output = reader.ReadToEnd();
                        }
                        finally
                        {
                            if (response != null)
                                response.Close();
                        }
                    }

                    var j = JObject.Parse(output);

                    if (j["response"]["status"]["code"].Value<int>() == 0
                        && j["response"]["songs"].Count() == 1)
                    {
                        var song = j["response"]["songs"][0];
                        item.metadata.id = song["id"].Value<string>().Trim();
                        item.metadata.artist_id = song["artist_id"].Value<string>().Trim();
                        item.metadata.title = song["title"].Value<string>().Trim();
                        item.metadata.artist = song["artist_name"].Value<string>().Trim();
                        result = EchonestResult.Success;
                    }
                    else
                        result = EchonestResult.NotFound;

                }
                catch (WebException ex)
                {
                    if (ex.Response != null && ex.Response.Headers["X-RateLimit-Remaining"] == "0")
                    {
                        // slow things down and retry
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        result = EchonestResult.Error;
                        errorMessage = ex.Message;
                    }
                }
                catch (Exception ex)
                {
                    result = EchonestResult.Error;
                    errorMessage = ex.Message;
                }
            }

            return result;
        }
    }
}
