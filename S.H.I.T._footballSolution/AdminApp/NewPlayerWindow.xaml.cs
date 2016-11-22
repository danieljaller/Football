using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public NewPlayerWindow()
        {
            InitializeComponent();
         
            DataContext = this;
            
        }

        private void cancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addContact_Clicked(object sender, RoutedEventArgs e)
        {
            var result = new DatePicker();
            var dateOfBirth = datePicker1.SelectedDate;
          
            Player player = new Player( new PlayerName(FirstName),new PlayerName(LastName), (DateTime)dateOfBirth );
            DialogResult = true;
            Close();
        }
    }
}
