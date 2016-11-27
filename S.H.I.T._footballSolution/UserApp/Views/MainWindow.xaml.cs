using FootballEngine.Domain.Entities;
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
using FootballEngine.Helper;
using UserApp.Views;


namespace UserApp
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

        private void updateSearchCheckList(string searchText)
        { 
            var searchResults = ServiceLocator.Instance.SearchService.Search(searchText, true, playerCheckBox.IsChecked == true, serieCheckBox.IsChecked == true, teamCheckBox.IsChecked == true, true);
            if (searchText.Trim() == "")
            {
                SearchCheckedList.ItemsSource = null;
            }
            else
            {
                SearchCheckedList.ItemsSource = searchResults;
            }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void serieCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void serieCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void teamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var searchText = searchBox.Text;
            updateSearchCheckList(searchText);
        }

        private void SearchCheckedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SearchCheckedList.SelectedItem.GetType() == typeof(Serie))
                {
                    MainPageFrame.Content = new SeriePage((Serie)SearchCheckedList.SelectedItem);
                }

                if (SearchCheckedList.SelectedItem.GetType() == typeof(Team))
                {
                    MainPageFrame.Content = new TeamPage((Team)SearchCheckedList.SelectedItem);
                }

                if (SearchCheckedList.SelectedItem.GetType() == typeof(Player))
                {
                    MainPageFrame.Content = new SinglePlayerPage((Player)SearchCheckedList.SelectedItem);
                }
            }
            catch
            {
                MainPageFrame.Content = null;
            }
        }
    
    }
}
