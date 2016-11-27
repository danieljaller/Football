using System.Windows;
using System.Windows.Controls;


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
        }

        private void schedule_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new SchedulePage();
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
