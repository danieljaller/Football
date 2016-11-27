using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        Match match;
        Team homeTeam;
        Team visitorTeam;
        int homeScore;
        int visitorScore;
        bool isPlayed;
        HashSet<Event> homeGoals;
        HashSet<Event> visitorGoals;


        public MatchProtocolPage()
        {
            InitializeComponent();
            matchDatePicker.DisplayDateStart = DateTime.Today;
            matchesList.ItemsSource = ServiceLocator.Instance.MatchService.GetAll();
                        
        }
       
        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addGoalHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
            //if (addEvent)
            //{
            //    match.HomeGoals.Add(addEvent);
            //}
        }

        private void addGoalAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeGoalAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeAssistHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addAssistHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addAssistAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeAssistAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeRedCardsHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeRedCardsAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void addYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
        }

        private void removeYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removePlayerHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addPlayerHome_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow();
            var addExchange = addPlayerWindow.ShowDialog();
        }

        private void addPlayerAway_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddPlayerWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void removePlayerAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeExchangeHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddExchangeWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void addExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddExchangeWindow();
            var addExchange = addExchangeWindow.ShowDialog();
        }

        private void removeExchangeAway_Click(object sender, RoutedEventArgs e)
        {

        }

        private void matchesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            convertListsToObjects();
            homeTeamNameBlock.DataContext = homeTeam;
            visitorTeamNameBlock.DataContext = visitorTeam;
            if (isPlayed)
            {
                homeTeamScoreBlock.DataContext = homeScore;
                visitorTeamScoreBlock.DataContext = visitorScore;
            }
            else
            {
                homeTeamScoreBlock.DataContext = " ";
                visitorTeamScoreBlock.DataContext = " ";    
            }
            homeGoalsList.ItemsSource = homeGoals;
            visitorGoalsList.ItemsSource = visitorGoals;
                       
        }
        private void convertListsToObjects()
        {
            
            match = (Match)matchesList.SelectedItem;
            isPlayed = match.IsPlayed;
            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            visitorScore = match.VisitorGoals.Count();
            homeGoals = match.HomeGoals.ToHashSet();
            visitorGoals = match.VisitorGoals.ToHashSet();
        }
    }
}
