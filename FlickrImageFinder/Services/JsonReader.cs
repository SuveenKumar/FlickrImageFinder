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
            T info = JsonSerializer.Deserialize<T>(jsonstr);
            return info;
        }

        private string Read(string url)
        {
            string s= webclient.DownloadString(url);
            return s;
        }

    }
}
