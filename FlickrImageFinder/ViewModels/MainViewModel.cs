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

        private List<string> SearchHistory { get; set; }
        public ICommand FindButtonCommand { get; }  //Bind to Find button
        public ICommand BackButtonCommand { get; }  //Bind to Find button
        public MainViewModel()
        {
            BackButtonEnabled = false;
            SearchHistory=new List<string>();
            FindButtonCommand = new ButtonCommand(ExecuteFindCommand);
            BackButtonCommand = new ButtonCommand(ExecuteBackCommand);
        }

        private void ExecuteFindCommand(object obj)
        {
            //Return when query string is null
            SearchHistory.Add(SearchStr);
            StartSearchProcess();
            BackButtonEnabled = true;
        }

        private void StartSearchProcess()
        {
            FlickerApi.LoadApi(SearchStr);
            var result = FlickerApi.Images;
            var imgUrl = new ObservableCollection<ImageModel>();

            foreach (var i in result)
            {
                imgUrl.Add(new ImageModel() 
                {
                    Img=i.Img,
                    Title=i.Title
                });
            }
            DisplayImageResults(imgUrl);
        }

        private void ExecuteBackCommand(object obj)
        {
            if (CurrentViewModel.GetType() == typeof(ImageListPageViewModel))
            {
                if(SearchHistory.Count == 1) 
                {
                    CurrentViewModel = null;
                    SearchStr = "";
                    SearchHistory.Clear();
                    BackButtonEnabled = false;
                }
                else if(SearchHistory.Count > 1)
                {
                    SearchHistory.RemoveAt(SearchHistory.Count - 1);
                    SearchStr = SearchHistory[SearchHistory.Count - 1];
                    StartSearchProcess();
                }
            }
            else
            {
                SearchStr = SearchHistory[SearchHistory.Count - 1];
                StartSearchProcess();
            }
        }
        private void DisplayImageResults(ObservableCollection<ImageModel> list)
        {
            CurrentViewModel = new ImageListPageViewModel();
            var imageListVM = CurrentViewModel as ImageListPageViewModel;
            imageListVM.UpdateImageList(list, DisplaySelectedImage);
        }
        private void DisplaySelectedImage(ImageModel image)
        {
            CurrentViewModel = new ImageSelectPageViewModel();
            var imageSelectVM = CurrentViewModel as ImageSelectPageViewModel;
            imageSelectVM.UpdateSelectedImage(image);
        }
    }
}
