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
        List<string> testList = new List<string> { "Lag1", "Lag2", "Spelare1", "Spelare2", "Serie1", "Serie2", "Match1", "Match2" };
        public MainWindow()
        {
            InitializeComponent();
            SearchBox.MaxDropDownHeight = 0;
            SearchBox.Loaded += new RoutedEventHandler(SearchBox_Loaded);
            SearchBox.DropDownClosed += new EventHandler(SearchBox_DropDownClosed);
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
        private void SearchBox_DropDownClosed(object sender, EventArgs e)
        {
            if (SearchBox.Text != string.Empty)
            {
                TextBox textBox = SearchBox.Template.FindName("PART_EditableTextBox", SearchBox) as TextBox;

                if (!SearchBox.Items.Contains(textBox.Text) && SearchBox.SelectedIndex < 0)
                {
                    textBox.Clear();
                }
            }

        }

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            SearchBox.ApplyTemplate();
            TextBox textBox = SearchBox.Template.FindName("PART_EditableTextBox", SearchBox) as TextBox;
            textBox.SelectionLength = 0;

            if (textBox != null)
            {
                textBox.TextChanged += delegate
                {
                    SearchBox.IsDropDownOpen = true;
                    SearchBox.SelectedIndex = -1;
                    SearchBox.Items.Filter += a =>
                    {
                        if (a.ToString().ToLower().Contains(textBox.Text.ToLower()))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    };

                    textBox.SelectionLength = 0;
                    textBox.CaretIndex = textBox.Text.Length;
                };
            }
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (SearchBox.Text.Length == 0)
            {
                SearchBox.IsDropDownOpen = false;
                SearchBox.ItemsSource = null;
                SearchBox.MaxDropDownHeight = 0;
            }
            else
            {
                SearchBox.ItemsSource = testList; //Lägg till itemsource här!!
                SearchBox.MaxDropDownHeight = 200;
            }
        }
    }
}
