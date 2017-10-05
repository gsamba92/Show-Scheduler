using ShowScheduler.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShowScheduler.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyAlertView : Page
    {
        TableQueries tableQueries = new TableQueries();
        public MyAlertView()
        {
            this.InitializeComponent();
            ObservableCollection<TvShow> Alerts = new ObservableCollection<TvShow>();
            Alerts = tableQueries.selectAllAlerts();
            AlertList.ItemsSource = Alerts;
        }

        private async void EditAlert_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var result = await EditAlertContentDialog.ShowAsync();
            //btn.Content = "Result: " + result;
        }

        private async void DeleteAlert_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var result = await DeleteAlertContentDialog.ShowAsync();
            if (result.ToString() == "Primary")
            {

                var _GridView = AlertList as ListView;
                TvShow show = ((Windows.UI.Xaml.FrameworkElement)btn.Parent).DataContext as TvShow;
                tableQueries.deleteAlertRecord(show.ShowTitle);
                AlertList.ItemsSource = tableQueries.selectAllAlerts();
            }            
        }
    }
}
