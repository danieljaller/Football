﻿using System;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateOrAdministrateSeries.xaml
    /// </summary>
    public partial class CreateOrAdministrateSeriesPage : Page
    {
        public CreateOrAdministrateSeriesPage()
        {
            InitializeComponent();

            var match1 = new TempMatch("16-09-12", "Lag1", "Lag2", "Plats", "2-1");
            var match2 = new TempMatch("16-09-24", "Lag1", "Lag2", "Plats", "1-5");
            var match3 = new TempMatch("16-10-11", "Lag1", "Lag2", "Plats", "4-2");
            var match4 = new TempMatch("16-11-02", "Lag1", "Lag2", "Plats", "5-1");
            var match5 = new TempMatch("16-11-23", "Lag1", "Lag2", "Plats", "");
            var match6 = new TempMatch("16-09-22", "Lag1", "Lag2", "Plats", "5-2");
            var match7 = new TempMatch("16-10-12", "Lag1", "Lag2", "Plats", "8-1");
            var match8 = new TempMatch("16-10-22", "Lag1", "Lag2", "Plats", "9-1");
            var match9 = new TempMatch("16-11-12", "Lag1", "Lag2", "Plats", "");
            var match10 = new TempMatch("16-11-19", "Lag1", "Lag2", "Plats", "");

            Dictionary<string, List<TempMatch>> seriesDictionary = new Dictionary<string, List<TempMatch>>() {
                {"serie1", new List<TempMatch>() {match1, match2, match3, match4, match5 } },
                {"serie2", new List<TempMatch>() {match6, match7, match8, match9, match10 } }
            };
                    
            seriesList.ItemsSource = seriesDictionary;
        }

        private void NewSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateSeriesPageFrame.Content = new NewSeriesPage();
            matchSchedule.Visibility = Visibility.Collapsed;
        }
    }
    public class TempMatch
    {
        public string Date { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Location { get; set; }
        public string Result { get; set; }
        public TempMatch(string date, string team1, string team2, string location, string result)
        {
            Date = date;
            Team1 = team1;
            Team2 = team2;
            Location = location;
            Result = result;
        }
    }
}
