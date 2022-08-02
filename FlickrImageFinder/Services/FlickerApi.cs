using FlickrImageFinder.Models;

namespace FlickrImageFinder.Services
{
    //Make it singleton also make an iterface for flicer and twitter api

    public class FlickerApi
    {
        public SearchResultModel responses;
        JsonReader reader;

        public FlickerApi(string url)
        {
            reader = new JsonReader();
            responses = reader.FindResult(url);
        }

    }
}
