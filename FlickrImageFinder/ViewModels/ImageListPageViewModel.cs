using FlickrImageFinder.Commands;
using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class ImageListPageViewModel : ViewModelBase
    {
        public ObservableCollection<ImageModel> ImageList { get; set; }  //Bind to Image List Page's Items source.

        private bool _isNoResultVisible; // Bind to No result textbox visibility
        public bool IsNoResultVisible
        {
            get
            {
                return _isNoResultVisible;
            }
            set
            {
                _isNoResultVisible = value;
                OnPropertyChanged(nameof(IsNoResultVisible));
            }
        }

        public ICommand SelectImageCommand { get; set; } //Command for selecting image

        private Action<ImageModel> GetSelectedImage; // Handler for notifying and getting selected image

        public ImageListPageViewModel()
        {
            //Initializing Defaults
            IsNoResultVisible = false;
            SelectImageCommand = new ButtonCommand(ExecuteSelectCommand);
        }

        // Function for updating image list and registering handler
        public void UpdateImageList(ObservableCollection<ImageModel> imageList,Action<ImageModel> displaySelectedImageFn)
        {
            GetSelectedImage = displaySelectedImageFn;
            ImageList = imageList;
            if (ImageList != null)
            {
                if (ImageList.Count == 0)
                {
                    IsNoResultVisible = true;
                }
                else
                {
                    IsNoResultVisible = false;
                }
            }
        }

        // Function pointed to 
        private void ExecuteSelectCommand(object parameter)
        {
            var curImg = new ImageModel() { Img = (string)parameter };
            GetSelectedImage.Invoke(curImg);
        }
    }
    
}