using FootballEngine.Domain.Entities;
using System.Windows;
using System.Windows.Controls;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for SeriePage.xaml
    /// </summary>
    public partial class SeriePage : Page
    {
        private Serie serie;

        public SeriePage()
        {
            InitializeComponent();
        }

        public SeriePage(Serie selectedSerie)
        {
            serie = selectedSerie;
            InitializeComponent();
            serieName.DataContext = serie;
            seriePageFrame.Content = new SchedulePage(serie);
        }

        private void schedule_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new SchedulePage(serie);
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new TablePage(serie);
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new PlayerPage(serie);
        }
    }
}