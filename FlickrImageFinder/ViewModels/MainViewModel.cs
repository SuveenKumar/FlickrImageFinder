using FlickrImageFinder.Commands;
using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public FindCommand FindButtonCommand { get; }

        public ObservableCollection<ImageModel> ImageList { get; set; }

        public string SearchStr { get; set; }

        public Func<string> searchTextFn;

        public MainViewModel()
        {
            SearchStr = "Search Image";

            searchTextFn += () => { return SearchStr; };

            FindButtonCommand = new FindCommand(searchTextFn);

            FindButtonCommand.clearListFn += () => { ImageList.Clear(); };

            ImageList = new ObservableCollection<ImageModel>();

            FindButtonCommand.OnListUpdated += (list)=>
            {
                foreach(var i in list)
                {
                    ImageList.Add(new ImageModel() { Img = i });

                }
                OnPropertyChanged(nameof(ImageList));
            };
        }
    }
}
