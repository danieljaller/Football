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
        Player player;
        Team team;
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
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateTeamsPageFrame.Content = new NewTeamPage();
            teamsOverview.Visibility = Visibility.Collapsed;
        }

        private void addPlayer_Click(object sender, RoutedEventArgs e)
        {
            var newPlayerWindow = new NewPlayerWindow(false, (Team)teamsList.SelectedItem);
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();

            if (team.PlayerIds.Count() > 24)
                removePlayer.IsEnabled = true;
            if (team.PlayerIds.Count() >= 30)
                addPlayer.IsEnabled = false;
            ServiceLocator.Instance.TeamService.Save();
            ServiceLocator.Instance.PlayerService.Save();
            playersList.Items.Refresh();
        }

        private void removePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (playersList.SelectedItem != null)
            {
                player = ServiceLocator.Instance.PlayerService.GetBy((Guid)playersList.SelectedItem);
                team = ServiceLocator.Instance.TeamService.GetBy(player.TeamId);
                MessageBoxResult result = MessageBox.Show($"Är du säker på att du vill ta bort {player.FullName}?", "Bekräfta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    team.PlayerIds.Remove(player.Id);
                    ServiceLocator.Instance.PlayerService.Delete(player.Id);
                    playersList.Items.Refresh();
                }
                if (team.PlayerIds.Count() <= 24)
                    removePlayer.IsEnabled = false;
                if (team.PlayerIds.Count() < 30)
                    addPlayer.IsEnabled = true;

            }
            ServiceLocator.Instance.TeamService.Save();
            ServiceLocator.Instance.PlayerService.Save();
        }

        private void teamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedIndexInTeamsList = teamsList.SelectedIndex;
            var team = (Team)teamsList.SelectedItem;
            if (team.PlayerIds.Count() >= 30)
            {
                addPlayer.IsEnabled = false;
            }
            matchesPlayedTextBlock.Text = team.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == true).Count().ToString();
            StringBuilder serieStringBuilder = new StringBuilder();
            foreach (var serieId in team.SerieIds)
            {
                serieStringBuilder.Append($"{ServiceLocator.Instance.SerieService.GetBy(serieId).Name}, ");
            }
            seriesTextBox.Text = serieStringBuilder.ToString().TrimEnd(',', ' ');
            matchesNotPlayedTextBlock.Text = team.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == false).Count().ToString();
        }

        private void arenaName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var team = (Team)teamsList.SelectedItem;

            GeneralName result;
            if (GeneralName.TryParse(arenaName.Text, out result))
            {
                team.HomeArena = result;
                ServiceLocator.Instance.TeamService.Save();
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
            var team = (Team)teamsList.SelectedItem;

            GeneralName result;
            if (GeneralName.TryParse(teamName.Text, out result))
            {
                team.Name = result;
                ServiceLocator.Instance.TeamService.Save();
            }
        }
    }
}
