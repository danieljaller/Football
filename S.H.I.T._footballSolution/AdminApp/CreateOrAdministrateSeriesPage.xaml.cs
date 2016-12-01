using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateOrAdministrateSeries.xaml
    /// </summary>
    public partial class CreateOrAdministrateSeriesPage : Page
    {
        private HashSet<Team> teamList = new HashSet<Team>();
        private HashSet<Guid> matchScheduleWithIds;
        private HashSet<Match> matchScheduleWithMatches;
        private List<Team> homeTeamList, visitorTeamList;
        private Serie selectedSerie;

        public CreateOrAdministrateSeriesPage()
        {
            InitializeComponent();   
            seriesList.ItemsSource = ServiceLocator.Instance.SerieService.GetAll();
            showAllRadioButton.IsChecked = true;
        }

        public CreateOrAdministrateSeriesPage(Serie selectedSerie)
        {
            InitializeComponent();
            List<Serie> searchResult = new List<Serie>();
            searchResult.Add(selectedSerie);
            seriesList.ItemsSource = searchResult;
        }
        public CreateOrAdministrateSeriesPage(Team team, bool areMatchesPlayed)
        {
            InitializeComponent();
            mainGrid.Background = new SolidColorBrush(Colors.White);
            showAllRadioButton.Visibility = Visibility.Collapsed;
            showCommingRadioButton.Visibility = Visibility.Collapsed;
            showPlayedRadioButton.Visibility = Visibility.Collapsed;
            seriesList.Visibility = Visibility.Collapsed;
            NewSeriesButton.Visibility = Visibility.Collapsed;
            
            if (areMatchesPlayed)
            {
                Header.Text = $"{team.Name}s spelade matcher";
                matchScheduleWithIds = ServiceLocator.Instance.MatchService.GetAll().Where(m => m.HomeTeamId == team.Id || m.VisitorTeamId == team.Id).Where(m => m.IsPlayed == true).Select(m => m.Id).ToHashSet();
                CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
                SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
            }
            else
            {
                Header.Text = $"{team.Name}s kommande matcher";
                matchScheduleWithIds = ServiceLocator.Instance.MatchService.GetAll().Where(m => m.HomeTeamId == team.Id || m.VisitorTeamId == team.Id).Where(m => m.IsPlayed == false).Select(m => m.Id).ToHashSet();
                CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
                SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
            }
        }

        private void NewSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateSeriesPageFrame.Content = new NewSeriesPage();
        }

        private void seriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSerie = (Serie)seriesList.SelectedItem;
            matchScheduleWithIds = selectedSerie.MatchTable.ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
            showAllRadioButton.IsChecked = true;
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
                matchProtocolList.SelectedItem = null;
            }
        }

        private void showAllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedSerie = (Serie)seriesList.SelectedItem;
            matchScheduleWithIds = selectedSerie.MatchTable.ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void showPlayedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedSerie = (Serie)seriesList.SelectedItem;
            matchScheduleWithIds = selectedSerie.MatchTable.Where(m => ServiceLocator.Instance.MatchService.GetBy(m).IsPlayed == true).ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void showCommingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedSerie = (Serie)seriesList.SelectedItem;
            matchScheduleWithIds = selectedSerie.MatchTable.Where(m => ServiceLocator.Instance.MatchService.GetBy(m).IsPlayed == false).ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }
    }
}
