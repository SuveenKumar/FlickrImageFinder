using FlickrImageFinder.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for ImageListPage.xaml
    /// </summary>
    public partial class ImageListPage : Page
    {
        public ImageListPage()
        {
            InitializeComponent();
        }

        private void img_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 0.5;
        }
        private void img_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }
    }
}
