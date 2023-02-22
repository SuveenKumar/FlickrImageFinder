using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrImageFinder.Commands
{
    public class FindCommand:CommandBase
    {
        public Action<ObservableCollection<ImageModel>> GetList;

        private Func<string> searchTextFn;

        public FindCommand(Func<string> searchStrFn)
        {
            searchTextFn = searchStrFn;
        }    

        public override void Execute(object parameter)
        {
            var queryString = searchTextFn.Invoke();

            //Return when query string is null
            if (queryString == null)
            {
                return;
            }
            FlickerApi.LoadApi(queryString);
            SearchResultModel result = FlickerApi.Responses;
            var imgUrl = new ObservableCollection<ImageModel>();

            foreach(var i in result.items)
            {
                imgUrl.Add(new ImageModel() { Img=i.media.m }) ;
            }
            
            //Invoke when models are updated.
            GetList.Invoke(imgUrl);
        }
    }
}
