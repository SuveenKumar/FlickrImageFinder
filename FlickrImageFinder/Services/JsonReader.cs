using FlickrImageFinder.Models;
using System.Net;
using System.Text.Json;

namespace FlickrImageFinder.Services
{
    //Generic Json Reader class
    public class JsonReader<T>
    {
        public WebClient webclient;

        public JsonReader()
        {
            webclient = new WebClient();
        }
        
        public T FindResult(string url)
        {
            var jsonstr = Read(url);
            var json = jsonstr.Substring(15);
            var res = json.Remove(json.Length - 1, 1);
            T info = JsonSerializer.Deserialize<T>(res);
            return info;
        }

        private string Read(string url)
        {
            string s= webclient.DownloadString(url);
            return s;
        }

    }
}
