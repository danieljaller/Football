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
        public MatchMinute timeOfEvent;
        IEnumerable<Player> playerInList;
        IEnumerable<Player> playerOutList;


        public AddExchangeWindow(Team team, ObservableCollection<Guid> lineup)
            :this(team, lineup, 90)
        { }

        public AddExchangeWindow(Team team, ObservableCollection<Guid> lineup, int matchLength)
        {
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            InitializeComponent();
            playerInList = teamService.GetAllPlayersByTeam(team.Id).Where(p => !lineup.Contains(p.Id));
            playerOutList = teamService.GetAllPlayersByTeam(team.Id).Where(p => lineup.Contains(p.Id));
            string[] minutes = new string[matchLength];
            for (int i = 1; i <= matchLength; i++)
            {
                minutes[i - 1] = i.ToString();
            }
            playerOutListBox.ItemsSource = playerOutList;
            playerInListBox.ItemsSource = playerInList;
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
