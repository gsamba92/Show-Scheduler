using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
using ShowScheduler.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Windows.UI.Popups;

namespace ShowScheduler
{
    class TableQueries
    {
        public void createTable() {
            try {
                using (var conn = new SQLiteConnection("Scheduler.db"))
                {
                    string sql = @"CREATE TABLE IF NOT EXISTS WatchList (
                                                ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                                PosterIcon NVARCHAR(450),
                                                Rating NVARCHUAR(50),
                                                 Title NVARCHUAR(50),
                                                 Status NVARCHUAR(50),
                                                 Time NVARCHUAR(50),
                                                 Network NVARCHUAR(50),
                                                Days NVARCHUAR(50),
                                                isAlertSet NVARCHUAR(50));";
                    //  string sql = @"DROP TABLE Watchlist";
                    using (var statement = conn.Prepare(sql))
                    {
                        statement.Step();
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
          
        }
        public void updateRecord(String name) {
            try
            {
                using (var conn = new SQLiteConnection("Scheduler.db"))
                {
                    using (var statement = conn.Prepare("update WatchList set isAlertSet = 'yes' where title=?"))
                    {
                        statement.Bind(1, name);
                        statement.Step();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        internal void deleteAlertRecord(string showTitle)
        {
            try
            {
                using (var conn = new SQLiteConnection("Scheduler.db"))
                {
                    using (var statement = conn.Prepare("update WatchList set isAlertSet = 'no' where title=?"))
                    {
                        statement.Bind(1, showTitle);
                        statement.Step();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void deleteRecord(String name) {
            try
            {
                using (var conn = new SQLiteConnection("Scheduler.db"))
                {
                    using (var statement = conn.Prepare("Delete from WatchList where title='"+name+"'"))
                    {
                        statement.Step();
                    }
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public  bool insertRecord(TvShow show)
        {
            bool flag = true;
            try
            {
                using (var conn = new SQLiteConnection("Scheduler.db"))
                {
                    var stmt = conn.Prepare("SELECT title FROM WatchList where title=?");
                    stmt.Bind(1, show.ShowTitle);
                    while (stmt.Step() == SQLiteResult.ROW)
                    {
                        if (stmt[0] != null)
                        {
                            flag = false;

                        }
                       
                    }
                    if (flag ==true) {
                        using (var statement = conn.Prepare("INSERT INTO WatchList (PosterIcon, Rating,Title,Status,Time,Network,Days,isAlertSet) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"))
                        {
                            statement.Bind(1, show.ShowPosterIcon);
                            statement.Bind(2, show.Rating);
                            statement.Bind(3, show.ShowTitle);
                            statement.Bind(4, show.ShowStatus);
                            statement.Bind(5, show.ShowTime);
                            statement.Bind(6, show.ShowNetwork);
                            statement.Bind(7, show.ShowDays);
                            statement.Bind(8, "no");
                            statement.Step();
                        }
                        Debug.WriteLine("Inserted");
                    }
                    

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return flag;

        }

        public ObservableCollection<TvShow> selectAll() {
            ObservableCollection<TvShow> Shows = new ObservableCollection<TvShow>();
            try {
                using (var connection = new SQLiteConnection("Scheduler.db"))
                {
                    var statement = connection.Prepare("SELECT * FROM WatchList where isAlertSet='no'");

                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        if (statement[0] != null)
                        {
                            Shows.Add(new TvShow
                            {
                                ShowPosterIcon = statement[1].ToString(),
                                Rating = statement[2].ToString(),
                                ShowTitle = statement[3].ToString(),
                                ShowStatus = statement[4].ToString(),
                                ShowTime = statement[5].ToString(),
                                ShowNetwork = statement[6].ToString(),
                                ShowDays = statement[7].ToString()
                            });
                            //    Debug.WriteLine(String.Format("ID : {0} - Position : {1} {2} {3} {4} {5} {6} {7} {8}", statement[0].ToString(), statement[1].ToString(), statement[2].ToString(), statement[3].ToString(), statement[4].ToString(), statement[5].ToString(), statement[6].ToString(), statement[7].ToString(), statement[8].ToString()));                          
                        }

                    }

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Shows;
        }
        public ObservableCollection<TvShow> selectAllAlerts()
        {
            ObservableCollection<TvShow> Shows = new ObservableCollection<TvShow>();
            try
            {
                using (var connection = new SQLiteConnection("Scheduler.db"))
                {
                    var statement = connection.Prepare("SELECT * FROM WatchList where isAlertSet='yes'");

                    while (statement.Step() == SQLiteResult.ROW)
                    {
                        if (statement[0] != null)
                        {
                            Shows.Add(new TvShow
                            {
                                ShowPosterIcon = statement[1].ToString(),
                                Rating = statement[2].ToString(),
                                ShowTitle = statement[3].ToString(),
                                ShowStatus = statement[4].ToString(),
                                ShowTime = statement[5].ToString(),
                                ShowNetwork = statement[6].ToString(),
                                ShowDays = statement[7].ToString()
                            });
                            //    Debug.WriteLine(String.Format("ID : {0} - Position : {1} {2} {3} {4} {5} {6} {7} {8}", statement[0].ToString(), statement[1].ToString(), statement[2].ToString(), statement[3].ToString(), statement[4].ToString(), statement[5].ToString(), statement[6].ToString(), statement[7].ToString(), statement[8].ToString()));                          
                        }

                    }

                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Shows;
        }
    }
}
