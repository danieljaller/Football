using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AdministratePlayersPage.xaml
    /// </summary>
    public partial class AdministratePlayersPage : Page
    {
        public AdministratePlayersPage()
        {
       
            InitializeComponent();
            playerDoBPicker.DisplayDateEnd=DateTime.Today;
        }

        public AdministratePlayersPage(Player selectedPlayer)
        {
            InitializeComponent();
            HashSet<Player> player = new HashSet<Player>() {selectedPlayer};
            playersList.ItemsSource = player;
            playerDoBPicker.DisplayDateEnd = DateTime.Today;
        }
    }
}
