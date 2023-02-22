using FlickrImageFinder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlickrImageFinder.CustomControls
{
    public class SearchBox:TextBox
    {
        public bool IsTextPresent { get; set; }

        public SearchBox()
        {
            InitializedDefaultProperties();
            InitializedFocusChangeEvents();
        }

        private void InitializedDefaultProperties()
        {
            Text = Constants.PlaceHolderText;
            Foreground = Brushes.Gray;
            FontStyle = FontStyles.Italic;
            VerticalAlignment = VerticalAlignment.Center;
        }

        private void InitializedFocusChangeEvents()
        {
            GotFocus += (s, e) =>
            {
                if(Text == Constants.PlaceHolderText)
                {
                    Text = "";
                    IsTextPresent = false;
                    Foreground = Brushes.Black;
                    FontStyle = FontStyles.Normal;
                }
            };

            LostFocus += (s, e) =>
            {
                if (Text.Length == 0)
                {
                    IsTextPresent = false;
                    Text = Constants.PlaceHolderText;
                    Foreground = Brushes.Gray;
                    FontStyle = FontStyles.Italic;
                }
            };
        }
    }
}
