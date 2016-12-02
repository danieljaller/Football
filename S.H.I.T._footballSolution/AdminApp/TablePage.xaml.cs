using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        bool isMouseLeftButtonDown;
        Serie selectedSerie;
        ObservableCollection<Team> teams;
        public TablePage()
        { }
        public TablePage(Serie selectedSerie)
        {
            this.selectedSerie = selectedSerie;
            InitializeComponent();
            SetTableData(selectedSerie);
        }

        private void SetTableData(Serie selectedSerie)
        {
            if (selectedSerie != null)
            {
                teams = new ObservableCollection<Team>(ServiceLocator.Instance.TeamService.OrderByPoints(selectedSerie.Id));
                tableStatsListbox.ItemsSource = teams;                
            }

        }

        private void team_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByTeamName(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByTeamName(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void points_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByPoints(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByPoints(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void goalDifference_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByGoalDifference(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByGoalDifference(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void wins_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfWins(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfWins(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void ties_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfTies(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfTies(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void losses_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfLosses(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfLosses(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void goalsMade_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsFor(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsFor(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void goalsReceived_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsAgainst(selectedSerie.Id);
                    isMouseLeftButtonDown = false;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsAgainst(selectedSerie.Id).Reverse();
                    isMouseLeftButtonDown = true;
                }
            }
        }

        private void numberOfMatches_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseLeftButtonDown)
            {
                tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfMatchesPlayed(selectedSerie.Id);
                isMouseLeftButtonDown = false;
            }
            else
            {
                tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse();
                isMouseLeftButtonDown = true;
            }
                }
        }
    }
}
