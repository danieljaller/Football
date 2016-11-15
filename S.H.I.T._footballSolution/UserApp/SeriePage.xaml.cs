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
        public SeriePage()
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
