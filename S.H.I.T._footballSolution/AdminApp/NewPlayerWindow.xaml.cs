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
        public Player player { get; set; }
        public List<Player> tempPlayersList = new List<Player>();
        

        public NewPlayerWindow()
        {
            
            InitializeComponent();
            DataContext = this;
            if (datePicker1.SelectedDate == null)
            { addPlayerButton.IsEnabled = false; }
            
            
        }

        private void cancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            
         DateOfBirth = (DateTime)datePicker1.SelectedDate;
            player = new Player(new PlayerName(FirstName), new PlayerName(LastName), DateOfBirth);
            

            if (tempPlayersList.Count < 3)//31
            {
                tempPlayersList.Add(player);
            }
            firstName.Text = "";
            lastName.Text = "";
            datePicker1.Text = "";
            if (tempPlayersList == null)
            { numberOfPlayers.Text = "0"; }
            numberOfPlayers.Text = tempPlayersList.Count.ToString();
            
            if (datePicker1.SelectedDate == null)
            { addPlayerButton.IsEnabled = false; }

            if (tempPlayersList.Count > 2)//24
            { DialogResult = true; }

        }

        private void closingNewPlayerWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void datePicker_selectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            addPlayerButton.IsEnabled = true;
        }
    }
}
