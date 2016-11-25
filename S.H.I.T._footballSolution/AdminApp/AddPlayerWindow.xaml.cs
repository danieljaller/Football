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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        PlayerService playerService;
        TeamService teamService;
        public Player player { get; set; }
        public Team team;
        IEnumerable<Player> playerList;

        public AddPlayerWindow(Team team, ObservableCollection<Guid> lineup)
        {
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            InitializeComponent();
            playerList = teamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id));
            playerListbox.ItemsSource = playerList;
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            player = (Player)playerListbox.SelectedItem;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
