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
    /// Interaction logic for PickedSeriesPage.xaml
    /// </summary>
    public partial class NewSeriesPage : Page
    {
        public NewSeriesPage()
        {
            InitializeComponent();
            List<string> teamsListC = new List<string> { "Lag1", "Lag2", "Lag3", "Lag4", "Lag5", "Lag6",
                                                         "Lag7", "Lag8", "Lag9", "Lag10", "Lag11", "Lag12",
                                                         "Lag13", "Lag14", "Lag15", "Lag16", "Lag17", "Lag18",
                                                         "Lag19", "Lag20"};
            teamsList.ItemsSource = teamsListC;
            serieDatePicker.BlackoutDates.AddDatesInPast();
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.Navigate(new Uri("NewTeamPage.xaml", UriKind.Relative));
        }

        private void CreateMatchScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        List<string> tList = new List<string> { };
        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {            
            var team = ((CheckBox)sender).Content;
            tList.Add(team.ToString());
            teamsCheckedList.ItemsSource = tList;
            if (tList.Count == 16)
            {
                CreateMatchScheduleButton.IsEnabled = true;
            }
            if(tList.Count > 16)
            {
                CreateMatchScheduleButton.IsEnabled = false;
            }
            teamsCheckedList.Items.Refresh();
        }

        private void teamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var team = ((CheckBox)sender).Content;
            tList.Remove(team.ToString());
            teamsCheckedList.ItemsSource = tList;
            if (tList.Count < 16)
            {
                CreateMatchScheduleButton.IsEnabled = false;
            }
            if(tList.Count == 16)
            {
                CreateMatchScheduleButton.IsEnabled = true;
            }
            teamsCheckedList.Items.Refresh();
        }
    }
}
