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
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        private bool _backButtonEnabled;
        public bool BackButtonEnabled 
        {
            get 
            {
                return _backButtonEnabled; 
            } 
            set 
            {
                _backButtonEnabled = value;
                OnPropertyChanged(nameof(BackButtonEnabled));
            }
        }

        private string _searchStr;
        public string SearchStr 
        { 
            get 
            {
                return _searchStr;
            }
            set 
            {
                _searchStr = value; 
                OnPropertyChanged(nameof(SearchStr));
            }
        } //Bind to Search box text.

        public List<string> SearchHistory { get; set; }
        public ICommand FindButtonCommand { get; }  //Bind to Find button
        public ICommand BackButtonCommand { get; }  //Bind to Find button
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
        }

        private void StartSearchProcess()
        {
            FlickerApi.LoadApi(SearchStr);
            SearchResultModel result = FlickerApi.Responses;
            var imgUrl = new ObservableCollection<ImageModel>();

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
            }

            if (SearchHistory!=null && SearchHistory.Count> 1)
            {
                if (CurrentViewModel.GetType() == typeof(ImageListPageViewModel))
                {
                        SearchHistory.RemoveAt(SearchHistory.Count - 1);
                }
                SearchStr = SearchHistory[SearchHistory.Count - 1];
                StartSearchProcess();
            }
        }
        private void DisplayImageResults(ObservableCollection<ImageModel> list)
        {
            CurrentViewModel = new ImageListPageViewModel();
            var imageListVM = CurrentViewModel as ImageListPageViewModel;
            imageListVM.GetSelectedImage += DisplaySelectedImage;
            imageListVM.UpdateImageList(list);
        }
        private void DisplaySelectedImage(ImageModel image)
        {
            CurrentViewModel = new ImageSelectPageViewModel();
            var imageSelectVM = CurrentViewModel as ImageSelectPageViewModel;
            imageSelectVM.UpdateSelectedImage(image);
        }
    }
}
