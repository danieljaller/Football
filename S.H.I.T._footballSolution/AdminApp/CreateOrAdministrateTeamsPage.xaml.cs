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
    /// Interaction logic for CreateOrAdministrateTeamsPage.xaml
    /// </summary>
    public partial class CreateOrAdministrateTeamsPage : Page
    {
        public CreateOrAdministrateTeamsPage()
        {
            InitializeComponent();
            List<string> teamsListC = new List<string> { "Lag1", "Lag2", "Lag3", "Lag4", "Lag5", "Lag6",
                                                         "Lag7", "Lag8", "Lag9", "Lag10", "Lag11", "Lag12",
                                                         "Lag13", "Lag14", "Lag15", "Lag16", "Lag17", "Lag18",
                                                         "Lag19", "Lag20"};
            teamsList.ItemsSource = teamsListC;
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            CreateOrAdministrateTeamsPageFrame.Content = new NewTeamPage();
            teamsOverview.Visibility = Visibility.Collapsed;
        }

        private void addPlayer_Click(object sender, RoutedEventArgs e)
        {
            var newPlayerWindow = new NewPlayerWindow();
            var newPlayerWindowResult = newPlayerWindow.ShowDialog();
        }

        private void removePlayer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
