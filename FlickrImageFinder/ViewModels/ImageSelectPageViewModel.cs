using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrImageFinder.ViewModels
{
    public class ImageSelectPageViewModel:ViewModelBase
    {
        public ImageModel imageModel { get; set; }

        public void UpdateSelectedImage(ImageModel img)
        {
            imageModel = img;
            OnPropertyChanged(nameof(imageModel));
        }
    }
}
