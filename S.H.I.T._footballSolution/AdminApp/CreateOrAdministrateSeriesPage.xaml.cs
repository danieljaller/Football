using FootballEngine.Domain.Entities;
using FootballEngine.Services;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateOrAdministrateSeries.xaml
    /// </summary>
    public partial class CreateOrAdministrateSeriesPage : Page
    {
        SerieService serieService = new SerieService();
        TeamService teamService = new TeamService();
        MatchService matchService = new MatchService();
        private List<Guid> matchScheduleWithIds = new List<Guid>();
        private List<Team> teamList = new List<Team>();
        private List<Match> matchScheduleWithMatches = new List<Match>();
        private List<Team> homeTeamList = new List<Team>();
        private List<Team> visitorTeamList = new List<Team>();

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
            seriesList.ItemsSource = serieService.GetAll();

        }

        public CreateOrAdministrateSeriesPage(Serie selectedSerie)
        {
            Dictionary<string, List<TempMatch>> seriesDictionary = new Dictionary<string, List<TempMatch>>() {
                {selectedSerie.Name.Value, new List<TempMatch>() }
            };
            InitializeComponent();
            seriesList.ItemsSource = seriesDictionary;
        }

        public void ConvertFromGuid()
        {
            matchScheduleWithMatches.Clear();
            homeTeamList.Clear();
            visitorTeamList.Clear();
            foreach (var matchId in matchScheduleWithIds)
            {
                matchScheduleWithMatches.Add(matchService.GetBy(matchId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                homeTeamList.Add(teamService.GetBy(match.HomeTeamId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                visitorTeamList.Add(teamService.GetBy(match.VisitorTeamId));
            }
        }

        private void NewSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateSeriesPageFrame.Content = new NewSeriesPage();
            //matchSchedule.Visibility = Visibility.Collapsed;
        }

        private void matchDatePicker_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void seriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSerie = (Serie)seriesList.SelectedItem;
            matchScheduleWithIds = selectedSerie.MatchTable;
            ConvertFromGuid();

            homeTeamListBox.ItemsSource = homeTeamList;
            visitorTeamListBox.ItemsSource = visitorTeamList;
            dateListBox.ItemsSource = matchScheduleWithMatches;
            resultListBox.ItemsSource = matchScheduleWithMatches;

            homeTeamListBox.Items.Refresh();
            visitorTeamListBox.Items.Refresh();
            dateListBox.Items.Refresh();
            resultListBox.Items.Refresh();
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
