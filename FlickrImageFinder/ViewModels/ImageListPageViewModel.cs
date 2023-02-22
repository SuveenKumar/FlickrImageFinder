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
        public ObservableCollection<ImageModel> ImageList { get; set; }  //Bind to Image Panel Items source.

        private bool _isNoResultVisible;
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

        public ICommand SelectImageCommand { get; set; }

        public Action<ImageModel> GetSelectedImage;
        public ImageListPageViewModel()
        {
            _isNoResultVisible = false;
            SelectImageCommand = new ButtonCommand(ExecuteSelectCommand);
        }

        public void UpdateImageList(ObservableCollection<ImageModel> imageList)
        {
            ImageList = imageList;
            if (ImageList != null)
            {
                if (ImageList.Count == 0)
                {
                    _isNoResultVisible = true;
                }
                else
                {
                    _isNoResultVisible = false;
                }
            }
        }
        public void ExecuteSelectCommand(object parameter)
        {
            var curImg = new ImageModel() { Img = (string)parameter };
            GetSelectedImage.Invoke(curImg);
        }
    }
    
}