using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for SearchPanel.xaml
    /// </summary>
    public partial class SearchPanel : UserControl
    {
        public SearchPanel()
        {
            InitializeComponent();

            searchBox.TextChanged += (o,e) => 
            {
                if (searchBox.Text.Length == 0)
                {
                    btn.IsEnabled = false;
                }
                else
                {
                    btn.IsEnabled = true;
                }
            };

        }

        public ICommand FindCommand
        {
            get { return (ICommand)GetValue(FindCommandProperty); }
            set { SetValue(FindCommandProperty, value); }
        }

        public static readonly DependencyProperty FindCommandProperty = DependencyProperty.Register("FindCommand", typeof(ICommand), typeof(UserControl), new UIPropertyMetadata(null));
   
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), typeof(UserControl), new UIPropertyMetadata(null));
    }
}
