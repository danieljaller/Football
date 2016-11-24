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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for CreateSchedulePage.xaml
    /// </summary>
    public partial class CreateSchedulePage : Page
    {
        MatchService matchService = new MatchService();
        TeamService teamService = new TeamService();
        private List<Guid> matchSchedule = new List<Guid>();
        private List<Match> matchList = new List<Match>();
        private List<Team> homeTeamList = new List<Team>();
        private List<Team> visitorTeamList = new List<Team>();

        public CreateSchedulePage(List<Guid> matchSchedule)
        {
            InitializeComponent();
            this.matchSchedule = matchSchedule;
            ConvertGuidList();
            homeTeamListBox.ItemsSource = homeTeamList;
            visitorTeamListBox.ItemsSource = visitorTeamList;
            dateListBox.ItemsSource = matchList;
            resultListBox.ItemsSource = matchList;
            
        }
        public void ConvertGuidList()
        {
            foreach(var matchId in matchSchedule)
            {
                matchList.Add(matchService.GetBy(matchId));
            }
            foreach(var match in matchList)
            {
                homeTeamList.Add(teamService.GetBy(match.HomeTeamId));
            }
            foreach(var match in matchList)
            {
                visitorTeamList.Add(teamService.GetBy(match.VisitorTeamId));
            }
        }

        private void GenerateGridRowsAndSetRowColor()
        {
            foreach (var match in matchSchedule)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto, MinHeight = 20 });
            }

            for (int i = 2; i < grid.RowDefinitions.Count(); i += 2)
            {
                Rectangle rect = new Rectangle();
                rect.Fill = new SolidColorBrush(Colors.LightGray);
                grid.Children.Add(rect);
                Grid.SetColumnSpan(rect, 9);
                Grid.SetRow(rect, i);
            }
        }

        private void matchDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Match)dateListBox.SelectedItem;
            var datePicker = (DatePicker)sender;
            selectedItem.Date.EditMatchDate(Convert.ToDateTime(datePicker.SelectedDate));
            //Visibility = Visibility.Hidden;
            
        }
    }
}
