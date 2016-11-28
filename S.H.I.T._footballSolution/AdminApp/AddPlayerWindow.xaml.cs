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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        //PlayerService playerService;
        //TeamService teamService;
        public List<Player> selectedPlayers { get; set; }

        public AddPlayerWindow(Team team, ObservableCollection<Guid> lineup)
        {
            //teamService = new TeamService();
            //playerService = new PlayerService(teamService);
            InitializeComponent();
            IEnumerable<Player> playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id));
            playerListbox.ItemsSource = playerList;
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            selectedPlayers = new List<Player>();
            foreach(var player in playerListbox.SelectedItems)
                selectedPlayers.Add((Player)player);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
