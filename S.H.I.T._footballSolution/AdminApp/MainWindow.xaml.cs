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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SeriesButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new CreateOrAdministrateSeriesPage();
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new CreateOrAdministrateTeamsPage();
        }

        private void ProtocolButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new MatchProtocolPage();
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new StatisticsPage();
        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new AdministratePlayersPage();
        }
    }
}
