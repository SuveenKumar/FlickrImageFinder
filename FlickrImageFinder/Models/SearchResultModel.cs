using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlickrImageFinder.Models
{
    public class SearchResultModel
    {
        public Photos photos { get; set; }
    }

    public class Photos
    {
        public Photo[] photo { get; set; }
    }

    public class Photo
    {
        public string id { get; set; }
        public string server { get; set; }
        public string secret { get; set; }
        public int farm { get; set; }
        public string title { get; set; }

    }
}
