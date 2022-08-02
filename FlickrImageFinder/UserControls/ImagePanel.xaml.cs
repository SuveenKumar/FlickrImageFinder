using FlickrImageFinder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlickrImageFinder.UserControls
{
    /// <summary>
    /// Interaction logic for ImagePanel.xaml
    /// </summary>
    public partial class ImagePanel : UserControl
    {
        public ImagePanel()
        {
            InitializeComponent();
            //int indx = 0;
            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        Image img = new Image() { Margin = new Thickness(10, 10, 10, 10), Source = new BitmapImage(new Uri("C:\\MyProjects\\FlickrImageFinder\\FlickrImageFinder\\Assets\\Logo.png")), Height = 300, Width = 300, Stretch = Stretch.Fill };
            //        wp.Items.Add(img);
            //        //    Grid.SetColumn(img, i);
            //        //    Grid.SetRow(img, j);
            //        indx++;
            //    }
            //}
        }

        public IEnumerable Url
        {
            get { return (IEnumerable)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url", typeof(IEnumerable), typeof(UserControl), new UIPropertyMetadata(null));

        //public string Img
        //{
        //    get { return (string)GetValue(ImgProperty); }
        //    set { SetValue(ImgProperty, value); }
        //}

        //public static readonly DependencyProperty ImgProperty = DependencyProperty.Register("Img", typeof(string), typeof(UserControl), new UIPropertyMetadata(null));
    }
}
