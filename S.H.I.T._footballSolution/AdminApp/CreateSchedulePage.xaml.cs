using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateSchedulePage.xaml
    /// </summary>
    public partial class CreateSchedulePage : Page
    {
        private string serieName;
        private HashSet<Guid> matchScheduleWithIds = new HashSet<Guid>();
        private HashSet<Team> teamList = new HashSet<Team>();
        private HashSet<Match> matchScheduleWithMatches = new HashSet<Match>();
        private HashSet<Team> homeTeamList = new HashSet<Team>();
        private HashSet<Team> visitorTeamList = new HashSet<Team>();
        private bool datePickerIsDisabled;

        public CreateSchedulePage(HashSet<Guid> matchSchedule, string serieName, HashSet<Team> teamList)
        {
            InitializeComponent();
            matchScheduleWithIds = matchSchedule;
            this.serieName = serieName;
            this.teamList = teamList;
            ConvertFromGuid();
            homeTeamListBox.ItemsSource = homeTeamList;
            visitorTeamListBox.ItemsSource = visitorTeamList;
            dateListBox.ItemsSource = matchScheduleWithMatches;
            resultListBox.ItemsSource = matchScheduleWithMatches;

        }
        public void ConvertFromGuid()
        {
            foreach (var matchId in matchScheduleWithIds)
            {
                matchScheduleWithMatches.Add(ServiceLocator.Instance.MatchService.GetBy(matchId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                homeTeamList.Add(ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId));
            }
            foreach (var match in matchScheduleWithMatches)
            {
                visitorTeamList.Add(ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId));
            }
        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Match)dateListBox.SelectedItem;
            var datePicker = (DatePicker)sender;
            selectedItem.Date.EditMatchDate(Convert.ToDateTime(datePicker.SelectedDate));

        }

        private void createSerieButton_Click(object sender, RoutedEventArgs e)
        {
            Serie newSerie = new Serie(new GeneralName(serieName), teamList.Select(t => t.Id).ToHashSet(), matchScheduleWithIds.ToHashSet());
            ServiceLocator.Instance.SerieService.Add(newSerie);
            foreach (var team in teamList)
            {
                team.SerieIds.Add(newSerie.Id);
            }
            ServiceLocator.Instance.SerieService.Save();
            ServiceLocator.Instance.TeamService.Save();
            ServiceLocator.Instance.MatchService.Save();
            MessageBox.Show($"Serien {serieName} är skapad");
            createSerieButton.IsEnabled = false;
            datePickerIsDisabled = true;

        }

        private void matchDatePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            if (datePickerIsDisabled)
            {
                var datePicker = (DatePicker)sender;
                datePicker.IsEnabled = false;
            }
        }
    }
}
