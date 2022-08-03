using FlickrImageFinder.Commands;
using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public FindCommand FindButtonCommand { get; }  //Bind to Find button

        public ObservableCollection<ImageModel> ImageList { get; set; }  //Bind to Image Panel Items source.

        public string SearchStr { get; set; } //Bind to Search box text.

        public Func<string> searchTextFn;  //fn pointer for getting image urls list from nodel

        public Visibility IsPopupVisible { get; set; }

        public MainViewModel()
        {
            IsPopupVisible = Visibility.Hidden;
            SearchStr = "Search Image";

            searchTextFn += () => { return SearchStr; };

            FindButtonCommand = new FindCommand(searchTextFn);

            FindButtonCommand.clearListFn += () => { ImageList.Clear(); };

            ImageList = new ObservableCollection<ImageModel>();

            //Invoked when list is updated.
            FindButtonCommand.OnListUpdated += (list)=>
            {
                foreach(var i in list)
                {
                    ImageList.Add(new ImageModel() { Img = i });

                }
                if (list.Count == 0)
                {
                    IsPopupVisible = Visibility.Visible;
                }
                else
                {
                    IsPopupVisible = Visibility.Hidden;
                }
                OnPropertyChanged(nameof(IsPopupVisible));

                OnPropertyChanged(nameof(ImageList));
            };
        }
    }
}
