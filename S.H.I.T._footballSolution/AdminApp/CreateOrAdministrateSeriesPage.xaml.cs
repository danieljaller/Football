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
    /// Interaction logic for CreateOrAdministrateSeries.xaml
    /// </summary>
    public partial class CreateOrAdministrateSeriesPage : Page
    {
        public CreateOrAdministrateSeriesPage()
        {
            InitializeComponent();
            List<string> teamList = new List<string> { "Serie1", "Serie2", "Serie3", "Serie4" };
            seriesList.ItemsSource = teamList;
        }

        private void NewSeriesButton_Click(object sender, RoutedEventArgs e)
        {

            CreateOrAdministrateSeriesPageFrame.Content = new NewSeriesPage();
            matchSchedule.Visibility = Visibility.Collapsed;
        }
    }
}
