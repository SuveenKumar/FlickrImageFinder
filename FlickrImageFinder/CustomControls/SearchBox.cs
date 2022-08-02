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
        const string PlaceHoldertext = "Search Image";

        public SearchBox()
        {
            InitializedDefaultProperties();
            InitializedFocusChangeEvents();
        }

        private void InitializedDefaultProperties()
        {
            Text = PlaceHoldertext;
            Foreground = Brushes.Gray;
            FontStyle = FontStyles.Italic;
            VerticalAlignment = VerticalAlignment.Center;
        }

        private void InitializedFocusChangeEvents()
        {
            GotFocus += (s, e) =>
            {
                if(Text == PlaceHoldertext)
                {
                    Text = "";
                    Foreground = Brushes.Black;
                    FontStyle = FontStyles.Normal;
                }
            };

            LostFocus += (s, e) =>
            {
                if (Text.Length == 0)
                {
                    Text = PlaceHoldertext;
                    Foreground = Brushes.Gray;
                    FontStyle = FontStyles.Italic;
                }
            };
        }
    }
}
