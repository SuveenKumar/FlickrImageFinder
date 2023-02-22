using FlickrImageFinder.Commands;
using FlickrImageFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlickrImageFinder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public FindCommand FindButtonCommand { get; }  //Bind to Find button

        public ObservableCollection<ImageModel> ImageList { get; set; }  //Bind to Image Panel Items source.

        public string SearchStr { get; set; } //Bind to Search box text.

        public Func<string> searchTextFn;  //fn pointer for getting image urls list from nodel

        public MainViewModel()
        {
            SearchStr = "Search Image";

            searchTextFn += () => { return SearchStr; };

            FindButtonCommand = new FindCommand(searchTextFn);

            FindButtonCommand.clearListFn += () => { ImageList.Clear(); };

            ImageList = new ObservableCollection<ImageModel>();
            CurrentViewModel = new ImageListPageViewModel();
            ((ImageListPageViewModel)CurrentViewModel).imageListFn += (s) =>
            {
                ((ImageListPageViewModel)CurrentViewModel).ImageList = ImageList;
            };
            //Invoked when list is updated.
            FindButtonCommand.OnListUpdated += (list)=>
            {
                
                foreach (var i in list)
                {
                    ImageList.Add(new ImageModel() { Img = i });

                }
                CurrentViewModel = new ImageListPageViewModel();
                ((ImageListPageViewModel)CurrentViewModel).imageListFn += (s) =>
                {
                    ((ImageListPageViewModel)CurrentViewModel).ImageList = ImageList;
                };
                ((ImageListPageViewModel)CurrentViewModel).imageListFn.Invoke(ImageList);
                ((ImageListPageViewModel)CurrentViewModel).SelectImageCommand = new SelectCommand();
                var x = ((SelectCommand)((ImageListPageViewModel)CurrentViewModel).SelectImageCommand);
                x.selectImageFn += (s) =>
                {
                    CurrentViewModel = new ImageSelectPageViewModel();
                    ((ImageSelectPageViewModel)CurrentViewModel).imageModel = new ImageModel() { Img = s.ToString() };
                    OnPropertyChanged(nameof(CurrentViewModel));
                };
                OnPropertyChanged(nameof(CurrentViewModel));
            };
           
        }
    }
}
