using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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

namespace UserApp
{
    /// <summary>
    /// Interaction logic for TeamPage.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        Serie serie;
        Team selectedTeam;
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
