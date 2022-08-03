using FlickrImageFinder.Models;

namespace FlickrImageFinder.Services
{
    //Make it singleton also make an iterface for flicer and twitter api

    public static class FlickerApi
    {
        private const string endPointUrl = @"https://api.flickr.com/services/feeds/photos_public.gne?format=json&tags=";

        public static SearchResultModel Responses;
        public static JsonReader Reader;

        public static void LoadApi(string url)
        {
            if(Reader == null)
            {
                Reader = new JsonReader();
            }
            Responses = Reader.FindResult(endPointUrl + url);
        }

    }
}
