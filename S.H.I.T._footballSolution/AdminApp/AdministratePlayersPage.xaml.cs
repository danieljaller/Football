using FootballEngine.Domain.Entities;
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
        public AdministratePlayersPage()
        {
       
            InitializeComponent();
            playerDoBPicker.DisplayDateEnd=DateTime.Today;
        }

        public AdministratePlayersPage(Player selectedPlayer)
        {
            InitializeComponent();
            List<Player> player = new List<Player>() {selectedPlayer};
            playersList.ItemsSource = player;
            playerDoBPicker.DisplayDateEnd = DateTime.Today;
        }
    }
}
