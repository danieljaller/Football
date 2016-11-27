using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
        public HashSet<Player> tempPlayersList = new HashSet<Player>();
        

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
            DialogResult=true;
            //if(tempPlayersList.Count < 31)
            //{           
            //tempPlayersList.Add(player);
            //}
            //firstName.Text = "";
            //lastName.Text = "";
            //datePicker1.Text = "";
            //if(tempPlayersList == null)
            //{ numberOfPlayers.Text = "0"; }
            //numberOfPlayers.Text = player.Count.ToString();
            ////DialogResult = true;
            //if (datePicker1.SelectedDate == null)
            //{ addPlayerButton.IsEnabled = false; }


            
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
