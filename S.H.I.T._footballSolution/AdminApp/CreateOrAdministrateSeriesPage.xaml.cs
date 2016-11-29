using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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
        private HashSet<Team> teamList = new HashSet<Team>();

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

            Dictionary<string, HashSet<TempMatch>> seriesDictionary = new Dictionary<string, HashSet<TempMatch>>() {
                {"serie1", new HashSet<TempMatch>() {match1, match2, match3, match4, match5 } },
                {"serie2", new HashSet<TempMatch>() {match6, match7, match8, match9, match10 } }
            };
            seriesList.ItemsSource = ServiceLocator.Instance.SerieService.GetAll();
        }

        public CreateOrAdministrateSeriesPage(Serie selectedSerie)
        {
            Dictionary<string, HashSet<TempMatch>> seriesDictionary = new Dictionary<string, HashSet<TempMatch>>() {
                {selectedSerie.Name.Value, new HashSet<TempMatch>() }
            };
            InitializeComponent();
            seriesList.ItemsSource = seriesDictionary;
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
            HashSet<Guid> matchScheduleWithIds = selectedSerie.MatchTable.ToHashSet();
            HashSet<Match> matchScheduleWithMatches;
            List<Team> homeTeamList, visitorTeamList;
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void SetItemSources(HashSet<Match> matchScheduleWithMatches, List<Team> homeTeamList, List<Team> visitorTeamList)
        {
            matchProtocolList.ItemsSource = matchScheduleWithMatches;
            homeTeamListBox.ItemsSource = homeTeamList;
            visitorTeamListBox.ItemsSource = visitorTeamList;
            dateListBox.ItemsSource = matchScheduleWithMatches;
            resultListBox.ItemsSource = matchScheduleWithMatches;
        }

        private void CreateAndConvertLists(HashSet<Guid> matchScheduleWithIds, out HashSet<Match> matchScheduleWithMatches, out List<Team> homeTeamList, out List<Team> visitorTeamList)
        {
            matchScheduleWithMatches = new HashSet<Match>();
            homeTeamList = new List<Team>();
            visitorTeamList = new List<Team>();

            foreach (var matchId in matchScheduleWithIds)
            {
                matchScheduleWithMatches.Add(ServiceLocator.Instance.MatchService.GetBy(matchId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                homeTeamList.Add(ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                visitorTeamList.Add(ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId));
            }
        }

        private void matchProtocolList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Match matchProto = (Match)matchProtocolList.SelectedItem;
            if (matchProto != null)
            {
                var previousDate = matchProto.Date.Value;
                var previousResult = matchProto.Result;
                var matchProtocolWindow = new MatchProtocol(matchProto);
                matchProtocolWindow.ShowDialog();
                if (matchProto.Date.Value != previousDate)
                    dateListBox.Items.Refresh();
                if (matchProto.Result != previousResult)
                    resultListBox.Items.Refresh();
            }
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
