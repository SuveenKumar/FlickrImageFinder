using FlickrImageFinder.Commands;
using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class ImageListPageViewModel:ViewModelBase
    {
        public ObservableCollection<ImageModel> ImageList { get; set; }  //Bind to Image Panel Items source.

        public Visibility IsPopupVisible { get; set; }

        public string Hello { get; set; }   
        public Action<ObservableCollection<ImageModel>> imageListFn { get; internal set; }

        public ICommand SelectImageCommand { get; set; }

        public Action<string> changeCurrentVMFn;
        public ImageListPageViewModel() 
        {
            IsPopupVisible = Visibility.Hidden;
            imageListFn += (s) => 
            {
                ImageList = s;
                if (ImageList != null)
                {
                    if (ImageList.Count == 0)
                    {
                        IsPopupVisible = Visibility.Visible;
                    }
                    else
                    {
                        IsPopupVisible = Visibility.Hidden;
                    }
                    OnPropertyChanged(nameof(IsPopupVisible));
                }
                OnPropertyChanged(nameof(ImageList));
            };
           
        }
    }
}
