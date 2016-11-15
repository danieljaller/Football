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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void serieCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void serieCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void teamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void playerCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void playerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void doesSomething_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Content = new TeamPage();
        }
    }
}
