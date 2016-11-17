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
        List<string> testList = new List<string> { "Item1", "Item2", "Item3", "Thing1", "Thing2", "Thing3", "Sak1", "Sak2", "Sak3" };
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
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchResult = testList.Where(i => i.ToLower().Contains(searchTextBox.Text.ToLower()));
            
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
            if(searchResultList.SelectedIndex == 0 && e.Key == Key.Up)
            {
                Keyboard.Focus(searchTextBox);
                searchTextBox.Focus();
            }
            if(e.Key == Key.Return)
            {
                MainPageFrame.Focus();
            }
        }
    }
}