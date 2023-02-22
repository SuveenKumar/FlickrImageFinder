using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrImageFinder.Commands
{
    public class SelectCommand : CommandBase
    {
        public Action<string> selectImageFn;

        public override void Execute(object parameter)
        {
            selectImageFn.Invoke((string)parameter);
        }
    }
}
