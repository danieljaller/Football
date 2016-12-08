using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Factories;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for PickedSeriesPage.xaml
    /// </summary>
    public partial class NewSeriesPage : Page
    {
        private List<Team> teamList = new List<Team> { };
        private bool teamsAreValid;
        private bool nameIsValid;
        private bool dateIsValid;
        private Frame mainFrame;

        public NewSeriesPage(Frame mainFrame)
        {
            this.mainFrame = mainFrame;
            InitializeComponent();
            teamsList.ItemsSource = ServiceLocator.Instance.TeamService.GetAll().Where(t => t.SerieIds.Count == 0);
            serieDatePicker.BlackoutDates.AddDatesInPast();
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("NewTeamPage.xaml", UriKind.Relative));
        }

        private void CreateMatchScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceLocator.Instance.SerieService.NameExist(serieName.Text))
            {
                MessageBox.Show($"Det finns redan en serie med namnet {serieName.Text}.\nVar god välj ett annat namn.", "Ett fel uppstod");
                serieName.Focus();
            }
            else
            {
                var startDate = Convert.ToDateTime(serieDatePicker.SelectedDate);
                var matchSchedule = MatchTableFactory.CreateMatchTable(teamList, startDate);
                ServiceLocator.Instance.MatchService.AddRange(matchSchedule);
                mainFrame.Content = new CreateSchedulePage(matchSchedule.Select(match => match.Id).ToList(), serieName.Text, teamList, startDate, mainFrame);
            }
        }

        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var team = ServiceLocator.Instance.TeamService.GetBy(((CheckBox)sender).Content.ToString());
            teamList.Add(team);
            teamsCheckedList.ItemsSource = teamList;
            if (teamList.Count == 16)
            {
                teamsAreValid = true;
            }
            if (teamList.Count > 16)
            {
                teamsAreValid = false;
            }
            teamsCheckedList.Items.Refresh();
            ToggleCreateMatchScheduleButton();
        }

        private void teamCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var team = ServiceLocator.Instance.TeamService.GetBy(((CheckBox)sender).Content.ToString());
            teamList.Remove(team);
            teamsCheckedList.ItemsSource = teamList;
            if (teamList.Count < 16)
            {
                teamsAreValid = false;
            }
            if (teamList.Count == 16)
            {
                teamsAreValid = true;
            }
            teamsCheckedList.Items.Refresh();
            ToggleCreateMatchScheduleButton();
        }

        private void serieName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(serieName.Text))
            {
                nameIsValid = false;
            }
            else
            {
                GeneralName result;
                if (GeneralName.TryParse(serieName.Text, out result))
                    nameIsValid = true;
                else
                    nameIsValid = false;
            }
            ToggleCreateMatchScheduleButton();
        }

        private void serieDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serieDatePicker.SelectedDate == null)
            {
                dateIsValid = false;
            }
            else
            {
                dateIsValid = true;
            }
            ToggleCreateMatchScheduleButton();
        }

        private void ToggleCreateMatchScheduleButton()
        {
            if (teamsAreValid && nameIsValid && dateIsValid)
            {
                CreateMatchScheduleButton.IsEnabled = true;
            }
            else
            {
                CreateMatchScheduleButton.IsEnabled = false;
            }
        }
    }
}