using FlickrImageFinder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using static System.Net.Mime.MediaTypeNames;

namespace FlickrImageFinder.UserControls
{
    /// <summary>
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public SearchPanel()
        {
            InitializeComponent();
            searchBox.Text = Constants.PlaceHolderText;
            searchBox.Foreground = Brushes.Gray;
            searchBox.FontStyle = FontStyles.Italic;

            searchBox.TextChanged += (o,e) => 
            {
                if (searchBox.Text.Length == 0 || searchBox.Text==Constants.PlaceHolderText)
                {
                    findBtn.IsEnabled = false;
                }
                else
                {
                    findBtn.IsEnabled = true;
                }
                if (searchBox.Text.Length==0 && !searchBox.IsFocused)
                {
                    searchBox.Text = Constants.PlaceHolderText;
                    searchBox.Foreground = Brushes.Gray;
                    searchBox.FontStyle = FontStyles.Italic;
                }
            };

            GotFocus += (s, e) =>
            {
                if (searchBox.Text == Constants.PlaceHolderText)
                {
                    searchBox.Text = "";
                    searchBox.Foreground = Brushes.Black;
                    searchBox.FontStyle = FontStyles.Normal;
                }
            };

            LostFocus += (s, e) =>
            {
                if (searchBox.Text.Length == 0 && !backBtn.IsEnabled)
                {
                    searchBox.Text = Constants.PlaceHolderText;
                    searchBox.Foreground = Brushes.Gray;
                    searchBox.FontStyle = FontStyles.Italic;
                }
                if (searchBox.Text.Length == 0 && backBtn.IsEnabled)
                {
                    searchBox.Foreground = Brushes.Black;
                    searchBox.FontStyle = FontStyles.Normal;
                }
            };
        }
        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(UserControl), new UIPropertyMetadata(null));

        public ICommand FindCommand
        {
            get { return (ICommand)GetValue(FindCommandProperty); }
            set { SetValue(FindCommandProperty, value); }
        }

        public static readonly DependencyProperty FindCommandProperty = DependencyProperty.Register("FindCommand", typeof(ICommand), typeof(UserControl), new UIPropertyMetadata(null));

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        public static readonly DependencyProperty BackCommandProperty = DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(UserControl), new UIPropertyMetadata(null));

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), typeof(UserControl), new UIPropertyMetadata(null));
    }
}
