using FlickrImageFinder.Commands;
using FlickrImageFinder.Common;
using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }
        public ICommand FindButtonCommand { get; }  //Bind to Find button
        public string SearchStr { get; set; } //Bind to Search box text.

        public MainViewModel()
        {
            SearchStr = Constants.PlaceHolderText;

            FindButtonCommand = new ButtonCommand(ExecuteFindCommand);
        }

        private void ExecuteFindCommand(object obj)
        {
            //Return when query string is null
            if (SearchStr == null || SearchStr== Constants.PlaceHolderText)
            {
                return;
            }
            FlickerApi.LoadApi(SearchStr);
            SearchResultModel result = FlickerApi.Responses;
            var imgUrl = new ObservableCollection<ImageModel>();

            foreach (var i in result.items)
            {
                imgUrl.Add(new ImageModel() { Img = i.media.m });
            }
            DisplayImageResults(imgUrl);
        }
        private void DisplayImageResults(ObservableCollection<ImageModel> list)
        {
            CurrentViewModel = new ImageListPageViewModel();
            var imageListVM = CurrentViewModel as ImageListPageViewModel;
            imageListVM.GetSelectedImage += DisplaySelectedImage;
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
