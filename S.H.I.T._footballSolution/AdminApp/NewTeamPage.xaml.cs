﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System.Collections.ObjectModel;

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
        PlayerService _playerService;   
        TeamService _teamService;
        List<Player> listOfPlayers;
        List<Player> listPlayers = new List<Player>();
        bool playersAreValid;
        Team team;

        public NewTeamPage()
        {
            listOfPlayers = new List<Player>();
            InitializeComponent();
            _newPlayerWindow = new NewPlayerWindow();
            _teamService = new TeamService();
            _playerService = new PlayerService(_teamService);
            //TeamName = teamName.Text;
            //ArenaName = arenaName.Text;
            TeamName = "Team";
            ArenaName = "Arena";
            playersList.ItemsSource = new ObservableCollection<Player>(listOfPlayers);
            ToggleCreateTeamButton();
            ToggleNewPlayerButton();


        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var player = _playerService.GetBy(((CheckBox)sender).Content.ToString());
            listPlayers.Add(player);
            playersList.ItemsSource = listPlayers;
            //playersCheckedList.ItemsSource = listPlayers;
            if (_newPlayerWindow.tempPlayersList.Count + _teamService.GetAllPlayersByTeam(team.Id).Count() > 2 && _newPlayerWindow.tempPlayersList.Count + _teamService.GetAllPlayersByTeam(team.Id).Count() < 3)//25 och 31
            {
                playersAreValid = true;
            }
            else
            {
                playersAreValid = false;
            }
            playersList.Items.Refresh();

            ToggleCreateTeamButton();
            ToggleNewPlayerButton();
        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)

        { 
            var player = _playerService.GetBy(((CheckBox)sender).Content.ToString());
            listPlayers.Remove(player);
            playersList.ItemsSource = listPlayers;
            int allPlayers = 0;
            if (team == null)
            {
                allPlayers = 0;
            }
            else
            {
                allPlayers = _teamService.GetAllPlayersByTeam(team.Id).Count();
            }
            if (_newPlayerWindow.tempPlayersList.Count + allPlayers > 2 && _newPlayerWindow.tempPlayersList.Count + allPlayers < 3)//25 och 31
            {
                playersAreValid = true;
            }
            
            else
            {
                playersAreValid = false;
            }
            playersList.Items.Refresh();
           
            ToggleCreateTeamButton();
            ToggleNewPlayerButton();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {

            if (team != null)
            { team = new Team(new GeneralName(TeamName), new GeneralName(ArenaName)); }
            
            var newPlayerWindow = new NewPlayerWindow();
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();
           
            if (newPlayerWindowResult == true)
            {
                foreach (var player in newPlayerWindow.tempPlayersList)
                {
                    listOfPlayers.Add(player);
                    ToggleCreateTeamButton();
                }
            }
            playersList.ItemsSource = new ObservableCollection<Player>(listOfPlayers);
            ToggleCreateTeamButton();
            ToggleNewPlayerButton();
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
        {           if(team != null && playersAreValid)
            { 
            if (_newPlayerWindow.tempPlayersList.Count + _teamService.GetAllPlayersByTeam(team.Id).Count() > 2)//25
            {
                CreateTeamButton.IsEnabled = true;
            }
            }
            else
            {
                CreateTeamButton.IsEnabled = false;
            }
        }


        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (Player item in _newPlayerWindow.tempPlayersList)
            {
                team.PlayerIds.Add(item.Id);
                item.TeamId = team.Id;
                _playerService.Add(item);

            }
            _teamService.Add(team);

        }

    }
}
