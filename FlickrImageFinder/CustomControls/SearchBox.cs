﻿using FlickrImageFinder.Common;
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
    public enum SearchBoxType
    {
        NOTEXT,
        TEXT
    }
    public class SearchBox:TextBox
    {
        public SearchBoxType searchBoxType { get; set; }

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
                if(searchBoxType==SearchBoxType.NOTEXT)
                {
                    Text = "";
                    searchBoxType = SearchBoxType.TEXT;
                    Foreground = Brushes.Black;
                    FontStyle = FontStyles.Normal;
                }
            };

            LostFocus += (s, e) =>
            {
                if (Text.Length == 0)
                {
                    searchBoxType = SearchBoxType.NOTEXT;
                    Text = Constants.PlaceHolderText;
                    Foreground = Brushes.Gray;
                    FontStyle = FontStyles.Italic;
                }
            };

            TextChanged += (s, e) =>
            {
                if (Text.Length == 0 && !IsFocused)
                {
                    searchBoxType = SearchBoxType.NOTEXT;
                    Text = Constants.PlaceHolderText;
                    Foreground = Brushes.Gray;
                    FontStyle = FontStyles.Italic;
                }
            };
        }
    }
}
