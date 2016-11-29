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
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MatchProtocolPage.xaml
    /// </summary>
    public partial class MatchProtocolPage : Page
    {
        //MatchService matchService;
        //TeamService teamService;
        //PlayerService playerService;
        Match match;
        Team homeTeam;
        Team visitorTeam;
        int homeScore;
        int visitorScore;
        bool isPlayed;
        ObservableCollection<Event> homeGoals;
        ObservableCollection<Event> visitorGoals;
        ObservableCollection<Event> homeAssists;
        ObservableCollection<Event> visitorAssists;
        ObservableCollection<Event> homeRedCards;
        ObservableCollection<Event> visitorRedCards;
        ObservableCollection<Event> homeYellowCards;
        ObservableCollection<Event> visitorYellowCards;
        ObservableCollection<Guid> homeLineup;
        ObservableCollection<Guid> visitorLineup;
        ObservableCollection<Exchange> homeExchanges;
        ObservableCollection<Exchange> visitorExchanges;


        public MatchProtocolPage()
        {
            //matchService = new MatchService();
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            matchDatePicker.DisplayDateStart = DateTime.Today;
            matchesList.ItemsSource = ServiceLocator.Instance.MatchService.GetAll(); 
        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            match.Date.Value = (DateTime)matchDatePicker.SelectedDate;
        }

        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeGoals.Remove((Event)homeGoalsList.SelectedItem);
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            homeScore--;
            homeTeamScoreBlock.DataContext = homeScore;
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
                homeScore++;
                homeTeamScoreBlock.DataContext = homeScore;
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
                visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
                visitorScore++;
                visitorTeamScoreBlock.DataContext = visitorScore;
                visitorGoalsList.ItemsSource = visitorGoals;
            }
        }

        private void removeGoalAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorGoals.Remove((Event)visitorGoalsList.SelectedItem);
            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
            visitorScore--;
            visitorTeamScoreBlock.DataContext = visitorScore;
            visitorGoalsList.ItemsSource = visitorGoals;
        }

        private void removeAssistHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeAssists.Remove((Event)homeAssistsList.SelectedItem);
            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
            homeAssistsList.ItemsSource = homeAssists;
        }

        private void addAssistHome_Click(object sender, RoutedEventArgs e)
        {
            //MatchMinute[] minutes = new MatchMinute[homeGoals.Count()](homeGoals.Select)
            var addEventWindow = new AddEvent(homeTeam );
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeAssists.Add(addEventWindow.result);
                homeAssists = new ObservableCollection<Event>(match.HomeAssists);
                homeAssistsList.ItemsSource = homeAssists;
            }
        }

        private void addAssistAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorAssists.Add(addEventWindow.result);
                visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
                visitorAssistsList.ItemsSource = visitorAssists;
            }
        }

        private void removeAssistAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorAssists.Remove((Event)visitorAssistsList.SelectedItem);
            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
            visitorAssistsList.ItemsSource = visitorAssists;
        }

        private void removeRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeRedCards.Remove((Event)homeRedCardsList.SelectedItem);
            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
            homeRedCardsList.ItemsSource = homeRedCards;
        }

        private void addRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeRedCards.Add(addEventWindow.result);
                homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
                homeRedCardsList.ItemsSource = homeRedCards;
            }
        }

        private void addRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorRedCards.Add(addEventWindow.result);
                visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
                visitorRedCardsList.ItemsSource = visitorRedCards;
            }
        }

        private void removeRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorRedCards.Remove((Event)visitorRedCardsList.SelectedItem);
            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
            visitorRedCardsList.ItemsSource = visitorRedCards;
        }

        private void removeYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeYellowCards.Remove((Event)homeYellowCardsList.SelectedItem);
            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
            homeYellowCardsList.ItemsSource = homeYellowCards;
        }

        private void addYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(homeTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeYellowCards.Add(addEventWindow.result);
                homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
                homeYellowCardsList.ItemsSource = homeYellowCards;
            }
        }

        private void addYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(visitorTeam);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorYellowCards.Add(addEventWindow.result);
                visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
                visitorYellowCardsList.ItemsSource = visitorYellowCards;
            }
        }

        private void removeYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorYellowCards.Remove((Event)visitorYellowCardsList.SelectedItem);
            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
            visitorYellowCardsList.ItemsSource = visitorYellowCards;
        }

        private void removePlayerHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeLineup.Remove((Guid)homeLineupList.SelectedItem);
            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
            homeLineupList.ItemsSource = homeLineup;
        }

        private void addPlayerHome_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow(homeTeam, homeLineup);
            var addPlayer = addPlayerWindow.ShowDialog();
            if (addPlayer == true)
            {
                foreach(var player in addPlayerWindow.selectedPlayers)
                    match.HomeLineup.Add(player.Id);
                homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
                homeLineupList.ItemsSource = homeLineup;
            }
        }

        private void addPlayerAway_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow(visitorTeam, visitorLineup);
            var addPlayer = addPlayerWindow.ShowDialog();
            if (addPlayer == true)
            {
                foreach (var player in addPlayerWindow.selectedPlayers)
                    match.VisitorLineup.Add(player.Id);
                visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
                visitorLineupList.ItemsSource = visitorLineup;
            }
        }

        private void removePlayerAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorLineup.Remove((Guid)visitorLineupList.SelectedItem);
            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
            visitorLineupList.ItemsSource = visitorLineup;
        }

        private void removeExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            match.HomeExchanges.Remove((Exchange)homeExchangesList.SelectedItem);
            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
            homeExchangesList.ItemsSource = homeExchanges;
        }

        private void addExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            List<Guid> exchangedPlayerIds = (List<Guid>)ServiceLocator.Instance.PlayerService.GetAll()
                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
                                                    .Select(p => p.Id);
            var addExchangeWindow = new AddExchangeWindow(homeTeam, homeLineup, exchangedPlayerIds);
            var addExchange = addExchangeWindow.ShowDialog();
            if (addExchange == true)
            {
                match.HomeExchanges.Add(addExchangeWindow.result);
                homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
                homeExchangesList.ItemsSource = homeExchanges;
            }
        }

        private void addExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            var addExchangeWindow = new AddExchangeWindow(visitorTeam, visitorLineup);
            var addExchange = addExchangeWindow.ShowDialog();
            if (addExchange == true)
            {
                match.VisitorExchanges.Add(addExchangeWindow.result);
                visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
                visitorExchangesList.ItemsSource = visitorExchanges;
            }
        }

        private void removeExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            match.VisitorExchanges.Remove((Exchange)visitorExchangesList.SelectedItem);
            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
            visitorExchangesList.ItemsSource = visitorExchanges;
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
            matchDatePicker.SelectedDate = match.Date.Value;
            homeGoalsList.ItemsSource = homeGoals;
            visitorGoalsList.ItemsSource = visitorGoals;
            homeAssistsList.ItemsSource = homeAssists;
            visitorAssistsList.ItemsSource = visitorAssists;
            homeRedCardsList.ItemsSource = homeRedCards;
            visitorRedCardsList.ItemsSource = visitorRedCards;
            homeYellowCardsList.ItemsSource = homeYellowCards;
            visitorYellowCardsList.ItemsSource = visitorYellowCards;
            homeLineupList.ItemsSource = homeLineup;
            visitorLineupList.ItemsSource = visitorLineup;
            homeExchangesList.ItemsSource = homeExchanges;
            visitorExchangesList.ItemsSource = visitorExchanges;
        }

        private void convertListsToObjects()
        {
            match = (Match)matchesList.SelectedItem;
            isPlayed = match.IsPlayed;
            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            visitorScore = match.VisitorGoals.Count();
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
