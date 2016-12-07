using FootballEngine.Domain.Entities;
using System.Windows;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for TeamInoPopUp.xaml
    /// </summary>
    public partial class TeamInoPopUp : Window
    {
        public TeamInoPopUp(Team team)
        {
            InitializeComponent();
            mainFrame.Content = new TeamInfoPage(team);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}