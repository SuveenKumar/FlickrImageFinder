using FlickrImageFinder;
using FlickrImageFinder.UserControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using FlickrImageFinder.Commands;

namespace FlickrImageFinderUnitTests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void Commands_FindCOmmand_Verify()
        {
            //Arrange
            List<string> testList=new List<string>();
            FindCommand testCommand = new FindCommand(() => { return "TEST"; });
            testCommand.clearListFn += () => { };
            testCommand.OnListUpdated += (param) => 
            {
                testList = param;
            };

            //Act
            testCommand.Execute(this);

            //Assert
            Assert.IsNotNull(testList);
            Assert.IsNotNull(testList.Count == 20);
        }
    }
}
