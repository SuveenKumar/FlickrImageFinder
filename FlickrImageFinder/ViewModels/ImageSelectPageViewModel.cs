using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrImageFinder.ViewModels
{
    public class ImageSelectPageViewModel:ViewModelBase
    {
        private ImageModel _imageModel;
        public ImageModel ImageModel 
        { 
            get 
            {
                return _imageModel;
            }
            set
            {
                _imageModel= value;
                OnPropertyChanged(nameof(ImageModel));
            }
        }

        public void UpdateSelectedImage(ImageModel img)
        {
            ImageModel = img;
        }
    }
}
