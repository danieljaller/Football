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
using UserApp.Views;


namespace UserApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SearchService searchService;


        public MainWindow()
        {
            InitializeComponent();
            searchService = new SearchService();
        }

        private void serie_Click(object sender, RoutedEventArgs e)
        {
            if (serieCheckBox.IsChecked == true && teamCheckBox.IsChecked == false && playerCheckBox.IsChecked == false)
            {
                MainPageFrame.Content = new SeriePage();
            }
            if(teamCheckBox.IsChecked == true && serieCheckBox.IsChecked == false && playerCheckBox.IsChecked == false)
            {
                MainPageFrame.Content = new TeamPage();
            }
            if (playerCheckBox.IsChecked == true && serieCheckBox.IsChecked == false && teamCheckBox.IsChecked == false)
            {
                MainPageFrame.Content = new SinglePlayerPage();
            }
        }

        private void team_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new TeamPage();
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new SinglePlayerPage();
        }

        private void playerinfo_Click(object sender, RoutedEventArgs e)
        {
            var matchWindow = new MatchProtocol();
            var showMatch = matchWindow.ShowDialog();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchBox.Text;
            var searchResults = searchService.Search(searchText, true, serieCheckBox.IsChecked == true, teamCheckBox.IsChecked == true, playerCheckBox.IsChecked == true );
            if (searchText.Trim() == "")
            {
                SearchCheckedList.ItemsSource = null;
            }
            else
            {
                SearchCheckedList.ItemsSource = searchResults;
            }
        }
    }
}
