﻿using FootballEngine.Domain.Entities;
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
        Team homeTeam;
        Team visitorTeam;
        int homeScore;
        int visitorScore;
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
        ObservableCollection<Event> homeGoalsBackup;
        ObservableCollection<Event> visitorGoalsBackup;
        ObservableCollection<Event> homeAssistsBackup;
        ObservableCollection<Event> visitorAssistsBackup;
        ObservableCollection<Event> homeRedCardsBackup;
        ObservableCollection<Event> visitorRedCardsBackup;
        ObservableCollection<Event> homeYellowCardsBackup;
        ObservableCollection<Event> visitorYellowCardsBackup;
        ObservableCollection<Guid> homeLineupBackup;
        ObservableCollection<Guid> visitorLineupBackup;
        ObservableCollection<Exchange> homeExchangesBackup;
        ObservableCollection<Exchange> visitorExchangesBackup;
        private bool isPlayedBackup;
        private DateTime dateBackup;


        public MatchProtocol(Match _match)
        {
            match = _match;
            InitializeComponent();
            ConvertListsToObjects();

            if(match.Date.Value >= DateTime.Today)
                isPlayedCheckBox.IsEnabled = false;

            isPlayedCheckBox.IsChecked = match.IsPlayed;
            matchDatePicker.SelectedDate = match.Date.Value;
            homeGoalsList.ItemsSource = match.HomeGoals;
            visitorGoalsList.ItemsSource = match.VisitorGoals;
            homeAssistsList.ItemsSource = match.HomeAssists;
            visitorAssistsList.ItemsSource = match.VisitorAssists;
            homeExchangesList.ItemsSource = match.HomeExchanges;
            visitorExchangesList.ItemsSource = match.VisitorExchanges;
            homeLineupList.ItemsSource = match.HomeLineup;
            visitorLineupList.ItemsSource = match.VisitorLineup;
            homeRedCardsList.ItemsSource = match.HomeRedCards;
            visitorRedCardsList.ItemsSource = match.VisitorRedCards;
            homeYellowCardsList.ItemsSource = match.HomeYellowCards;
            visitorRedCardsList.ItemsSource = match.VisitorYellowCards;
        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            match.Date.Value = (DateTime)matchDatePicker.SelectedDate;

            if (match.Date.Value < DateTime.Today)
                isPlayedCheckBox.IsEnabled = true;
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
            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
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
            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
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
            List<MatchMinute> minutes = match.HomeGoals.Select(g => g.TimeOfEvent).ToList();
            var addEventWindow = new AddEvent(minutes, homeLineup, homeExchanges);
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
            List<MatchMinute> minutes = match.HomeGoals.Select(g => g.TimeOfEvent).ToList();
            var addEventWindow = new AddEvent(minutes, visitorLineup, visitorExchanges);
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
            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
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
            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
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
            var addEventWindow = new AddEvent(homeLineup, homeExchanges);
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
            var addEventWindow = new AddEvent(visitorLineup, visitorExchanges);
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
                foreach (Player player in addPlayerWindow.selectedPlayers)
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
                foreach (Player player in addPlayerWindow.selectedPlayers)
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
            var playerOutIds = ServiceLocator.Instance.PlayerService.GetAll()
                                                     .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
                                                     .Select(p => p.Id);
            var playerInIds = ServiceLocator.Instance.PlayerService.GetAll()
                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
                                                    .Select(p => p.Id);
            var addExchangeWindow = new AddExchangeWindow(homeLineup, playerOutIds, playerInIds);
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
            var playerOutIds = ServiceLocator.Instance.PlayerService.GetAll()
                                        .Where(p => match.HomeExchanges.Select(ex => ex.PlayerOutId).Contains(p.Id))
                                        .Select(p => p.Id);
            var playerInIds = ServiceLocator.Instance.PlayerService.GetAll()
                                                    .Where(p => match.HomeExchanges.Select(ex => ex.PlayerInId).Contains(p.Id))
                                                    .Select(p => p.Id);
            var addExchangeWindow = new AddExchangeWindow(visitorLineup, playerOutIds, playerInIds);
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

        private void ConvertListsToObjects()
        {
            isPlayedBackup = match.IsPlayed;
            dateBackup = match.Date.Value;
            homeTeam = ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId);
            visitorTeam = ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId);
            homeScore = match.HomeGoals.Count();
            visitorScore = match.VisitorGoals.Count();
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

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if ((homeLineup.Count() == 11 && visitorLineup.Count() == 11) || !match.IsPlayed)
            {
                ServiceLocator.Instance.MatchService.Save();
                ServiceLocator.Instance.TeamService.Save();
                ServiceLocator.Instance.PlayerService.Save();
                ServiceLocator.Instance.SerieService.Save();
                Close();
                MessageBox.Show($"Matchprotokoll sparat");
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

            ConvertListsToObjects();

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
