using FlickrImageFinder.Models;
using System.Net;
using System.Text.Json;

namespace FlickrImageFinder.Services
{
    public class JsonReader
    {
        public WebClient webclient;

        public JsonReader()
        {
            webclient = new WebClient();
        }
        
        public SearchResultModel FindResult(string url)
        {
            var jsonstr = Read(url);
            var json = jsonstr.Substring(15);
            var res = json.Remove(json.Length - 1, 1);
            SearchResultModel info = JsonSerializer.Deserialize<SearchResultModel>(res);
            return info;
        }

        private string Read(string url)
        {
            string s;
            using (WebClient client = new WebClient())
            {
                s = client.DownloadString(url);
            }
            return s;
        }

    }
}
