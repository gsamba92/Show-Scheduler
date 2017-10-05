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
using ShowScheduler.Models;
using Windows.UI.Xaml.Media.Imaging;
using AutoMapper;
using Windows.Storage;
using SQLitePCL;
using System.Diagnostics;
using Windows.UI.Popups;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShowScheduler.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WatchListView : Page
    {
        ObservableCollection<TvShow> Shows = new ObservableCollection<TvShow>();
        TableQueries tableQueries = new TableQueries();
        static RootObject showDets;
        public WatchListView()
        {
            this.InitializeComponent();
            getWatchList();
            //ObservableCollection<TvShow> Shows = new ObservableCollection<TvShow>();
            //Shows = ShowManager.getShows();
          //  WatchListGrid.ItemsSource = Shows;
          
        }
        private void getWatchList()
        {           
            try
            {
                tableQueries.createTable();
                Shows = tableQueries.selectAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void  AddAlert_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var result = await AlertContentDialog.ShowAsync();
            //  btn.Content = "Res : "+result;
            if (result.ToString() == "Primary")
            {
                var _GridView = WatchListGrid as GridView;
                TvShow show = ((Windows.UI.Xaml.FrameworkElement)btn.Parent).DataContext as TvShow;
                tableQueries.updateRecord(show.ShowTitle);
                WatchListGrid.ItemsSource = tableQueries.selectAll();
            }
        }

        private async void DeleteShow_Click(object sender, RoutedEventArgs e)
        {
            
            var btn = sender as Button;
            var result = await DeleteContentDialog.ShowAsync();
            if (result.ToString() == "Primary") {

                var _GridView = WatchListGrid as GridView;
                TvShow show = ((Windows.UI.Xaml.FrameworkElement)btn.Parent).DataContext as TvShow;
                tableQueries.deleteRecord(show.ShowTitle);
                WatchListGrid.ItemsSource = tableQueries.selectAll();
            }

        }

       
        private async void Find_Click(object sender, RoutedEventArgs e)
        {
            if (searchShow.Text != "")
            {
                try
                {
                    showDets =

                         await TvShowApi.GetShowDetails(searchShow.Text);
                }
                catch (Exception)
                {
                    Warning.Text = "No show found! Try again";
                }
                if (showDets!=null) {
                    Warning.Text = "";
                   String icon = showDets.image.medium;
                    findListView.Visibility = Visibility.Visible;
                    posterImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                    ShowName.Text = "Name: " + showDets.name + "\n" + "Language: " + showDets.language + "\nRating: " + showDets.rating.average + "\nStatus: " + showDets.status + "\nTime" + showDets.schedule.time + "\nDays" + String.Join(String.Empty, showDets.schedule.days.ToArray()) + "\nNetwork" + showDets.network.name;

                }

            }


        }

        private async void AddToWatchList_Click(object sender, RoutedEventArgs e)
        {
            Mapper.CreateMap<RootObject, TvShow>()
             .ForMember(dest => dest.ShowTitle, opt => opt.MapFrom(src => src.name))
             .ForMember(dest => dest.ShowPosterIcon, opt => opt.MapFrom(src => src.image.medium))
             .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.rating.average))
             .ForMember(dest => dest.ShowStatus, opt => opt.MapFrom(src => src.status))
             .ForMember(dest => dest.ShowTime, opt => opt.MapFrom(src => src.schedule.time))
             .ForMember(dest => dest.ShowNetwork, opt => opt.MapFrom(src => src.network.name))
             .ForMember(dest => dest.ShowDays, opt => opt.MapFrom(src => String.Join(String.Empty, src.schedule.days.ToArray())));
            TvShow show = Mapper.Map<TvShow>(showDets);
            var flag = tableQueries.insertRecord(show);
            if (flag==false) {
                var btn = sender as Button;
                var result = await WarningContentDialog.ShowAsync();
            }
            WatchListGrid.ItemsSource = tableQueries.selectAll();
            findListView.Visibility = Visibility.Collapsed;
            searchShow.Text = String.Empty;
            AddFlyout.Hide();

        }
    }
}
