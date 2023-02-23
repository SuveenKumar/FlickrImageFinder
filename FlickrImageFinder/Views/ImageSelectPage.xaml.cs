using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlickrImageFinder.Views
{
    /// <summary>
    /// Interaction logic for ImageSelectPage.xaml
    /// </summary>
    public partial class ImageSelectPage : Page
    {
        public ImageSelectPage()
        {
            InitializeComponent();
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta != 0)
            {
                img.Width = Math.Max(50, img.Width + (int)e.Delta);
                img.Height = Math.Max(50, img.Height + (int)e.Delta);
            }
        }
    }
}
