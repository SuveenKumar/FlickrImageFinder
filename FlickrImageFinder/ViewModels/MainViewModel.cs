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
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }
        public FindCommand FindButtonCommand { get; }  //Bind to Find button

        public string SearchStr { get; set; } //Bind to Search box text.

        public Func<string> searchTextFn;  //fn pointer for getting image urls list from nodel

        public MainViewModel()
        {
            SearchStr = "Search Image";

            searchTextFn += () => { return SearchStr; };

            FindButtonCommand = new FindCommand(searchTextFn);

            FindButtonCommand.GetList += (s) => 
            {
                DisplayImageResults(s);
            };
        }
        private void DisplayImageResults(ObservableCollection<ImageModel> list)
        {
            CurrentViewModel = new ImageListPageViewModel();
            var imageListVM = CurrentViewModel as ImageListPageViewModel;
            imageListVM.RegisterSelectedImageHandler(DisplaySelectedImage);
            imageListVM.UpdateImageList(list);
            OnPropertyChanged(nameof(CurrentViewModel));
        }
        private void DisplaySelectedImage(ImageModel image)
        {
            CurrentViewModel = new ImageSelectPageViewModel();
            var imageSelectVM = CurrentViewModel as ImageSelectPageViewModel;
            imageSelectVM.UpdateSelectedImage(image);
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
