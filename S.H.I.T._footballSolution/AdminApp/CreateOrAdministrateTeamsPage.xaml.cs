using FootballEngine.Domain.Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            HashSet<string> teamsListC = new HashSet<string> { "Lag1", "Lag2", "Lag3", "Lag4", "Lag5", "Lag6",
                                                         "Lag7", "Lag8", "Lag9", "Lag10", "Lag11", "Lag12",
                                                         "Lag13", "Lag14", "Lag15", "Lag16", "Lag17", "Lag18",
                                                         "Lag19", "Lag20"};
            teamsList.ItemsSource = teamsListC;
        }

        public CreateOrAdministrateTeamsPage(Team selectedTeam)
        {
            InitializeComponent();
            HashSet<string> teamsListC = new HashSet<string> {selectedTeam.Name.Value};
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
