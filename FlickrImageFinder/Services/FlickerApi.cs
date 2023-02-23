using FlickrImageFinder.Models;
using System;
using System.Collections.ObjectModel;
using static System.Net.WebRequestMethods;

namespace FlickrImageFinder.Services
{
    //Make it singleton also make an iterface for flicer and twitter api

    public static class FlickerApi
    {
        private const string endPointUrl = @"https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=cc5e9c54ed8a260850fa4e9c5725eb01&format=json&nojsoncallback=1&tags=";

        public static SearchResultModel Responses;
        public static ObservableCollection<ImageModel> Images;
        public static JsonReader<SearchResultModel> Reader;

        public static void LoadApi(string searchText)
        {
            //Initialize reader if not initialized. 
            if(Reader == null)
            {
                Reader = new JsonReader<SearchResultModel>();
            }

            string queryString = endPointUrl + searchText;

            //Update responses
            Responses = Reader.FindResult(queryString);

            FillImages(Responses);

            Console.WriteLine("Response");
        }

        public static void FillImages(SearchResultModel Responses)
        {
            Images= new ObservableCollection<ImageModel>();
            //Initialize reader if not initialized. 
            int i = 0;
            foreach (var image in Responses.photos.photo)
            {
                ImageModel imageModel = new ImageModel()
                {
                    Title = Responses.photos.photo[i].title,
                    Img = GetImageLink(image.id, image.farm, image.server, image.secret)
                };
                Images.Add(imageModel);
                i++;
            }
        }

        private static string GetImageLink(string id,int farm, string server, string secret)
        {
            string link= "https://farm"+farm.ToString()+".staticflickr.com/"+server+"/"+id+"_"+secret+".jpg";
            return link;
        }
    }
}
