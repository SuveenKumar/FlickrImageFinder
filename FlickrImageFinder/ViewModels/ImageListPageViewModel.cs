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

        public Visibility IsPopupVisible { get; set; }

        public ICommand SelectImageCommand { get; set; }

        public Action<ImageModel> GetSelectedImage;
        public ImageListPageViewModel()
        {
            IsPopupVisible = Visibility.Hidden;
            SelectImageCommand = new ButtonCommand(ExecuteSelectCommand);
        }

        public void UpdateImageList(ObservableCollection<ImageModel> imageList)
        {
            ImageList = imageList;
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
        }
        public void ExecuteSelectCommand(object parameter)
        {
            var curImg = new ImageModel() { Img = (string)parameter };
            GetSelectedImage.Invoke(curImg);
        }
    }
    
}