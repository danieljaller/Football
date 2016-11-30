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

        public CreateOrAdministrateTeamsPage()
        {
            InitializeComponent();

            teamsList.ItemsSource = ServiceLocator.Instance.TeamService.GetAll();
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
        }

        private void teamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var team = (Team)teamsList.SelectedItem;
            if(team.PlayerIds.Count() >= 30)
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
        }

        private void arenaName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var team = (Team)teamsList.SelectedItem;
            try
            {
                team.HomeArena = new GeneralName(arenaName.Text);
                arenaName.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            catch (Exception ex)
            {
                arenaName.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
