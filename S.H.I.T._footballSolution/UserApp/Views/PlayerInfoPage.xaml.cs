using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for PlayerInfoPage.xaml
    /// </summary>
    public partial class PlayerInfoPage : Page
    {        
        public PlayerInfoPage()
        { InitializeComponent(); }
        public PlayerInfoPage(Player _selectedPlayer)
        {
            string teamNameResult = ServiceLocator.Instance.TeamService.GetBy(_selectedPlayer.TeamId).Name.ToString();
            InitializeComponent();
            team.DataContext = teamNameResult;
            age.DataContext = _selectedPlayer.Age;
            dob.DataContext = _selectedPlayer.DateOfBirth;
            goalsMade.DataContext = _selectedPlayer.Goals.Count;
            assists.DataContext = _selectedPlayer.Assists.Count;
            red.DataContext = _selectedPlayer.YellowCards.Count;
            yellow.DataContext = _selectedPlayer.RedCards.Count;
            matchesPlayed.DataContext = _selectedPlayer.MatchesPlayed;
            status.DataContext = _selectedPlayer.PlayerStatus;
        }
    }
}
