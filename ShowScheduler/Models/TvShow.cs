using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowScheduler.Models
{
    public class TvShow
    {
            public string id { get; set; }
            public string ShowPosterIcon { get; set; }
            public string Rating { get; set; }
            public string ShowTitle { get; set; }
            public string ShowStatus { get; set; }
            public string ShowDays { get; set; }
            public string ShowTime { get; set; }
            public string ShowNetwork { get; set; }
    }
    public class ShowManager
    {
        public static ObservableCollection<TvShow> getShows()
        {
            ObservableCollection<TvShow> Shows = new ObservableCollection<TvShow>();
            //Shows.Add(new TvShow { ShowPosterIcon = "../Assets/posters/Breaking-Bad.jpg", Rating = "9.5", ShowTitle = "Breaking Bad", ShowStatus = "Ended", ShowSummary = "", ShowTime="10pm",Duration="40" });            
            //Shows.Add(new TvShow { ShowPosterIcon = "../Assets/posters/vikings.jpg", Rating = "8.6", ShowTitle = "Vikings", ShowStatus = "Running", ShowSummary = "", ShowTime = "11pm", Duration = "60" });
            //Shows.Add(new TvShow { ShowPosterIcon = "../Assets/posters/narcos.jpg", Rating = "8.9", ShowTitle = "Narcos", ShowStatus = "Running", ShowSummary = "", ShowTime = "12pm", Duration = "60" });
            //Shows.Add(new TvShow { ShowPosterIcon = "../Assets/posters/the-blacklist.jpg", Rating = "8.2", ShowTitle = "Blacklist", ShowStatus = "Running", ShowSummary = "", ShowTime = "8am", Duration = "40" });
            return Shows;
        }
    }
}
