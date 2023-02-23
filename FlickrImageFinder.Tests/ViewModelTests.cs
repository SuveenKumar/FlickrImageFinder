using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrImageFinder.ViewModels;
using FlickrImageFinder.Models;
using FlickrImageFinder.Common;
using System.Collections.ObjectModel;

namespace FlickrImageFinderUnitTests
{
    [TestClass]
    public class ViewModelTests
    {
        private const string SearchString1= "Boy";
        private const string SearchString2 = "Girl";
        private const string SearchStringNoResult = "aaaaaaaaaaaaaaa00000000000000000000";
        private const string FakeImageUrl = "https://fakeurl.com/fakeimg.png";
        private MainViewModel testMainVM;
        private void InitializeMainViewModel()
        {
            testMainVM = new MainViewModel();
        }

        [TestMethod]
        public void MainViewModel_Initialize_Verify()
        {
            //Arrange
            InitializeMainViewModel();

            // Assert Default Values
            Assert.IsNull(testMainVM.CurrentViewModel);
            Assert.IsNotNull(testMainVM.FindButtonCommand);
            Assert.IsFalse(testMainVM.BackButtonEnabled);

            //Act-1 Find Images for first search string 
            testMainVM.SearchStr = SearchString1;
            testMainVM.FindButtonCommand.Execute(null);

            //Assert
            Assert.IsNotNull(testMainVM.CurrentViewModel);
            Assert.AreEqual(typeof(ImageListPageViewModel),testMainVM.CurrentViewModel.GetType());
            Assert.IsTrue(testMainVM.BackButtonEnabled);


            //Act-2 Find Images for first search string 
            testMainVM.SearchStr = SearchString2;
            testMainVM.FindButtonCommand.Execute(null);

            //Assert
            Assert.IsNotNull(testMainVM.CurrentViewModel);
            Assert.AreEqual(typeof(ImageListPageViewModel), testMainVM.CurrentViewModel.GetType());
            Assert.IsTrue(testMainVM.BackButtonEnabled);

            //Act-3 Navigate back to previous Result
            testMainVM.BackButtonCommand.Execute(null);

            //Assert
            Assert.IsNotNull(testMainVM.CurrentViewModel);
            Assert.AreEqual(typeof(ImageListPageViewModel), testMainVM.CurrentViewModel.GetType());
            Assert.AreEqual(SearchString1, testMainVM.SearchStr);
            Assert.IsTrue(testMainVM.BackButtonEnabled);

            //Act-4 Navigate back to previous Result
            testMainVM.BackButtonCommand.Execute(null);

            //Assert
            Assert.IsNull(testMainVM.CurrentViewModel);
            Assert.AreEqual(string.Empty, testMainVM.SearchStr);
            Assert.IsFalse(testMainVM.BackButtonEnabled);

            //Act-5 Find Images for No Results search string 
            testMainVM.SearchStr = SearchStringNoResult;
            testMainVM.FindButtonCommand.Execute(null);

            //Assert
            Assert.IsNotNull(testMainVM.CurrentViewModel);
            Assert.AreEqual(typeof(ImageListPageViewModel), testMainVM.CurrentViewModel.GetType());
            Assert.IsTrue(testMainVM.BackButtonEnabled);
        }

        [TestMethod]
        public void ImageListAndSelectPageViewModel_Initialize_Verify()
        {
            //Arrange
            InitializeMainViewModel();

            ImageListPageViewModel testListPageVM= new ImageListPageViewModel();

            //// Assert Default Values
            Assert.IsFalse(testListPageVM.IsNoResultVisible);
            Assert.IsNotNull(testListPageVM.SelectImageCommand);
            Assert.IsNull(testListPageVM.ImageList);

            //Act-1: Update image list with no result
            testMainVM.SearchStr = SearchStringNoResult;
            testMainVM.FindButtonCommand.Execute(null);
            testListPageVM = testMainVM.CurrentViewModel as ImageListPageViewModel;

            //Assert
            Assert.IsTrue(testListPageVM.IsNoResultVisible);
            Assert.AreEqual(0, testListPageVM.ImageList.Count);

            //Act-2: Select a fake image

            var fakeImage =new ImageModel()
            {
                Img = FakeImageUrl,
            };

            testListPageVM.SelectImageCommand.Execute(fakeImage.Img);
            var testSelectPageVM = testMainVM.CurrentViewModel as ImageSelectPageViewModel;
            //Assert
            Assert.AreEqual(fakeImage.Img, testSelectPageVM.ImageModel.Img);

            //Act-3: Navigate back to previous page
            testMainVM.BackButtonCommand.Execute(null);

            testListPageVM = testMainVM.CurrentViewModel as ImageListPageViewModel;

            //Assert
            Assert.AreEqual(0, testListPageVM.ImageList.Count);
            Assert.IsTrue(testListPageVM.IsNoResultVisible);
            Assert.AreEqual(0, testListPageVM.ImageList.Count);
        }
    }
}
