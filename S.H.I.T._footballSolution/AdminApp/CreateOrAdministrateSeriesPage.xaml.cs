using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            seriesList.ItemsSource = ServiceLocator.Instance.SerieService.GetAll();
        }

        public CreateOrAdministrateSeriesPage(Serie selectedSerie)
        {
            InitializeComponent();
            List<Serie> searchResult = new List<Serie>();
            searchResult.Add(selectedSerie);
            seriesList.ItemsSource = searchResult;
        }

        private void NewSeriesButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateSeriesPageFrame.Content = new NewSeriesPage();
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
                matchProtocolList.SelectedItem = null;
            }
        }
        
    }
}
