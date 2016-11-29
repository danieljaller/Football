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

        public AddEvent(Team team)
            :this(team, 90)
        {
        }

        public AddEvent(Team team, int matchLength)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);        
            InitializeComponent();
            playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id);
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

        public AddEvent(Team team, List<MatchMinute> matchMinutes)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id);
            playerListbox.ItemsSource = playerList;
            timeBox.ItemsSource = matchMinutes;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)playerListbox.SelectedItem;
            MatchMinute.TryParse(timeBox.SelectedIndex+1, out timeOfEvent);          
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
