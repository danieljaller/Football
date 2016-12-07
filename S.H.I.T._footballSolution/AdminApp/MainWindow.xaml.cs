using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            MainPageFrame.Content = new CreateOrAdministrateSeriesPage(MainPageFrame);
        }

        private void TeamButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new CreateOrAdministrateTeamsPage();
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new StatisticsPage();
        }

        private void PlayerButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new AdministratePlayersPage();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = ServiceLocator.Instance.SearchService.Search(searchTextBox.Text, true, true, true, false, true);

            if (string.IsNullOrWhiteSpace(searchTextBox.Text) || searchResult == null)
            {
                searchResultList.ItemsSource = null;
            }
            else
            {
                searchResultList.ItemsSource = searchResult;
                if (searchResult.Count() == 0)
                {
                    Grid.SetRowSpan(searchResultList, 1);
                }
                else if (searchResult.Count() < 6)
                {
                    Grid.SetRowSpan(searchResultList, searchResult.Count());
                }
                else
                {
                    Grid.SetRowSpan(searchResultList, 5);
                }
            }
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Keyboard.Focus(searchResultList);
                searchResultList.Focus();
                searchResultList.SelectedIndex = 0;
            }
        }

        private void searchResultList_KeyDown(object sender, KeyEventArgs e)
        {
            if (searchResultList.SelectedIndex == 0 && e.Key == Key.Up)
            {
                Keyboard.Focus(searchTextBox);
                searchTextBox.Focus();
            }
            if (e.Key == Key.Return)
            {
                MainPageFrame.Focus();
            }
        }

        private void searchResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchResultList.SelectedItem.GetType() == typeof(Serie))
                {
                    MainPageFrame.Content = new CreateOrAdministrateSeriesPage((Serie)searchResultList.SelectedItem);
                }

                if (searchResultList.SelectedItem.GetType() == typeof(Team))
                {
                    MainPageFrame.Content = new CreateOrAdministrateTeamsPage((Team)searchResultList.SelectedItem);
                }

                if (searchResultList.SelectedItem.GetType() == typeof(Player))
                {
                    MainPageFrame.Content = new AdministratePlayersPage((Player)searchResultList.SelectedItem);
                }
            }
            catch
            {
                MainPageFrame.Content = null;
            }
        }
    }
}