﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System.Collections.ObjectModel;
using FootballEngine.Helper;

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
       
            listOfPlayers = new List<Player>();          
            _newPlayerWindow = new NewPlayerWindow();            
            playersList.ItemsSource = _newPlayerWindow.tempPlayersList;
            ToggleCreateTeamButton();
            ToggleNewPlayerButton();
            listOfPlayersUnChecked = new List<Player>();
            showCreatedTeam.Text = $"";
        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Player sentPlayer = listOfPlayers.Find(p => p.FullName == ((CheckBox)sender).Content.ToString());

            listOfPlayersUnChecked.Add(sentPlayer);
            playersCheckedList.ItemsSource = listOfPlayersUnChecked;

            if (listOfPlayersUnChecked.Count >= 2 && listOfPlayersUnChecked.Count <= 3)//25 och 31
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

            if (listOfPlayersUnChecked.Count >= 2 && listOfPlayersUnChecked.Count <= 3)//25 och 31
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

            var newPlayerWindow = new NewPlayerWindow();
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
            if (TeamName != null  && ArenaName != null)
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
    }
}
