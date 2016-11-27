using FootballEngine.Domain.Entities;
using System.Collections.Generic;
using System.Windows;
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
        HashSet<Player> playerList;
        public AddEvent(Team team)
        {      
            InitializeComponent();
            playerList = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(team.Id).ToHashSet();
            playerListbox.ItemsSource = playerList;
            
        }
    }
}
