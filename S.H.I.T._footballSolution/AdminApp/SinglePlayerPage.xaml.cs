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
    /// Interaction logic for SinglePlayerPage.xaml
    /// </summary>
    public partial class SinglePlayerPage : Page
    {
        public SinglePlayerPage()
        {
            InitializeComponent();
        }

        private void team_Click(object sender, RoutedEventArgs e)
        {
            singlePlayerPageFrame.Content = new TeamInfoPage();
        }

        private void playerInfo_Click(object sender, RoutedEventArgs e)
        {
            singlePlayerPageFrame.Content = new PlayerInfoPage();
        }
    }
}
