using System.IO;
using System.Net;

namespace Discord.CoD.Blops.Api
{
    public class RestClient
    {
        private string _url { get; set; }

        public RestClient(string url)
        {
            _url = url;
        }

        public T GetData<T>() where T : new()
        {
            string res;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                res = reader.ReadToEnd();
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(res);
        }
    }
}
