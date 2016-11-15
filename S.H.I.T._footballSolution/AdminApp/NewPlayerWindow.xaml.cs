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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewPlayerWindow : Window
    {
        public NewPlayerWindow()
        {
            InitializeComponent();
        }

        private void cancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addContact_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
