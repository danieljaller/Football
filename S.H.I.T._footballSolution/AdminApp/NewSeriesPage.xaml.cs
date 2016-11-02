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
            List<string> teamsListC = new List<string> { "Lag1", "Lag2", "Lag3", "Lag4" };
            teamsList.ItemsSource = teamsListC;
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateMatschScheduleButton_Click(object sender, RoutedEventArgs e)
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
            teamsCheckedList.Items.Refresh();
        }
        
    }
}
