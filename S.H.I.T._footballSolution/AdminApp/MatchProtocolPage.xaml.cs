﻿//using FootballEngine.Domain.Entities;
//using FootballEngine.Domain.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using FootballEngine.Helper;

//namespace AdminApp
//{
//    /// <summary>
//    /// Interaction logic for MatchProtocolPage.xaml
//    /// </summary>
//    public partial class MatchProtocolPage : Page
//    {
//        Match match;
//        Team homeTeam;
//        Team visitorTeam;
//        int homeScore;
//        int visitorScore;
//        bool isPlayed;
//        ObservableCollection<Event> homeGoals;
//        ObservableCollection<Event> visitorGoals;
//        ObservableCollection<Event> homeAssists;
//        ObservableCollection<Event> visitorAssists;
//        ObservableCollection<Event> homeRedCards;
//        ObservableCollection<Event> visitorRedCards;
//        ObservableCollection<Event> homeYellowCards;
//        ObservableCollection<Event> visitorYellowCards;
//        ObservableCollection<Guid> homeLineup;
//        ObservableCollection<Guid> visitorLineup;
//        ObservableCollection<Exchange> homeExchanges;
//        ObservableCollection<Exchange> visitorExchanges;
//        ObservableCollection<Event> homeGoalsBackup;
//        ObservableCollection<Event> visitorGoalsBackup;
//        ObservableCollection<Event> homeAssistsBackup;
//        ObservableCollection<Event> visitorAssistsBackup;
//        ObservableCollection<Event> homeRedCardsBackup;
//        ObservableCollection<Event> visitorRedCardsBackup;
//        ObservableCollection<Event> homeYellowCardsBackup;
//        ObservableCollection<Event> visitorYellowCardsBackup;
//        ObservableCollection<Guid> homeLineupBackup;
//        ObservableCollection<Guid> visitorLineupBackup;
//        ObservableCollection<Exchange> homeExchangesBackup;
//        ObservableCollection<Exchange> visitorExchangesBackup;
//        private bool isPlayedBackup;

//        public MatchProtocolPage()
//        {
//            InitializeComponent();
            
//            matchDatePicker.DisplayDateStart = DateTime.Today;
//            matchesList.ItemsSource = ServiceLocator.Instance.MatchService.GetAll(); 
//        }

//        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
//        {
//            match.Date.Value = (DateTime)matchDatePicker.SelectedDate;
//        }

//        private void removeGoalHome_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)homeGoalsList.SelectedItem;
//            match.HomeGoals.Remove(activeEvent);
//            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
//            homeScore--;
//            homeTeamScoreBlock.DataContext = homeScore;
//            homeGoalsList.ItemsSource = homeGoals;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.Goals.Remove(match.Id);
//        }

//        private void addGoalHome_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.HomeGoals.Add(addEventWindow.result);
//                homeGoals = new ObservableCollection<Event>(match.HomeGoals);
//                homeScore++;
//                homeTeamScoreBlock.DataContext = homeScore;
//                homeGoalsList.ItemsSource = homeGoals;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.Goals.Add(match.Id);
//            }
//        }

//        private void addGoalAway_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.VisitorGoals.Add(addEventWindow.result);
//                visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
//                visitorScore++;
//                visitorTeamScoreBlock.DataContext = visitorScore;
//                visitorGoalsList.ItemsSource = visitorGoals;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.Goals.Add(match.Id);
//            }
//        }

//        private void removeGoalAway_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)visitorGoalsList.SelectedItem;
//            match.VisitorGoals.Remove(activeEvent);
//            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
//            visitorScore--;
//            visitorTeamScoreBlock.DataContext = visitorScore;
//            visitorGoalsList.ItemsSource = visitorGoals;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.Goals.Remove(match.Id);
//        }

//        private void removeAssistHome_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)homeAssistsList.SelectedItem;
//            match.HomeAssists.Remove(activeEvent);
//            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
//            homeAssistsList.ItemsSource = homeAssists;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.Assists.Remove(match.Id);
//        }

//        private void addAssistHome_Click(object sender, RoutedEventArgs e)
//        {
//            List<MatchMinute> minutes = match.HomeGoals.Select(g => g.TimeOfEvent).ToList();
//            var addEventWindow = new AddEvent(minutes, homeLineup, homeExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.HomeAssists.Add(addEventWindow.result);
//                homeAssists = new ObservableCollection<Event>(match.HomeAssists);
//                homeAssistsList.ItemsSource = homeAssists;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.Assists.Add(match.Id);
//            }
//        }

//        private void addAssistAway_Click(object sender, RoutedEventArgs e)
//        {
//            List<MatchMinute> minutes = match.VisitorGoals.Select(g => g.TimeOfEvent).ToList();
//            var addEventWindow = new AddEvent(minutes, visitorLineup, visitorExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.VisitorAssists.Add(addEventWindow.result);
//                visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
//                visitorAssistsList.ItemsSource = visitorAssists;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.Assists.Add(match.Id);
//            }
//        }

//        private void removeAssistAway_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)visitorAssistsList.SelectedItem;
//            match.VisitorAssists.Remove(activeEvent);
//            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
//            visitorAssistsList.ItemsSource = visitorAssists;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.Assists.Remove(match.Id);
//        }

//        private void removeRedCardsHome_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)homeRedCardsList.SelectedItem;
//            match.HomeRedCards.Remove(activeEvent);
//            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
//            homeRedCardsList.ItemsSource = homeRedCards;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.RedCards.Remove(match.Id);
//        }

//        private void addRedCardsHome_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.HomeRedCards.Add(addEventWindow.result);
//                homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
//                homeRedCardsList.ItemsSource = homeRedCards;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.RedCards.Add(match.Id);
//            }
//        }

//        private void addRedCardsAway_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.VisitorRedCards.Add(addEventWindow.result);
//                visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
//                visitorRedCardsList.ItemsSource = visitorRedCards;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.RedCards.Add(match.Id);
//            }
//        }

//        private void removeRedCardsAway_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)visitorRedCardsList.SelectedItem;
//            match.VisitorRedCards.Remove(activeEvent);
//            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
//            visitorRedCardsList.ItemsSource = visitorRedCards;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.RedCards.Remove(match.Id);
//        }

//        private void removeYellowCardsHome_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)homeYellowCardsList.SelectedItem;
//            match.HomeYellowCards.Remove(activeEvent);
//            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
//            homeYellowCardsList.ItemsSource = homeYellowCards;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.YellowCards.Remove(match.Id);
//        }

//        private void addYellowCardsHome_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.HomeYellowCards.Add(addEventWindow.result);
//                homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
//                homeYellowCardsList.ItemsSource = homeYellowCards;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.YellowCards.Add(match.Id);
//            }
//        }

//        private void addYellowCardsAway_Click(object sender, RoutedEventArgs e)
//        {
//            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
//            var addEvent = addEventWindow.ShowDialog();
//            if (addEvent == true)
//            {
//                match.VisitorYellowCards.Add(addEventWindow.result);
//                visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
//                visitorYellowCardsList.ItemsSource = visitorYellowCards;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addEventWindow.result.PlayerId);
//                activePlayer.YellowCards.Add(match.Id);
//            }
//        }

//        private void removeYellowCardsAway_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)visitorAssistsList.SelectedItem;
//            match.VisitorYellowCards.Remove(activeEvent);
//            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
//            visitorYellowCardsList.ItemsSource = visitorYellowCards;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.YellowCards.Remove(match.Id);
//        }

//        private void removePlayerHome_Click(object sender, RoutedEventArgs e)
//        {
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy((Guid)homeLineupList.SelectedItem);
//            match.HomeLineup.Remove(activePlayer.Id);
//            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
//            homeLineupList.ItemsSource = homeLineup;
//            activePlayer.MatchesPlayedIds.Remove(match.Id);
//        }

//        private void addPlayerHome_Click(object sender, RoutedEventArgs e)
//        {
//            var addPlayerWindow = new AddPlayerWindow(homeTeam, homeLineup);
//            var addPlayer = addPlayerWindow.ShowDialog();
//            if (addPlayer == true)
//            {
//                foreach (var player in addPlayerWindow.selectedPlayers)
//                {
//                    match.HomeLineup.Add(player.Id);
//                    player.MatchesPlayedIds.Add(match.Id);
//                }
//                homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
//                homeLineupList.ItemsSource = homeLineup;
                
//            }
//        }

//        private void addPlayerAway_Click(object sender, RoutedEventArgs e)
//        {
//            var addPlayerWindow = new AddPlayerWindow(visitorTeam, visitorLineup);
//            var addPlayer = addPlayerWindow.ShowDialog();
//            if (addPlayer == true)
//            {
//                foreach (var player in addPlayerWindow.selectedPlayers)
//                {
//                    match.VisitorLineup.Add(player.Id);
//                    player.MatchesPlayedIds.Add(match.Id);
//                }
//                homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
//                homeLineupList.ItemsSource = homeLineup;
//                visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
//                visitorLineupList.ItemsSource = visitorLineup;
//            }
//        }

//        private void removePlayerAway_Click(object sender, RoutedEventArgs e)
//        {
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy((Guid)visitorLineupList.SelectedItem);
//            match.VisitorLineup.Remove((Guid)visitorLineupList.SelectedItem);
//            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
//            visitorLineupList.ItemsSource = visitorLineup;
//            activePlayer.MatchesPlayedIds.Remove(match.Id);
//        }

//        private void removeExchangeHome_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)homeExchangesList.SelectedItem;
//            match.HomeExchanges.Remove((Exchange)homeExchangesList.SelectedItem);
//            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
//            homeExchangesList.ItemsSource = homeExchanges;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.MatchesPlayedIds.Remove(match.Id);
//        }

//        private void addExchangeHome_Click(object sender, RoutedEventArgs e)
//        {
//            List<Guid> playerOutIds = ServiceLocator.Instance.PlayerService.GetAll()
//                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
//                                                    .Select(p => p.Id).ToList();
//            List<Guid> playerInIds = ServiceLocator.Instance.PlayerService.GetAll()
//                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
//                                                    .Select(p => p.Id).ToList();
//            var addExchangeWindow = new AddExchangeWindow(homeLineup, playerOutIds, playerInIds);
//            var addExchange = addExchangeWindow.ShowDialog();
//            if (addExchange == true)
//            {
//                match.HomeExchanges.Add(addExchangeWindow.result);
//                homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
//                homeExchangesList.ItemsSource = homeExchanges;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addExchangeWindow.result.PlayerInId);
//                activePlayer.MatchesPlayedIds.Add(match.Id);
//            }
//        }

//        private void addExchangeAway_Click(object sender, RoutedEventArgs e)
//        {
//            List<Guid> playerOutIds = (List<Guid>)ServiceLocator.Instance.PlayerService.GetAll()
//                                        .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
//                                        .Select(p => p.Id);
//            List<Guid> playerInIds = (List<Guid>)ServiceLocator.Instance.PlayerService.GetAll()
//                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
//                                                    .Select(p => p.Id);
//            var addExchangeWindow = new AddExchangeWindow(visitorLineup, playerOutIds, playerInIds);
//            var addExchange = addExchangeWindow.ShowDialog();
//            if (addExchange == true)
//            {
//                match.VisitorExchanges.Add(addExchangeWindow.result);
//                visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
//                visitorExchangesList.ItemsSource = visitorExchanges;
//                Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(addExchangeWindow.result.PlayerInId);
//                activePlayer.MatchesPlayedIds.Add(match.Id);
//            }
//        }

//        private void removeExchangeAway_Click(object sender, RoutedEventArgs e)
//        {
//            Event activeEvent = (Event)visitorExchangesList.SelectedItem;
//            match.VisitorExchanges.Remove((Exchange)visitorExchangesList.SelectedItem);
//            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);
//            visitorExchangesList.ItemsSource = visitorExchanges;
//            Player activePlayer = ServiceLocator.Instance.PlayerService.GetBy(activeEvent.PlayerId);
//            activePlayer.MatchesPlayedIds.Remove(match.Id);
//        }

//        private void matchesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            Cancel();
//            convertListsToObjects();
//            isPlayedCheckBox.IsChecked = match.IsPlayed;
//            homeTeamNameBlock.DataContext = homeTeam;
//            visitorTeamNameBlock.DataContext = visitorTeam;
//            if (isPlayed)
//            {
//                homeTeamScoreBlock.DataContext = homeScore;
//                visitorTeamScoreBlock.DataContext = visitorScore;
//            }
//            else
//            {
//                homeTeamScoreBlock.DataContext = " ";
//                visitorTeamScoreBlock.DataContext = " ";    
//            }
//            matchDatePicker.SelectedDate = match.Date.Value;
//            homeGoalsList.ItemsSource = homeGoals;
//            visitorGoalsList.ItemsSource = visitorGoals;
//            homeAssistsList.ItemsSource = homeAssists;
//            visitorAssistsList.ItemsSource = visitorAssists;
//            homeRedCardsList.ItemsSource = homeRedCards;
//            visitorRedCardsList.ItemsSource = visitorRedCards;
//            homeYellowCardsList.ItemsSource = homeYellowCards;
//            visitorYellowCardsList.ItemsSource = visitorYellowCards;
//            homeLineupList.ItemsSource = homeLineup;
//            visitorLineupList.ItemsSource = visitorLineup;
//            homeExchangesList.ItemsSource = homeExchanges;
//            visitorExchangesList.ItemsSource = visitorExchanges;
//        }

//        private void convertListsToObjects()
//        {
//            match = (Match)matchesList.SelectedItem;
//            isPlayed = match.IsPlayed;
//            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
//            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
//            homeScore = match.HomeGoals.Count();
//            visitorScore = match.VisitorGoals.Count();
//            homeGoals = new ObservableCollection<Event>(match.HomeGoals);
//            visitorGoals = new ObservableCollection<Event>(match.VisitorGoals);
//            homeAssists = new ObservableCollection<Event>(match.HomeAssists);
//            visitorAssists = new ObservableCollection<Event>(match.VisitorAssists);
//            homeRedCards = new ObservableCollection<Event>(match.HomeRedCards);
//            visitorRedCards = new ObservableCollection<Event>(match.VisitorRedCards);
//            homeYellowCards = new ObservableCollection<Event>(match.HomeYellowCards);
//            visitorYellowCards = new ObservableCollection<Event>(match.VisitorYellowCards);
//            homeLineup = new ObservableCollection<Guid>(match.HomeLineup);
//            visitorLineup = new ObservableCollection<Guid>(match.VisitorLineup);
//            homeExchanges = new ObservableCollection<Exchange>(match.HomeExchanges);
//            visitorExchanges = new ObservableCollection<Exchange>(match.VisitorExchanges);

//            homeGoalsBackup = new ObservableCollection<Event>(homeGoals);
//            visitorGoalsBackup = new ObservableCollection<Event>(visitorGoals);
//            homeAssistsBackup = new ObservableCollection<Event>(homeAssists);
//            homeRedCardsBackup = new ObservableCollection<Event>(homeRedCards);
//            visitorRedCardsBackup = new ObservableCollection<Event>(visitorRedCards);
//            visitorAssistsBackup = new ObservableCollection<Event>(visitorAssists);
//            homeYellowCardsBackup = new ObservableCollection<Event>(homeYellowCards);
//            visitorYellowCardsBackup = new ObservableCollection<Event>(visitorYellowCards);
//            homeLineupBackup = new ObservableCollection<Guid>(homeLineup);
//            visitorLineupBackup = new ObservableCollection<Guid>(visitorLineup);
//            homeExchangesBackup = new ObservableCollection<Exchange>(homeExchanges);
//            visitorExchangesBackup = new ObservableCollection<Exchange>(visitorExchanges);
//        }

//        private void okButton_Click(object sender, RoutedEventArgs e)
//        {
//            if ((homeLineup.Count() == 11 && visitorLineup.Count() == 11) || !match.IsPlayed)
//            {
//                homeTeam.GoalsFor = homeTeam.GoalsFor + homeScore;
//                visitorTeam.GoalsFor = visitorTeam.GoalsFor + visitorScore;
//                homeTeam.GoalsAgainst = homeTeam.GoalsAgainst + visitorScore;
//                visitorTeam.GoalsAgainst = visitorTeam.GoalsAgainst + homeScore;
//                if (homeScore > visitorScore)
//                {
//                    homeTeam.Wins++;
//                    visitorTeam.Losses++;
//                }
//                if (homeScore < visitorScore)
//                {
//                    homeTeam.Losses++;
//                    visitorTeam.Wins++;
//                }
//                if (homeScore == visitorScore)
//                {
//                    homeTeam.Ties++;
//                    visitorTeam.Ties++;
//                }

//                ServiceLocator.Instance.MatchService.Save();
//                ServiceLocator.Instance.PlayerService.Save();
//                ServiceLocator.Instance.SerieService.Save();
//                ServiceLocator.Instance.TeamService.Save();

//                isPlayedBackup = match.IsPlayed;
//                homeGoalsBackup = homeGoals;
//                visitorGoalsBackup = visitorGoals;
//                homeAssistsBackup = homeAssists;
//                homeRedCardsBackup = homeRedCards;
//                visitorRedCardsBackup = visitorRedCards;
//                visitorAssistsBackup = visitorAssists;
//                homeYellowCardsBackup = homeYellowCards;
//                visitorYellowCardsBackup = visitorYellowCards;
//                homeLineupBackup = homeLineup;
//                visitorLineupBackup = visitorLineup;
//                homeExchangesBackup = homeExchanges;
//                visitorExchangesBackup = visitorExchanges;
//                MessageBox.Show($"Matchprotokoll sparat");
//            }
//            else
//            {
//                MessageBox.Show($"En laguppställning måste bestå av 11 spelare");
//            }
//        }

//        private void cancelButton_Click(object sender, RoutedEventArgs e)
//        {
//            Cancel();
//        }

//        private void isPlayedCheckBox_Checked(object sender, RoutedEventArgs e)
//        {
//            match.IsPlayed = true;
//        }

//        private void isPlayedCheckBox_Unchecked(object sender, RoutedEventArgs e)
//        {
//            match.IsPlayed = false;
//        }

//        public void Cancel()
//        {
//            if (match != null)
//            {
//                match.HomeGoals = homeGoalsBackup.ToList();
//                match.VisitorGoals = visitorGoalsBackup.ToList();
//                match.HomeAssists = homeAssistsBackup.ToList();
//                match.VisitorAssists = visitorAssistsBackup.ToList();
//                match.HomeRedCards = homeRedCardsBackup.ToList();
//                match.VisitorRedCards = visitorRedCardsBackup.ToList();
//                match.HomeYellowCards = homeYellowCardsBackup.ToList();
//                match.VisitorYellowCards = visitorYellowCardsBackup.ToList();
//                match.HomeLineup = homeLineupBackup.ToHashSet();
//                match.VisitorLineup = visitorLineupBackup.ToHashSet();
//                match.HomeExchanges = homeExchangesBackup.ToHashSet();
//                match.VisitorExchanges = visitorExchangesBackup.ToHashSet();
//                isPlayedCheckBox.IsChecked = isPlayedBackup;

//                convertListsToObjects();

//                homeGoalsList.ItemsSource = homeGoals;
//                visitorGoalsList.ItemsSource = visitorGoals;
//                homeAssistsList.ItemsSource = homeAssists;
//                visitorAssistsList.ItemsSource = visitorAssists;
//                homeRedCardsList.ItemsSource = homeRedCards;
//                visitorRedCardsList.ItemsSource = visitorRedCards;
//                homeYellowCardsList.ItemsSource = homeYellowCards;
//                visitorYellowCardsList.ItemsSource = visitorYellowCards;
//                homeLineupList.ItemsSource = homeLineup;
//                visitorLineupList.ItemsSource = visitorLineup;
//                homeExchangesList.ItemsSource = homeExchanges;
//                visitorExchangesList.ItemsSource = visitorExchanges;
//                if (isPlayed)
//                {
//                    homeTeamScoreBlock.DataContext = homeScore;
//                    visitorTeamScoreBlock.DataContext = visitorScore;
//                }
//                else
//                {
//                    homeTeamScoreBlock.DataContext = " ";
//                    visitorTeamScoreBlock.DataContext = " ";
//                }
//            }
//        }
//    }
//}
