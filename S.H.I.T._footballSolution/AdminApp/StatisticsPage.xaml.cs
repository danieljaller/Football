using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
            serieSelector.ItemsSource = ServiceLocator.Instance.SerieService.GetAll();
        }

        //private Serie GetSelectedSerie()
        //{
            
        //}

        private void schedule_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new SchedulePage();
        }


        private void table_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new TablePage((Serie)serieSelector.SelectedItem);
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new PlayerPage((Serie)serieSelector.SelectedItem);
        }
        
    }
}
