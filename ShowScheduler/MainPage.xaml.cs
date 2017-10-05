using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShowScheduler
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();           
            MyFrame.Navigate(typeof(Views.HomeView));
            TitleTextBlock.Text = "Home";
            Home.IsSelected = true;

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                
                MyFrame.Navigate(typeof(Views.HomeView));
                TitleTextBlock.Text = "Home";
            }
            else if (Watchlist.IsSelected)
            {
               
                MyFrame.Navigate(typeof(Views.WatchListView));
                TitleTextBlock.Text = "My Watchlist";
            }
            else if (Alerts.IsSelected)
            {

                MyFrame.Navigate(typeof(Views.MyAlertView));
                TitleTextBlock.Text = "My Alerts";
            }
        }

    }
}
