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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddExchangeWindow.xaml
    /// </summary>
    public partial class AddExchangeWindow : Window
    {
        PlayerService playerService;
        TeamService teamService;
        public Exchange result { get; set; }
        public uint timeOfEvent;
        IEnumerable<Player> playerInList;
        IEnumerable<Player> playerOutList;


        public AddExchangeWindow(Team team, ObservableCollection<Guid> lineup)
        {
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            InitializeComponent();
            playerInList = teamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id));
            playerOutList = teamService.GetAllPlayersByTeam(team.Id).Where(p => lineup.Contains(p.Id));
            playerOutListBox.ItemsSource = playerOutList;
            playerInListBox.ItemsSource = playerInList;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Player playerIn = (Player)playerInListBox.SelectedItem;
            Player playerOut = (Player)playerOutListBox.SelectedItem;
            uint.TryParse(timeBox.Text, out timeOfEvent);
            result = new Exchange(playerOut.Id, playerIn.Id, timeOfEvent);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
