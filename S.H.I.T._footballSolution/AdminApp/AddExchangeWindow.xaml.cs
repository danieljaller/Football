using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddExchangeWindow.xaml
    /// </summary>
    public partial class AddExchangeWindow : Window
    {
        public Exchange Result { get; set; }
        public MatchMinute TimeOfEvent;

        public AddExchangeWindow(ObservableCollection<Guid> lineup, IEnumerable<Guid> playerOutIds, IEnumerable<Guid> playerInIds)
            :this(lineup, 90, playerOutIds, playerInIds)
        { }

        public AddExchangeWindow(ObservableCollection<Guid> lineup, int matchLength, IEnumerable<Guid> playerOutIds, IEnumerable<Guid> playerInIds)
        {
            InitializeComponent();
            //List<Guid> unexchangeablePlayerIds = new List<Guid>();
            //unexchangeablePlayerIds.AddRange(playerOutIds.Concat(playerInIds));
            Guid teamId = ServiceLocator.Instance.PlayerService.GetBy(lineup[0]).TeamId;
            IEnumerable<Player> playersInTeam = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(teamId);
            List<Player> activePlayers = new List<Player>();
            List<Player> availablePlayers = new List<Player>();
            foreach (Player player in playersInTeam)
            {
                if ((lineup.Contains(player.Id) || playerInIds.Contains(player.Id)) && !playerOutIds.Contains(player.Id))
                    activePlayers.Add(player);
                else if (player.Playable && !playerInIds.Contains(player.Id))
                    availablePlayers.Add(player);
            }
            //var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            //var activePlayers = allPlayers.Where(p => (lineup.Contains(p.Id) || playerInIds.Contains(p.Id)) 
            //                                                    && !playerOutIds.Contains(p.Id));
            //var availablePlayers = allPlayers.Where(p => !lineup.Contains(p.Id) && !playerInIds.Contains(p.Id) && p.Playable);

            string[] minutes = new string[matchLength];
            for (int i = 1; i <= matchLength; i++)
            {
                minutes[i - 1] = i.ToString();
            }
            playerOutListBox.ItemsSource = activePlayers;
            playerInListBox.ItemsSource = availablePlayers;
            timeBox.ItemsSource = minutes;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Player playerIn = (Player)playerInListBox.SelectedItem;
            Player playerOut = (Player)playerOutListBox.SelectedItem;
            MatchMinute.TryParse(timeBox.SelectedIndex + 1, out TimeOfEvent);
            Result = new Exchange(playerOut.Id, playerIn.Id, TimeOfEvent);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void timeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeBox.IsDropDownOpen = false;
        }
    }
}
