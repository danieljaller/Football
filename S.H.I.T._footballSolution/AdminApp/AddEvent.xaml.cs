using FootballEngine.Domain.Entities;
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
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        public Player player;
        public Team team;
        IEnumerable<Player> playerList;
        public AddEvent(Team team)
        {      
            InitializeComponent();
            playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id);
            playerListbox.ItemsSource = playerList;
            
        }
    }
}
