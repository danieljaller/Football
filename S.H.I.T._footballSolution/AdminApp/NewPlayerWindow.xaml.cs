using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
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
        public bool hasMinValue;
        public Team team;

        public NewPlayerWindow(bool hasMinValue)
        {
            InitializeComponent();
            this.hasMinValue = hasMinValue;
            DataContext = this;
            AllowedDates();
        }

        public NewPlayerWindow(bool hasMinValue, Team team)
        {
            InitializeComponent();
            this.team = team;
            this.hasMinValue = hasMinValue;
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
            player.TeamId = team.Id;

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
            if (hasMinValue)
            {
                if (tempPlayersList.Count < 24)
                {
                    addPlayersNowButton.IsEnabled = false;
                }
                if (tempPlayersList.Count >= 24)
                {
                    addPlayersNowButton.IsEnabled = true;
                }

                if (tempPlayersList.Count >= 30)
                {
                    DialogResult = true;
                }
            }
            else
            {
                if (tempPlayersList.Count < 1)
                    addPlayersNowButton.IsEnabled = false;
                else
                    addPlayersNowButton.IsEnabled = true;
            }
        }

        private void addPlayersNow_Clicked(object sender, RoutedEventArgs e)
        {
            if (!hasMinValue)
            {
                ServiceLocator.Instance.PlayerService.AddRange(tempPlayersList);
                foreach (var player in tempPlayersList)
                {
                    team.PlayerIds.Add(player.Id);
                }
            }

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
