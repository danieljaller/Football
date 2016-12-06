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
using System.Windows.Shapes;
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MatchProtocol.xaml
    /// </summary>
    public partial class MatchProtocol : Window
    {
        Match match;
        int[] overTimeMinutes = new int[31];
        Team homeTeam, visitorTeam;
        int homeScore, homeScoreBackup, visitorScore, visitorScoreBackup, matchTime, matchTimeBackup;
        ObservableCollection<Event> homeGoals, visitorGoals, homeAssists, visitorAssists, homeRedCards, visitorRedCards, homeYellowCards, visitorYellowCards,
            homeGoalsBackup, visitorGoalsBackup, homeAssistsBackup, visitorAssistsBackup, homeRedCardsBackup, visitorRedCardsBackup, homeYellowCardsBackup, visitorYellowCardsBackup;
        ObservableCollection<Guid> homeLineup, visitorLineup, homeLineupBackup, visitorLineupBackup;
        ObservableCollection<Exchange> homeExchanges, visitorExchanges, homeExchangesBackup, visitorExchangesBackup;
        private bool isPlayedBackup;
        private DateTime dateBackup;


        public MatchProtocol(Match _match)
        {
            match = _match;
            InitializeComponent();
            SetProperties();

            if(match.Date.Value >= DateTime.Today)
                isPlayedCheckBox.IsEnabled = false;

            isPlayedCheckBox.IsChecked = match.IsPlayed;
            matchDatePicker.SelectedDate = match.Date.Value;
            for (int i = 0; i <= 30; i++)
                overTimeMinutes[i] = i;
            timeBox.ItemsSource = overTimeMinutes;
            SetSources();
        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            match.Date.Value = (DateTime)matchDatePicker.SelectedDate;

            if (match.Date.Value < DateTime.Today)
                isPlayedCheckBox.IsEnabled = true;
        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            matchTime = 90 + timeBox.SelectedIndex;
            match.MatchTimeInMinutes = matchTime;        
            timeBox.IsDropDownOpen = false;
        
    }

        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)homeGoalsList.SelectedItem;
            match.HomeGoals.Remove(activeEvent);
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            homeScore--;
            homeTeamScoreBlock.DataContext = homeScore;
            homeGoalsList.ItemsSource = homeGoals;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.Goals.Remove(match.Id);
        }

        private void addGoalHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, homeLineup, homeExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeGoals.Add(addEventWindow.Result);
                homeGoals = new ObservableCollection<Event>(match.HomeGoals);
                homeScore++;
                homeTeamScoreBlock.DataContext = homeScore;
                homeGoalsList.ItemsSource = homeGoals;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.Goals.Add(match.Id);
            }
        }

        private void addGoalAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, visitorLineup, visitorExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorGoals.Add(addEventWindow.Result);
                visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
                visitorScore++;
                visitorTeamScoreBlock.DataContext = visitorScore;
                visitorGoalsList.ItemsSource = visitorGoals;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.Goals.Add(match.Id);
            }
        }

        private void removeGoalAway_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)visitorGoalsList.SelectedItem;
            match.VisitorGoals.Remove(activeEvent);
            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
            visitorScore--;
            visitorTeamScoreBlock.DataContext = visitorScore;
            visitorGoalsList.ItemsSource = visitorGoals;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.Goals.Remove(match.Id);
        }

        private void removeAssistHome_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)homeAssistsList.SelectedItem;
            match.HomeAssists.Remove(activeEvent);
            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
            homeAssistsList.ItemsSource = homeAssists;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.Assists.Remove(match.Id);
        }

        private void addAssistHome_Click(object sender, RoutedEventArgs e)
        {
            List<MatchMinute> minutes = match.HomeGoals.Select(g => g.TimeOfEvent).ToList();
            var addEventWindow = new AddEvent(minutes, homeLineup, homeExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeAssists.Add(addEventWindow.Result);
                homeAssists = new ObservableCollection<Event>(match.HomeAssists);
                homeAssistsList.ItemsSource = homeAssists;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.Assists.Add(match.Id);
            }
        }

        private void addAssistAway_Click(object sender, RoutedEventArgs e)
        {
            List<MatchMinute> minutes = match.VisitorGoals.Select(g => g.TimeOfEvent).ToList();
            var addEventWindow = new AddEvent(minutes, visitorLineup, visitorExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorAssists.Add(addEventWindow.Result);
                visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
                visitorAssistsList.ItemsSource = visitorAssists;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.Assists.Add(match.Id);
            }
        }

        private void removeAssistAway_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)visitorAssistsList.SelectedItem;
            match.VisitorAssists.Remove(activeEvent);
            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
            visitorAssistsList.ItemsSource = visitorAssists;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.Assists.Remove(match.Id);
        }

        private void removeRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)homeRedCardsList.SelectedItem;
            match.HomeRedCards.Remove(activeEvent);
            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
            homeRedCardsList.ItemsSource = homeRedCards;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.RedCards.Remove(match.Id);
        }

        private void addRedCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, homeLineup, homeExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeRedCards.Add(addEventWindow.Result);
                homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
                homeRedCardsList.ItemsSource = homeRedCards;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.RedCards.Add(match.Id);
            }
        }

        private void addRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, visitorLineup, visitorExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorRedCards.Add(addEventWindow.Result);
                visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
                visitorRedCardsList.ItemsSource = visitorRedCards;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.RedCards.Add(match.Id);
            }
        }

        private void removeRedCardsAway_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)visitorRedCardsList.SelectedItem;
            match.VisitorRedCards.Remove(activeEvent);
            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
            visitorRedCardsList.ItemsSource = visitorRedCards;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.RedCards.Remove(match.Id);
        }

        private void removeYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)homeYellowCardsList.SelectedItem;
            match.HomeYellowCards.Remove(activeEvent);
            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
            homeYellowCardsList.ItemsSource = homeYellowCards;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.YellowCards.Remove(match.Id);
        }

        private void addYellowCardsHome_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, homeLineup, homeExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.HomeYellowCards.Add(addEventWindow.Result);
                homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
                homeYellowCardsList.ItemsSource = homeYellowCards;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.YellowCards.Add(match.Id);
            }
        }

        private void addYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            var addEventWindow = new AddEvent(matchTime, visitorLineup, visitorExchanges);
            var addEvent = addEventWindow.ShowDialog();
            if (addEvent == true)
            {
                match.VisitorYellowCards.Add(addEventWindow.Result);
                visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
                visitorYellowCardsList.ItemsSource = visitorYellowCards;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.Result.PlayerId);
                activePlayer.YellowCards.Add(match.Id);
            }
        }

        private void removeYellowCardsAway_Click(object sender, RoutedEventArgs e)
        {
            Event activeEvent = (Event)visitorAssistsList.SelectedItem;
            match.VisitorYellowCards.Remove(activeEvent);
            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
            visitorYellowCardsList.ItemsSource = visitorYellowCards;
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
            activePlayer.YellowCards.Remove(match.Id);
        }

        private void removePlayerHome_Click(object sender, RoutedEventArgs e)
        {
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy((Guid)homeLineupList.SelectedItem);
            match.HomeLineup.Remove(activePlayer.Id);
            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
            homeLineupList.ItemsSource = homeLineup;
            activePlayer.MatchesPlayedIds.Remove(match.Id);
        }

        private void addPlayerHome_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow(homeTeam, homeLineup, homeLineup.Count);
            var addPlayer = addPlayerWindow.ShowDialog();
            if (addPlayer == true)
            {
                foreach (var player in addPlayerWindow.selectedPlayers)
                {
                    match.HomeLineup.Add(player.Id);
                    player.MatchesPlayedIds.Add(match.Id);
                }
                homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
                homeLineupList.ItemsSource = homeLineup;

            }
        }

        private void addPlayerAway_Click(object sender, RoutedEventArgs e)
        {
            var addPlayerWindow = new AddPlayerWindow(visitorTeam, visitorLineup, visitorLineup.Count);
            var addPlayer = addPlayerWindow.ShowDialog();
            if (addPlayer == true)
            {
                foreach (var player in addPlayerWindow.selectedPlayers)
                {
                    match.VisitorLineup.Add(player.Id);
                    player.MatchesPlayedIds.Add(match.Id);
                }
                homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
                homeLineupList.ItemsSource = homeLineup;
                visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
                visitorLineupList.ItemsSource = visitorLineup;
            }
        }

        private void removePlayerAway_Click(object sender, RoutedEventArgs e)
        {
            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy((Guid)visitorLineupList.SelectedItem);
            match.VisitorLineup.Remove((Guid)visitorLineupList.SelectedItem);
            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
            visitorLineupList.ItemsSource = visitorLineup;
            activePlayer.MatchesPlayedIds.Remove(match.Id);
        }

        private void removeExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            Exchange selectedExchange = (Exchange)homeExchangesList.SelectedItem;
            match.HomeExchanges.Remove(selectedExchange);
            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
            homeExchangesList.ItemsSource = homeExchanges;
            Player playerIn = ServiceLocator.Instance.PlayerService.GetBy(selectedExchange.PlayerInId);
            playerIn.MatchesPlayedIds.Remove(match.Id);
        }

        private void addExchangeHome_Click(object sender, RoutedEventArgs e)
        {
            List<Guid> playerOutIds = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(match.HomeTeamId)
                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
                                                    .Select(p => p.Id).ToList();
            List<Guid> playerInIds = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(match.HomeTeamId)
                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
                                                    .Select(p => p.Id).ToList();
            var addExchangeWindow = new AddExchangeWindow(homeLineup, matchTime, playerOutIds, playerInIds);
            var addExchange = addExchangeWindow.ShowDialog();
            if (addExchange == true)
            {
                match.HomeExchanges.Add(addExchangeWindow.Result);
                homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
                homeExchangesList.ItemsSource = homeExchanges;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addExchangeWindow.Result.PlayerInId);
                activePlayer.MatchesPlayedIds.Add(match.Id);
            }
        }

        private void addExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            List<Guid> playerOutIds = ServiceLocator.Instance.PlayerService.GetAll()
                                                    .Where(p => match.VisitorExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
                                                    .Select(p => p.Id).ToList();
            List<Guid> playerInIds = ServiceLocator.Instance.PlayerService.GetAll()
                                                    .Where(p => match.VisitorExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
                                                    .Select(p => p.Id).ToList();
            var addExchangeWindow = new AddExchangeWindow(visitorLineup, matchTime, playerOutIds, playerInIds);
            var addExchange = addExchangeWindow.ShowDialog();
            if (addExchange == true)
            {
                match.VisitorExchanges.Add(addExchangeWindow.Result);
                visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
                visitorExchangesList.ItemsSource = visitorExchanges;
                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addExchangeWindow.Result.PlayerInId);
                activePlayer.MatchesPlayedIds.Add(match.Id);
            }
        }

        private void removeExchangeAway_Click(object sender, RoutedEventArgs e)
        {
            Exchange selectedExchange = (Exchange)visitorExchangesList.SelectedItem;
            match.VisitorExchanges.Remove(selectedExchange);
            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
            visitorExchangesList.ItemsSource = visitorExchanges;
            Player playerIn = ServiceLocator.Instance.PlayerService.GetBy(selectedExchange.PlayerInId);
            playerIn.MatchesPlayedIds.Remove(match.Id);
        }

        private void SetProperties()
        {
            isPlayedBackup = match.IsPlayed;
            dateBackup = match.Date.Value;
            matchTime = match.MatchTimeInMinutes;
            matchTimeBackup = matchTime;
            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            homeScoreBackup = homeScore;
            visitorScore = match.VisitorGoals.Count();
            visitorScoreBackup = visitorScore;
            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
            homeGoalsBackup = new ObservableCollection<Event>(homeGoals);
            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
            visitorGoalsBackup = new ObservableCollection<Event>(visitorGoals);
            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
            homeAssistsBackup = new ObservableCollection<Event>(homeAssists);
            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
            visitorAssistsBackup = new ObservableCollection<Event>(visitorAssists);
            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
            homeRedCardsBackup = new ObservableCollection<Event>(homeRedCards);
            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
            visitorRedCardsBackup = new ObservableCollection<Event>(visitorRedCards);
            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
            homeYellowCardsBackup = new ObservableCollection<Event>(homeYellowCards);
            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
            visitorYellowCardsBackup = new ObservableCollection<Event>(visitorYellowCards);
            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
            homeLineupBackup = new ObservableCollection<Guid>(homeLineup);
            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
            visitorLineupBackup = new ObservableCollection<Guid>(visitorLineup);
            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
            homeExchangesBackup = new ObservableCollection<Exchange>(homeExchanges);
            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
            visitorExchangesBackup = new ObservableCollection<Exchange>(visitorExchanges);
        }

        private void SetSources()
        {
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

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if ((homeLineup.Count() == 11 && visitorLineup.Count() == 11) && match.IsPlayed)
            {
                homeTeam.GoalsFor = homeTeam.GoalsFor + homeScore - homeScoreBackup;
                visitorTeam.GoalsFor = visitorTeam.GoalsFor + visitorScore - visitorScoreBackup;
                homeTeam.GoalsAgainst = homeTeam.GoalsAgainst + visitorScore - visitorScoreBackup;
                visitorTeam.GoalsAgainst = visitorTeam.GoalsAgainst + homeScore - homeScoreBackup;
                if (homeScore > visitorScore)
                {
                    if(homeTeam.Losses.Contains(match.Id))
                        homeTeam.Losses.Remove(match.Id);
                    if(homeTeam.Ties.Contains(match.Id))
                        homeTeam.Ties.Remove(match.Id);
                    if(visitorTeam.Wins.Contains(match.Id))
                        visitorTeam.Wins.Remove(match.Id);
                    if(visitorTeam.Ties.Contains(match.Id))
                        visitorTeam.Ties.Remove(match.Id);
                    homeTeam.Wins.Add(match.Id);
                    visitorTeam.Losses.Add(match.Id);
                }
                if (homeScore < visitorScore)
                {
                    if (homeTeam.Wins.Contains(match.Id))
                        homeTeam.Wins.Remove(match.Id);
                    if (homeTeam.Ties.Contains(match.Id))
                        homeTeam.Ties.Remove(match.Id);
                    if (visitorTeam.Losses.Contains(match.Id))
                        visitorTeam.Losses.Remove(match.Id);
                    if (visitorTeam.Ties.Contains(match.Id))
                        visitorTeam.Ties.Remove(match.Id);
                    homeTeam.Losses.Add(match.Id);
                    visitorTeam.Wins.Add(match.Id);
                }
                if (homeScore == visitorScore)
                {
                    if (homeTeam.Losses.Contains(match.Id))
                        homeTeam.Losses.Remove(match.Id);
                    if (homeTeam.Wins.Contains(match.Id))
                        homeTeam.Wins.Remove(match.Id);
                    if (visitorTeam.Wins.Contains(match.Id))
                        visitorTeam.Wins.Remove(match.Id);
                    if (visitorTeam.Losses.Contains(match.Id))
                        visitorTeam.Losses.Remove(match.Id);
                    homeTeam.Ties.Add(match.Id);
                    visitorTeam.Ties.Add(match.Id);
                }

                ServiceLocator.Instance.MatchService.Save();
                ServiceLocator.Instance.TeamService.Save();
                ServiceLocator.Instance.PlayerService.Save();
                ServiceLocator.Instance.SerieService.Save();
                Close();
                MessageBox.Show($"Matchprotokoll sparat");
            }
            else if (homeGoals.Count==0 && visitorGoals.Count==0 && homeAssists.Count == 0 && visitorAssists.Count == 0 
                    && homeRedCards.Count == 0 && visitorRedCards.Count == 0 && homeYellowCards.Count == 0 && visitorYellowCards.Count == 0 
                    && homeLineup.Count == 0 && visitorLineup.Count == 0 && homeExchanges.Count == 0 && visitorExchanges.Count == 0
                    && !match.IsPlayed)
            {
                ServiceLocator.Instance.MatchService.Save();
                Close();
                MessageBox.Show($"Matchdatum sparat");
            }
            else
            {
                MessageBox.Show($"En laguppställning måste bestå av 11 spelare");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            match.HomeGoals = homeGoalsBackup.ToList();
            match.VisitorGoals = visitorGoalsBackup.ToList();
            match.HomeAssists = homeAssistsBackup.ToList();
            match.VisitorAssists = visitorAssistsBackup.ToList();
            match.HomeRedCards = homeRedCardsBackup.ToList();
            match.VisitorRedCards = visitorRedCardsBackup.ToList();
            match.HomeYellowCards = homeYellowCardsBackup.ToList();
            match.VisitorYellowCards = visitorYellowCardsBackup.ToList();
            match.HomeLineup = homeLineupBackup.ToHashSet();
            match.VisitorLineup = visitorLineupBackup.ToHashSet();
            match.HomeExchanges = homeExchangesBackup.ToHashSet();
            match.VisitorExchanges = visitorExchangesBackup.ToHashSet();
            isPlayedCheckBox.IsChecked = isPlayedBackup;
            matchDatePicker.SelectedDate = dateBackup;
            match.MatchTimeInMinutes = matchTimeBackup;

            SetProperties();
            SetSources();
            
            Close();
        }

        private void isPlayedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            match.IsPlayed = true;
        }

        private void isPlayedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            match.IsPlayed = false;
        }
    }
}
