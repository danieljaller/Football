using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System.Collections.ObjectModel;
using FootballEngine.Helper;
using System;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for NewTeamPage.xaml
    /// </summary>
    public partial class NewTeamPage : Page
    {
        public string TeamName { get; set; }
        public string ArenaName { get; set; }
        NewPlayerWindow _newPlayerWindow;
        List<Player> listOfPlayers;
        List<Player> listOfPlayersUnChecked;
        bool playersAreValid;
        Team team;

        public NewTeamPage()
        {
            DataContext = this;
            InitializeComponent();
            _newPlayerWindow = new NewPlayerWindow(true);
            listOfPlayers = new List<Player>();
            playersList.ItemsSource = _newPlayerWindow.tempPlayersList;
            ToggleCreateTeamButton();
            ToggleNewPlayerButton();
            listOfPlayersUnChecked = new List<Player>();
            saveTeamArenaNameButton.IsEnabled = false;
            showCreatedTeam.Text = $"";

        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Player sentPlayer = listOfPlayers.Find(p => p.FullName == ((CheckBox)sender).Content.ToString());

            listOfPlayersUnChecked.Add(sentPlayer);
            playersCheckedList.ItemsSource = listOfPlayersUnChecked;

            if (listOfPlayersUnChecked.Count >= 24 && listOfPlayersUnChecked.Count <= 30)
            {
                playersAreValid = true;
            }
            else
            {
                playersAreValid = false;
            }
            playersCheckedList.Items.Refresh();

            ToggleCreateTeamButton();
        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Player sentPlayer = listOfPlayers.Find(p => p.FullName == ((CheckBox)sender).Content.ToString());

            listOfPlayersUnChecked.Remove(sentPlayer);
            playersCheckedList.ItemsSource = listOfPlayersUnChecked;

            if (listOfPlayersUnChecked.Count >= 24 && listOfPlayersUnChecked.Count <= 30)
            {
                playersAreValid = true;
            }
            else
            {
                playersAreValid = false;
            }
            playersCheckedList.Items.Refresh();

            ToggleCreateTeamButton();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (team == null)
            { team = new Team(new GeneralName(TeamName), new GeneralName(ArenaName)); }

            var newPlayerWindow = new NewPlayerWindow(true);
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();

            if (newPlayerWindowResult == true)
            {
                listOfPlayers = newPlayerWindow.tempPlayersList;
            }
            playersList.ItemsSource = new ObservableCollection<Player>(listOfPlayers);
            ToggleCreateTeamButton();
            NewPlayerButton.IsEnabled = false;
        }

        private void ToggleNewPlayerButton()
        {
            if (TeamName != null && ArenaName != null)
            {
                NewPlayerButton.IsEnabled = true;
            }
            else
            {
                NewPlayerButton.IsEnabled = false;
            }
        }

        public void ToggleCreateTeamButton()
        {
            if (playersAreValid)
            {
                CreateTeamButton.IsEnabled = true;
            }
            else
            {
                CreateTeamButton.IsEnabled = false;
            }
        }

        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Player pl in listOfPlayersUnChecked)
            {
                team.PlayerIds.Add(pl.Id);
                pl.TeamId = team.Id;
                ServiceLocator.Instance.PlayerService.Add(pl);
            }
            ServiceLocator.Instance.TeamService.Add(team);
            ServiceLocator.Instance.PlayerService.Save();
            ServiceLocator.Instance.TeamService.Save();
            CreateTeamButton.IsEnabled = false;
            showCreatedTeam.Text = $"Du har lagt till laget {TeamName}";
        }

        private void saveTeamArenaNameButton_Click(object sender, RoutedEventArgs e)
        {
            NewPlayerButton.IsEnabled = true;
            saveTeamArenaNameButton.IsEnabled = false;
        }

        private void Binding_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {

        }

        private bool teamNameIsValid;

        private void teamName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                new GeneralName(teamName.Text);
                teamNameIsValid = true;
            }
            catch (Exception)
            {
                teamNameIsValid = false;
            }

            enable_saveTeamArenaNameButton();
        }

        private bool arenaNameIsValid;

        private void arenaName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                new GeneralName(arenaName.Text);
                arenaNameIsValid = true;
            }
            catch (Exception)
            {
                arenaNameIsValid = false;
            }

            enable_saveTeamArenaNameButton();
        }

        private void enable_saveTeamArenaNameButton()
        {
            if (arenaNameIsValid && teamNameIsValid)
                saveTeamArenaNameButton.IsEnabled = true;
            else
                saveTeamArenaNameButton.IsEnabled = false;
        }
    }
}
