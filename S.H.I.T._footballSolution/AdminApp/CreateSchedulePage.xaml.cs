using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
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
using FootballEngine.Helper;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateSchedulePage.xaml
    /// </summary>
    public partial class CreateSchedulePage : Page
    {
        private string serieName;
        private List<Guid> matchScheduleWithIds = new List<Guid>();
        private List<Team> teamList = new List<Team>();
        private List<Match> matchScheduleWithMatches = new List<Match>();
        private List<Team> homeTeamList = new List<Team>();
        private List<Team> visitorTeamList = new List<Team>();
        private bool datePickerIsDisabled;

        public CreateSchedulePage(List<Guid> matchSchedule, string serieName, List<Team> teamList)
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
            Serie newSerie = new Serie(new GeneralName(serieName), teamList.Select(t => t.Id).ToList(), matchScheduleWithIds);
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
