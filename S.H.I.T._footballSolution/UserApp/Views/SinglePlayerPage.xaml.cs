using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Windows;
using System.Windows.Controls;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for SinglePlayerPage.xaml
    /// </summary>
    public partial class SinglePlayerPage : Page
    {
        Player selectedPlayer;
        public SinglePlayerPage()
        {
            InitializeComponent();
        }

        public SinglePlayerPage(Player _selectedPlayer)
        {
            selectedPlayer = _selectedPlayer;
            InitializeComponent();
            singlePlayerName.DataContext = selectedPlayer;
            singlePlayerPageFrame.Content = new PlayerInfoPage(selectedPlayer);
        }

        private void playerInfo_Click(object sender, RoutedEventArgs e)
        {
            singlePlayerPageFrame.Content = new PlayerInfoPage(selectedPlayer);
        }

        private void teamInfo_Click(object sender, RoutedEventArgs e)
        {
            Team team = ServiceLocator.Instance.TeamService.GetBy(selectedPlayer.TeamId);
            singlePlayerPageFrame.Content = new TeamInfoPage(team);
        }
    }
}
