using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
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

namespace UserApp
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {

        private HashSet<Team> teamList = new HashSet<Team>();
        private HashSet<Guid> matchScheduleWithIds;
        private HashSet<Match> matchScheduleWithMatches;
        private List<Team> homeTeamList, visitorTeamList;
        private Serie SelectedSerie;

        public SchedulePage(Serie selectedSerie)
        {
            InitializeComponent();
            SelectedSerie = selectedSerie;
            matchScheduleWithIds = SelectedSerie.MatchTable;
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
            showAllRadioButton.IsChecked = true;
        }

        private void SetItemSources(HashSet<Match> matchScheduleWithMatches, List<Team> homeTeamList, List<Team> visitorTeamList)
        {
            homeTeamListBox.ItemsSource = homeTeamList;
            visitorTeamListBox.ItemsSource = visitorTeamList;
            dateListBox.ItemsSource = matchScheduleWithMatches;
            resultListBox.ItemsSource = matchScheduleWithMatches;
        }

        private void CreateAndConvertLists(HashSet<Guid> matchScheduleWithIds, out HashSet<Match> matchScheduleWithMatches, out List<Team> homeTeamList, out List<Team> visitorTeamList)
        {
            matchScheduleWithMatches = new HashSet<Match>();
            homeTeamList = new List<Team>();
            visitorTeamList = new List<Team>();

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

        private void showAllRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            matchScheduleWithIds = SelectedSerie.MatchTable;
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void showPlayedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            matchScheduleWithIds = SelectedSerie.MatchTable.Where(m => ServiceLocator.Instance.MatchService.GetBy(m).IsPlayed == true).ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }


        private void showCommingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            matchScheduleWithIds = SelectedSerie.MatchTable.Where(m => ServiceLocator.Instance.MatchService.GetBy(m).IsPlayed == false).ToHashSet();
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }
        
        private void HomeTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            matchScheduleWithIds = ServiceLocator.Instance.MatchService.OrderByHomeTeam(matchScheduleWithIds);
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void VisitorTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            matchScheduleWithIds = ServiceLocator.Instance.MatchService.OrderByVisitorTeam(matchScheduleWithIds);
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }

        private void Date_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            matchScheduleWithIds = ServiceLocator.Instance.MatchService.OrderByDate(matchScheduleWithIds);
            CreateAndConvertLists(matchScheduleWithIds, out matchScheduleWithMatches, out homeTeamList, out visitorTeamList);
            SetItemSources(matchScheduleWithMatches, homeTeamList, visitorTeamList);
        }
    }
}
