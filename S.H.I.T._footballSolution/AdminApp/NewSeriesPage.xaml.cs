using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for PickedSeriesPage.xaml
    /// </summary>
    public partial class NewSeriesPage : Page
    {
        TeamService teamService;
        SerieService serieService;
        List<Team> teamList = new List<Team> { };
        bool teamsAreValid;
        bool nameIsValid;
        bool dateIsValid;

        public NewSeriesPage()
        {
            InitializeComponent();
            teamService = new TeamService();
            serieService = new SerieService();
            teamsList.ItemsSource = teamService.GetAll();
            serieDatePicker.BlackoutDates.AddDatesInPast();
        }

        private void NewTeamButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("NewTeamPage.xaml", UriKind.Relative));
        }

        private void CreateMatchScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var teamIds = teamList.Select(x => x.Id).ToList();
            var matchSchedule = SerieAndMatchGenerator.SerieGenerator(teamIds, Convert.ToDateTime(serieDatePicker.SelectedDate));
            var newSerie = new Serie(new GeneralName(serieName.Text), teamIds, matchSchedule);
            serieService.Add(newSerie);
            foreach (var team in teamList)
            {
                team.SeriesIds.Add(newSerie.Id);
            }
            serieService.Save();
            teamService.Save();
        }

        private void teamCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var team = teamService.GetBy(((CheckBox)sender).Content.ToString());
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
            var team = teamService.GetBy(((CheckBox)sender).Content.ToString());
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
