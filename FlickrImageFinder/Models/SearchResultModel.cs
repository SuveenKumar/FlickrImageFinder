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
        public string title { get; set; }
        public Items[] items { get; set; }
    }

    public class Items
    {
        public Link media { get; set; }
    }

    public class Link
    {
        public string m { get; set; }
    }
}
