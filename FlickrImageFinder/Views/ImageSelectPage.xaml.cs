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
        private void DownloadImage(string url)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";

            var result = dialog.ShowDialog(); //shows save file dialog
            if (result == true)
            {
                Console.WriteLine("writing to: " + dialog.FileName); //prints the file to save

                var wClient = new WebClient();
                wClient.DownloadFile(url, dialog.FileName);
            }

        }

        private double _zoomValue = 1.0;

        private void img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                _zoomValue += 0.1;
            }
            else
            {
                _zoomValue -= 0.1;
            }

            ScaleTransform scale = new ScaleTransform(_zoomValue, _zoomValue);
            img.LayoutTransform = scale;
            e.Handled = true;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Creating a Right Click context menu
            ContextMenu cm = new ContextMenu();

            //Adding Download Menu Item to Context Menu
            MenuItem downloadMenu = new MenuItem();
            downloadMenu.Header = "Download";
            downloadMenu.Click += (s, e) =>
            {
                DownloadImage(img.Source.ToString());
            };
            cm.Items.Add(downloadMenu);

            //Adding Close Menu Item to Context Menu
            MenuItem copyImageAddressMenu = new MenuItem();
            copyImageAddressMenu.Header = "Copy Image Address";
            copyImageAddressMenu.Click += (s, e) =>
            {
                Clipboard.SetText(img.Source.ToString());
            };
            cm.Items.Add(copyImageAddressMenu);

            //Adding Close Menu Item to Context Menu
            MenuItem closeMenu = new MenuItem();
            closeMenu.Header = "Close";
            //closeMenu.Click += (s, e) =>
            //{
            //    Close();
            //};
            cm.Items.Add(closeMenu);


            cm.StaysOpen = true;
            this.ContextMenu = cm;
        }
    }
}
