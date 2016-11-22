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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for NewTeamPage.xaml
    /// </summary>
    public partial class NewTeamPage : Page
    {
        public string TeamName { get; set; }
        public string ArenaName { get; set; }
        NewPlayerWindow _newPlayerWindow;
        PlayerService _playerService;
        TeamService _teamService;
        public NewTeamPage()
        {
            InitializeComponent();
            _newPlayerWindow = new NewPlayerWindow();
            _teamService = new TeamService();
            _playerService = new PlayerService(_teamService);
        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            //skicka med ett lag, en checkbox selecterad
            var newPlayerWindow = new NewPlayerWindow();
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();
            if (_newPlayerWindow.tempPlayersList.Count > 30)      
            { NewPlayerButton.IsEnabled = false;     }    
        }


        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team(new GeneralName(TeamName), new GeneralName(ArenaName));
            foreach (Player item in _newPlayerWindow.tempPlayersList)
            {
                team.PlayerIds.Add(item.Id);
                item.TeamId = team.Id;
                _playerService.Add(item);                                      
            }
            _teamService.Add(team);

        }
    }
}
