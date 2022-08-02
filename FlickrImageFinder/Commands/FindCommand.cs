using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrImageFinder.Commands
{
    public class FindCommand:CommandBase
    {
        public delegate void ListUpdatedFn(List<string> imgUrl);
        public ListUpdatedFn OnListUpdated;

        public delegate void ClearListFn();
        public ClearListFn clearListFn;

        private Func<string> searchTextFn;

        public FindCommand(Func<string> searchStrFn)
        {
            searchTextFn = searchStrFn;
        }    

        public override void Execute(object parameter)
        {
            clearListFn.Invoke();

            var queryString = searchTextFn.Invoke();
            if (queryString != null)
            {

            }
            FlickerApi api = new FlickerApi(queryString);
            SearchResultModel result = api.responses;
            List<string> imgUrl = new List<string>();

            foreach(var i in result.items)
            {
                imgUrl.Add(i.media.m);
            }
            
            OnListUpdated.Invoke(imgUrl);
        }
    }
}
