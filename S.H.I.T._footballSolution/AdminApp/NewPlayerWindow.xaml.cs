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
        public List<Player> tempPlayersList = new List<Player>();
       

        public NewPlayerWindow()
        {
            InitializeComponent();
            DataContext = this;
                   
            AllowedDates();
        }

        private void cancel_Clicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void addPlayer_Clicked(object sender, RoutedEventArgs e)
        {
            DateOfBirth = (DateTime)datePicker1.SelectedDate;
            player = new Player(new PlayerName(FirstName), new PlayerName(LastName), new DateOfBirth(DateOfBirth));

            if (tempPlayersList.Count <= 30)
            {
                tempPlayersList.Add(player);
            }
            firstName.Text = "";
            lastName.Text = "";
            datePicker1.Text = "";
            if (tempPlayersList == null)
            { numberOfPlayers.Text = "0"; }
            numberOfPlayers.Text = tempPlayersList.Count.ToString();

            if (tempPlayersList.Count < 24)
            {
                addPlayersNowButton.IsEnabled = false;
            }
            if (tempPlayersList.Count >= 24)
            {
                addPlayersNowButton.IsEnabled = true;
            }

            if (tempPlayersList.Count >=30)
            {               
                DialogResult = true;
            }
        }

        private void addPlayersNow_Clicked(object sender, RoutedEventArgs e)
        {            
            DialogResult = true;
        }

        private void AllowedDates()
        {
            datePicker1.BlackoutDates.Add(new CalendarDateRange(new DateTime((DateTime.Today.Year - 16), 1, 1), DateTime.Now.AddDays(-1)));
            datePicker1.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, new DateTime(1950, 1, 1)));
            datePicker1.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, DateTime.MaxValue));
            datePicker1.DisplayDate = new DateTime((DateTime.Today.Year - 17), 1, 1);           
        }
        

        private void closingNewPlayerWindow(object sender, System.ComponentModel.CancelEventArgs e)
        { }

        
    }
}
