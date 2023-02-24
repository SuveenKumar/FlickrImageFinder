namespace FlickrImageFinder.Models
{
    //Main Response model
    public class SearchResultModel
    {
        public Photos photos { get; set; }
    }

    //Sub model for SearchResultModel
    public class Photos
    {
        public Photo[] photo { get; set; }
    }

    //Sub model for Photos model
    public class Photo
    {
        public string id { get; set; }
        public string server { get; set; }
        public string secret { get; set; }
        public int farm { get; set; }
        public string title { get; set; }
    }
}
