using FootballEngine.Domain.Entities;
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

namespace UserApp
{
    /// <summary>
    /// Interaction logic for SeriePage.xaml
    /// </summary>
    public partial class SeriePage : Page
    {
        Serie serie;

        public SeriePage()
        {
            InitializeComponent();
        }

        public SeriePage(Serie selectedSerie)
        {
            serie = selectedSerie;
            InitializeComponent();
            serieName.DataContext = serie;
        }

        private void schedule_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new SchedulePage(serie);
        }

       
        private void table_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new TablePage();
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new PlayerPage();
        }
    }
}
