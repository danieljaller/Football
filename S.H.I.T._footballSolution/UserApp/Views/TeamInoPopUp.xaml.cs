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
using System.Windows.Shapes;

namespace UserApp.Views
{
    /// <summary>
    /// Interaction logic for TeamInoPopUp.xaml
    /// </summary>
    public partial class TeamInoPopUp : Window
    {
        public TeamInoPopUp(Team team)
        {
            InitializeComponent();
            mainFrame.Content = new TeamInfoPage();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
