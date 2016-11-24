using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        MatchService matchService;
        TeamService teamService;
        PlayerService playerService;
        Match match;
        Team homeTeam;
        Team visitorTeam;
        int homeScore;
        int visitorScore;
        bool isPlayed;
        ObservableCollection<Event> homeGoals;
        ObservableCollection<Event> visitorGoals;


        public MatchProtocolPage()
        {
            matchService = new MatchService();
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            InitializeComponent();
            matchDatePicker.DisplayDateStart = DateTime.Today;
            matchesList.ItemsSource = matchService.GetAll();
                        
        }
       
        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeGoals.Remove((Event)homeGoalsList.SelectedItem);
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            homeGoalsList.ItemsSource = homeGoals;
        }

        private void addGoalHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeGoals.Add(addEventWindow.result);
                homeGoals = new ObservableCollection<Event>(match.HomeGoals);
                homeGoalsList.ItemsSource = homeGoals;
            }

        }

        private void addGoalAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorGoals.Add(addEventWindow.result);
                visitorGoals = new ObservableCollection<Event>(match.HomeGoals);
                homeGoalsList.ItemsSource = homeGoals;
            }
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
            homeTeam = teamService.GetBy(match.HomeTeamId);
            visitorTeam = teamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            visitorScore = match.VisitorGoals.Count();
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            visitorGoals = match.VisitorGoals;
        }
    }
}
