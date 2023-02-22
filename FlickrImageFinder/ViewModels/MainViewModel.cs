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
        public List<string> SearchHistory { get; set; }
        public bool BackButtonEnabled { get; set; } 
        public ICommand FindButtonCommand { get; }  //Bind to Find button
        public ICommand BackButtonCommand { get; }  //Bind to Find button
        public string SearchStr { get; set; } //Bind to Search box text.
        public MainViewModel()
        {
            BackButtonEnabled = false;
            SearchStr = Constants.PlaceHolderText;
            SearchHistory=new List<string>();
            FindButtonCommand = new ButtonCommand(ExecuteFindCommand);
            BackButtonCommand = new ButtonCommand(ExecuteBackCommand);
        }

        private void ExecuteFindCommand(object obj)
        {
            //Return when query string is null
            if (SearchStr == null || SearchStr== Constants.PlaceHolderText)
            {
                return;
            }   
            SearchHistory.Add(SearchStr);
            StartSearchProcess();
            BackButtonEnabled = true;
            OnPropertyChanged(nameof(BackButtonEnabled));
        }

        private void StartSearchProcess()
        {
            FlickerApi.LoadApi(SearchStr);
            SearchResultModel result = FlickerApi.Responses;
            var imgUrl = new List<ImageModel>();

            foreach (var i in result.items)
            {
                imgUrl.Add(new ImageModel() { Img = i.media.m });
            }
            DisplayImageResults(imgUrl);
        }

        private void ExecuteBackCommand(object obj)
        {
            if (SearchHistory != null && SearchHistory.Count == 1)
            {
                CurrentViewModel = null;
                SearchStr = "";
                SearchHistory.Clear();
                BackButtonEnabled = false;
                OnPropertyChanged(nameof(BackButtonEnabled));
                OnPropertyChanged(nameof(SearchStr));
                OnPropertyChanged(nameof(CurrentViewModel));
            }

            if (SearchHistory!=null && SearchHistory.Count> 1)
            {
                if (CurrentViewModel.GetType() == typeof(ImageListPageViewModel))
                {
                        SearchHistory.RemoveAt(SearchHistory.Count - 1);
                }
                SearchStr = SearchHistory[SearchHistory.Count - 1];
                OnPropertyChanged(nameof(BackButtonEnabled));
                OnPropertyChanged(nameof(SearchStr));
                StartSearchProcess();
            }
        }
        private void DisplayImageResults(List<ImageModel> list)
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
