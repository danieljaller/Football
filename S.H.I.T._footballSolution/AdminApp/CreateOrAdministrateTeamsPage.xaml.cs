using AdminApp.Converters;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateOrAdministrateTeamsPage.xaml
    /// </summary>
    public partial class CreateOrAdministrateTeamsPage : Page
    {
        Player selectedPlayer;
        Team selectedTeam;
        string backupTeamName;
        string backupArenaName;
        static int selectedIndexInTeamsList;

        public CreateOrAdministrateTeamsPage()
        {
            InitializeComponent();
            teamsList.ItemsSource = ServiceLocator.Instance.TeamService.GetAll();
            teamsList.SelectedIndex = selectedIndexInTeamsList;
        }

        public CreateOrAdministrateTeamsPage(Team selectedTeam)
        {
            InitializeComponent();
            List<Team> searchResultTeam = new List<Team> { selectedTeam };
            teamsList.ItemsSource = searchResultTeam;
            teamsList.SelectedIndex = 0;
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateTeamsPageFrame.Content = new NewTeamPage();
            teamsOverview.Visibility = Visibility.Collapsed;
        }

        private void addPlayer_Click(object sender, RoutedEventArgs e)
        {
            var newPlayerWindow = new NewPlayerWindow(false, selectedTeam);
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();

            if (selectedTeam.PlayerIds.Count() > 24)
                removePlayer.IsEnabled = true;
            if (selectedTeam.PlayerIds.Count() >= 30)
                addPlayer.IsEnabled = false;

            ServiceLocator.Instance.TeamService.Save();
            ServiceLocator.Instance.PlayerService.Save();
            playersList.Items.Refresh();
        }

        private void removePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (playersList.SelectedItem != null)
            {
                selectedPlayer = ServiceLocator.Instance.PlayerService.GetBy((Guid)playersList.SelectedItem);
                MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill ta bort {selectedPlayer.FullName}?", "Bekräfta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    selectedTeam.PlayerIds.Remove(selectedPlayer.Id);
                    ServiceLocator.Instance.PlayerService.Delete(selectedPlayer.Id);
                    ServiceLocator.Instance.TeamService.Save();
                    ServiceLocator.Instance.PlayerService.Save();
                    playersList.Items.Refresh();
                }
                if (selectedTeam.PlayerIds.Count() <= 24)
                    removePlayer.IsEnabled = false;
                if (selectedTeam.PlayerIds.Count() < 30)
                    addPlayer.IsEnabled = true;
            }
        }

        private void teamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndexInTeamsList = teamsList.SelectedIndex;
            backupArenaName = arenaName.Text;
            backupTeamName = teamName.Text;
            selectedTeam = (Team)teamsList.SelectedItem;
            if (selectedTeam.PlayerIds.Count >= 30)
                addPlayer.IsEnabled = false;
            else
                addPlayer.IsEnabled = true;

            matchesPlayedTextBlock.Text = selectedTeam.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == true).Count().ToString();
            StringBuilder serieStringBuilder = new StringBuilder();
            foreach (var serieId in selectedTeam.SerieIds)
            {
                serieStringBuilder.Append($"{ServiceLocator.Instance.SerieService.GetBy(serieId).Name}, ");
            }
            seriesTextBox.Text = serieStringBuilder.ToString().TrimEnd(',', ' ');
            matchesNotPlayedTextBlock.Text = selectedTeam.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == false).Count().ToString();
        }

        private void arenaName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validation.GetHasError(arenaName))
            {
                saveBtn.IsEnabled = false;
            }
            else
            {
                saveBtn.IsEnabled = true;
            }
        }

        private void matchesPlayedButton_Click(object sender, RoutedEventArgs e)
        {
            teamsOverview.Visibility = Visibility.Hidden;
            CreateOrAdministrateTeamsPageFrame.Content = new CreateOrAdministrateSeriesPage((Team)teamsList.SelectedItem, true);
        }

        private void matchesNotPlayedButton_Click(object sender, RoutedEventArgs e)
        {
            teamsOverview.Visibility = Visibility.Hidden;
            CreateOrAdministrateTeamsPageFrame.Content = new CreateOrAdministrateSeriesPage((Team)teamsList.SelectedItem, false);
        }

        private void teamName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validation.GetHasError(teamName))
            {
                saveBtn.IsEnabled = false;
            }
            else
            {
                saveBtn.IsEnabled = true;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedTeam.Name = new GeneralName(teamName.Text);
            selectedTeam.HomeArena = new GeneralName(arenaName.Text);
            ServiceLocator.Instance.TeamService.Save();
            ServiceLocator.Instance.PlayerService.Save();
            MessageBox.Show("Dina ändringar har sparats");
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            teamName.Text = backupTeamName;
            arenaName.Text = backupArenaName;
        }
    }
}
