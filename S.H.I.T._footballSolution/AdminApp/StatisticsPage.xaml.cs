using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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
            serieSelector.ItemsSource = ServiceLocator.Instance.SerieService.GetAll();
            if (serieSelector.HasItems)
            {
                serieSelector.SelectedIndex = 0;
                serieName.Text = ((Serie)serieSelector.SelectedItem).Name.Value;
                seriePageFrame.Content = new TablePage((Serie)serieSelector.SelectedItem);
            }
        }

        private void table_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new TablePage((Serie)serieSelector.SelectedItem);
            serieName.Text = ((Serie)serieSelector.SelectedItem).Name.Value;
        }

        private void player_Click(object sender, RoutedEventArgs e)
        {
            seriePageFrame.Content = new PlayerPage((Serie)serieSelector.SelectedItem);
            serieName.Text = ((Serie)serieSelector.SelectedItem).Name.Value;
        }
    }
}