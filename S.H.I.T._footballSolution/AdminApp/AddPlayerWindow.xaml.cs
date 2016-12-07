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
        public List<Player> selectedPlayers { get; set; }

        private int NumberOfSelectedPlayers;

        public AddPlayerWindow(Team team, ObservableCollection<Guid> lineup, int numberOfSelectedPlayers)
        {
            InitializeComponent();
            NumberOfSelectedPlayers = numberOfSelectedPlayers;
            IEnumerable<Player> playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id) && p.Playable);
            playerListbox.ItemsSource = playerList;
            InitializeComponent();
            playerCountBlock.DataContext = playerListbox.SelectedItems.Count + NumberOfSelectedPlayers;
            Ok.IsEnabled = false;
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
            int playerCount = playerListbox.SelectedItems.Count + NumberOfSelectedPlayers;
            playerCountBlock.DataContext = playerCount;
            if (playerCount == 11)
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }
    }
}