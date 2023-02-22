using FlickrImageFinder.Models;
using FlickrImageFinder.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrImageFinder.Commands
{
    public class SelectCommand : CommandBase
    {
        public Action<ImageModel> GetSelectedImage { get; set; }
        public override void Execute(object parameter)
        {
            var curImg=new ImageModel() { Img= (string)parameter };
            GetSelectedImage.Invoke(curImg);
        }
    }
}
