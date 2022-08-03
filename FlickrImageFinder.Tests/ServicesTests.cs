using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using FlickrImageFinder.ViewModels;
using FlickrImageFinderUnitTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrImageFinder.Tests
{
    [TestClass]
    public class ServicesTests
    {
        [TestMethod]
        public void FlickerApi_Initialize_Verify()
        {
            //Arrange
            const string testAPiKey = "TEST";

            //Act
            FlickerApi.LoadApi(testAPiKey);

            //Assert
            Assert.IsNotNull(FlickerApi.Responses);
            Assert.IsNotNull(FlickerApi.Responses.items);
            Assert.AreEqual(20,FlickerApi.Responses.items.Length);
        }
    }
}
