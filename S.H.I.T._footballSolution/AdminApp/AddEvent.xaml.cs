using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        //PlayerService playerService;
        //TeamService teamService;
        public Event result { get; set; }
        public MatchMinute timeOfEvent;
        IEnumerable<Player> playerList;

        public AddEvent(ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
            :this(90, lineup, exchanges)
        {
        }

        public AddEvent(int matchLength, ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);        
            InitializeComponent();
            var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            playerList = allPlayers.Where(p => (lineup.Contains(p.Id) || exchanges.Select(ex => ex.PlayerInId).Contains(p.Id)));
            List<MatchMinute> minutes = new List<MatchMinute>();
            for(int i=1; i<=matchLength; i++)
            {
                MatchMinute min = new MatchMinute();
                min.Value = i;
                minutes.Add(min);
            }           
            playerListbox.ItemsSource = playerList;
            timeBox.ItemsSource = minutes;
        }

        public AddEvent(List<MatchMinute> matchMinutes, ObservableCollection<Guid> lineup, ObservableCollection<Exchange> exchanges)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            playerList = allPlayers.Where(p => (lineup.Contains(p.Id) || exchanges.Select(ex => ex.PlayerInId).Contains(p.Id)));
            playerListbox.ItemsSource = playerList;
            timeBox.ItemsSource = matchMinutes;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)playerListbox.SelectedItem;
            timeOfEvent = (MatchMinute)timeBox.SelectedItem;          
            result = new Event(player.Id, timeOfEvent);
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
