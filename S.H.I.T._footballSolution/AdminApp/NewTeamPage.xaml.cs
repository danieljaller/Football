using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System.Collections.ObjectModel;
using FootballEngine.Helper;

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
        HashSet<Player> listOfPlayers;
        public NewTeamPage()
        {
            listOfPlayers = new HashSet<Player>();
            InitializeComponent();
            _newPlayerWindow = new NewPlayerWindow();


            playersList.ItemsSource = new ObservableCollection<Player>(listOfPlayers);
            ToggleCreateTeamButton();
            
           
        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ToggleCreateTeamButton();            
        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleCreateTeamButton();
        }

        private void NewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            //skicka med ett lag, en checkbox selecterad
            var newPlayerWindow = new NewPlayerWindow();
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();
            if (newPlayerWindowResult == true)
            { listOfPlayers.Add(newPlayerWindow.player); }
            playersList.ItemsSource = new ObservableCollection<Player>(listOfPlayers);
            ToggleCreateTeamButton();
        }

        private void ToggleCreateTeamButton()
        {
            
            if (listOfPlayers.Count() >= 2)
            {
                CreateTeamButton.IsEnabled = true;
            }
            else
            {
                CreateTeamButton.IsEnabled = false;
            }
        }


        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Team team = new Team(new GeneralName(TeamName), new GeneralName(ArenaName));
            foreach (Player player in _newPlayerWindow.tempPlayersList)
            {
                team.PlayerIds.Add(player.Id);
                player.TeamId = team.Id;
                ServiceLocator.Instance.PlayerService.Add(player);
               
            }
            ServiceLocator.Instance.TeamService.Add(team);

        }
        
    }
}
