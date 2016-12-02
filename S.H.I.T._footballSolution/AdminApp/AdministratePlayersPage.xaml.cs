using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Domain.ValueObjects;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AdministratePlayersPage.xaml
    /// </summary>
    public partial class AdministratePlayersPage : Page
    {
        Player selectedPlayer;
        string startingFirstName;
        string startingLastName;

        public AdministratePlayersPage()
        {
            InitializeComponent();
            teamsSelector.ItemsSource = ServiceLocator.Instance.TeamService.GetAll().ToList();
            
        }

        public AdministratePlayersPage(Player selectedPlayer)
        {
            InitializeComponent();
            List<Player> player = new List<Player>() { selectedPlayer };
            playersList.ItemsSource = player;
            playerDoBPicker.DisplayDateEnd = DateTime.Today;
        }

        private void teamsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Team selectedTeam = (Team)teamsSelector.SelectedItem;
            playersList.ItemsSource = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(selectedTeam.Id).ToList();
        }

        private void statusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void AllowedDates()
        {
            playerDoBPicker.BlackoutDates.Add(new CalendarDateRange(new DateTime((DateTime.Today.Year - 16), 1, 1), DateTime.Now.AddDays(-1)));
            playerDoBPicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, new DateTime(1950, 1, 1)));
            playerDoBPicker.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, DateTime.MaxValue));
            playerDoBPicker.DisplayDate = new DateTime((DateTime.Today.Year - 17), 1, 1);
        }

        private void SetPlayerStatus()
        {
            selectedPlayer = (Player)playersList.SelectedItem;

            if (statusSelector.SelectedItem == availableCBI)
            {
                selectedPlayer.PlayerStatus = Player.Status.Available;
            }
            if (statusSelector.SelectedItem == suspendedCBI)
            {
                selectedPlayer.PlayerStatus = Player.Status.Suspended;
            }
            if (statusSelector.SelectedItem == injuredCBI)
            {
                selectedPlayer.PlayerStatus = Player.Status.Injured;
            }
            if (statusSelector.SelectedItem == nationalTeamCBI)
            {
                selectedPlayer.PlayerStatus = Player.Status.NationalTeam;
            }
            if (statusSelector.SelectedItem == otherCBI)
            {
                selectedPlayer.PlayerStatus = Player.Status.Other;
            }
        }

        private void playersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startingFirstName = playerFirstName.Text;
            startingLastName = playerLastName.Text;
            EnableControls();
            GetPlayerStatus();
            playerDoBPicker.SelectedDate = selectedPlayer.DateOfBirth.Value;
            AllowedDates();
        }

        private void GetPlayerStatus()
        {
            selectedPlayer = (Player)playersList.SelectedItem;
            switch (selectedPlayer.PlayerStatus)
            {
                case Player.Status.Available:
                    statusSelector.SelectedItem = availableCBI;
                    break;
                case Player.Status.Suspended:
                    statusSelector.SelectedItem = suspendedCBI;
                    break;
                case Player.Status.Injured:
                    statusSelector.SelectedItem = injuredCBI;
                    break;
                case Player.Status.NationalTeam:
                    statusSelector.SelectedItem = nationalTeamCBI;
                    break;
                case Player.Status.Other:
                    statusSelector.SelectedItem = otherCBI;
                    break;
            }
        }

        private void EnableControls()
        {
            playerFirstName.IsEnabled = true;
            playerLastName.IsEnabled = true;
            playerDoBPicker.IsEnabled = true;
            statusSelector.IsEnabled = true;
            saveBtn.IsEnabled = true;
            cancelBtn.IsEnabled = true;
        }

        private void playerFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validation.GetHasError(playerFirstName))
            {
                saveBtn.IsEnabled = false;
            }
            else
            {
                saveBtn.IsEnabled = true;
            }
        }

        private void playerLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Validation.GetHasError(playerLastName))
            {
                saveBtn.IsEnabled = false;
            }
            else
            {
                saveBtn.IsEnabled = true;
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedPlayer.FirstName = new PlayerName(playerFirstName.Text);
            selectedPlayer.LastName = new PlayerName(playerLastName.Text);
            selectedPlayer.DateOfBirth = new DateOfBirth(playerDoBPicker.SelectedDate.Value);
            SetPlayerStatus();
            ServiceLocator.Instance.PlayerService.Save();
            MessageBox.Show("Dina ändringar är sparade");
            playersList.Items.Refresh();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            playerFirstName.Text = startingFirstName;
            playerLastName.Text = startingLastName;
            playerDoBPicker.SelectedDate = selectedPlayer.DateOfBirth.Value;
            GetPlayerStatus();
        }
    }
}
