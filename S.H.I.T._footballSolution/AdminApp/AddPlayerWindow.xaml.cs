using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        //PlayerService playerService;
        //TeamService teamService;
        public List<Player> selectedPlayers { get; set; }

        private int NumberOfSelectedPlayers;

        public AddPlayerWindow(Team team, ObservableCollection<Guid> lineup, int numberOfSelectedPlayers)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            NumberOfSelectedPlayers = numberOfSelectedPlayers;
            IEnumerable<Player> playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id) && p.Playable);
            playerListbox.ItemsSource = playerList;
            InitializeComponent();
            playerCountBlock.DataContext = playerListbox.SelectedItems.Count + NumberOfSelectedPlayers;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            selectedPlayers = new List<Player>();
            foreach (var player in playerListbox.SelectedItems)
                selectedPlayers.Add((Player)player);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void playerListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            playerCountBlock.DataContext = playerListbox.SelectedItems.Count + NumberOfSelectedPlayers;
        }
    }
}