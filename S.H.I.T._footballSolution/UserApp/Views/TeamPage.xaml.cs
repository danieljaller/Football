using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        private Serie serie;
        private Team selectedTeam;

        public TeamPage()
        {
            InitializeComponent();
        }

        public TeamPage(Team selectedTeam)
        {
            this.selectedTeam = selectedTeam;
            serie = ServiceLocator.Instance.SerieService.GetBy(selectedTeam.SerieIds.First());
            InitializeComponent();
            teamName.DataContext = selectedTeam;
            teamPageFrame.Content = new TeamInfoPage(selectedTeam, teamPageFrame);
        }

        private void teaminfo_Click(object sender, RoutedEventArgs e)
        {
            teamPageFrame.Content = new TeamInfoPage(selectedTeam, teamPageFrame);
        }

        private void schedule_Click(object sender, RoutedEventArgs e)
        {
            teamPageFrame.Content = new SchedulePage(serie, selectedTeam);
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            teamPageFrame.Content = new TablePage(serie);
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            teamPageFrame.Content = new PlayerPage(serie, selectedTeam);
        }
    }
}