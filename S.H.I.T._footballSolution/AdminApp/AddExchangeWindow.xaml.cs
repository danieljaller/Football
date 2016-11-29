using FootballEngine.Domain.Entities;
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
    /// Interaction logic for AddExchangeWindow.xaml
    /// </summary>
    public partial class AddExchangeWindow : Window
    {
        //PlayerService playerService;
        //TeamService teamService;
        public Exchange result { get; set; }
        public MatchMinute timeOfEvent;
        IEnumerable<Player> playerInList;
        IEnumerable<Player> playerOutList;


        public AddExchangeWindow(ObservableCollection<Guid> lineup, IEnumerable<Guid> PlayerOutIds, IEnumerable<Guid> PlayerInIds)
            :this(lineup, 90, PlayerOutIds, PlayerInIds)
        { }

        public AddExchangeWindow(ObservableCollection<Guid> lineup, int matchLength, IEnumerable<Guid> playerOutIds, IEnumerable<Guid> playerInIds)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            var activePlayers = allPlayers.Where(p => (lineup.Contains(p.Id) || playerInIds.Contains(p.Id)) 
                                                                && !playerOutIds.Contains(p.Id));
            var availablePlayers = allPlayers.Where(p => !lineup.Contains(p.Id) && !playerInIds.Contains(p.Id) && p.Playable);

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
            MatchMinute.TryParse(timeBox.SelectedIndex + 1, out timeOfEvent);
            result = new Exchange(playerOut.Id, playerIn.Id, timeOfEvent);
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
