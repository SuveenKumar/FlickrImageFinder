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
        public ViewModelBase CurrentViewModel // Bind to Frame's content 
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
        public bool BackButtonEnabled // Bind to back button isEnabled
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
        public string SearchStr //Bind to Search box text.
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
        }

        private List<string> SearchHistory { get; set; } // For restriving earlier search results
        public ICommand FindButtonCommand { get; }  //Bind to Find button
        public ICommand BackButtonCommand { get; }  //Bind to Back button
        public MainViewModel()
        {
            BackButtonEnabled = false;
            SearchHistory=new List<string>();
            FindButtonCommand = new ButtonCommand(ExecuteFindCommand);
            BackButtonCommand = new ButtonCommand(ExecuteBackCommand);
        }

        // Handling Find button press
        private void ExecuteFindCommand(object obj)
        {
            //Return when query string is null
            SearchHistory.Add(SearchStr);
            StartSearchProcess();
            BackButtonEnabled = true;
        }

        //Start search process with searched string
        private void StartSearchProcess()
        {
            FlickerApi.LoadApi(SearchStr);
            var result = FlickerApi.Images;
            DisplayImageResults(result);
        }

        // Handling Back button press
        private void ExecuteBackCommand(object obj)
        {
            //If current page is image List page then go back to previous search result
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
                //If current page is select page List then show current results in image list page
                SearchStr = SearchHistory[SearchHistory.Count - 1];
                StartSearchProcess();
            }
        }

        //Display list of image results
        private void DisplayImageResults(ObservableCollection<ImageModel> list)
        {
            CurrentViewModel = new ImageListPageViewModel();
            var imageListVM = CurrentViewModel as ImageListPageViewModel;
            imageListVM.UpdateImageList(list, DisplaySelectedImage);
        }

        //Display selected image
        private void DisplaySelectedImage(ImageModel image)
        {
            CurrentViewModel = new ImageSelectPageViewModel();
            var imageSelectVM = CurrentViewModel as ImageSelectPageViewModel;
            imageSelectVM.UpdateSelectedImage(image);
        }
    }
}
