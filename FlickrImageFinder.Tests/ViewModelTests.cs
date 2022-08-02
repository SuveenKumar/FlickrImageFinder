using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrImageFinder.ViewModels;
using FlickrImageFinder.Models;

namespace FlickrImageFinderUnitTests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void MainViewModel_Initialize_Verify()
        {
            MainViewModel testVm = new MainViewModel();
            Assert.AreEqual(TestHelper.PlaceHolderText,testVm.SearchStr);

            var textFn=testVm.searchTextFn;

            Assert.IsNotNull(textFn);
            Assert.AreEqual(TestHelper.PlaceHolderText, textFn.Invoke());

            var clearFn = testVm.FindButtonCommand.clearListFn;

            Assert.IsNotNull(clearFn);
            var testImgList = testVm.ImageList;
            testImgList.Add(new ImageModel() { Img="Dummy Text 1"});
            testImgList.Add(new ImageModel() { Img = "Dummy Text 2" });

            Assert.AreNotEqual(0, testImgList.Count);

            clearFn.Invoke();

            Assert.AreEqual(0, testImgList.Count);
        }
    }
}
