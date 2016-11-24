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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        PlayerService playerService;
        TeamService teamService;
        public Player player;
        public Team team;
        public Event result { get; set; }
        public uint timeOfEvent;
        IEnumerable<Player> playerList;
        public AddEvent(Team team)
        {
            teamService = new TeamService();
            playerService = new PlayerService(teamService);        
            InitializeComponent();
            playerList = teamService.GetAllPlayersByTeam(team.Id);
            playerListbox.ItemsSource = playerList;           
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            player = (Player)playerListbox.SelectedItem;          
            uint.TryParse(timeBox.Text, out timeOfEvent);
            result = new Event(player.Id, timeOfEvent);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
