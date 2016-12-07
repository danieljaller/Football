using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UserApp.Views;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        bool isTeamClicked, isPointsClicked, isGoalDifferenceClicked, isWinsClicked, isLossesClicked;
        bool isTiesClicked, isGoalsForClicked, isGoalsAgainstClicked, isMatchesPlayedClicked;
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
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isTiesClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isTeamClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByTeamName(selectedSerie.Id);
                    isTeamClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByTeamName(selectedSerie.Id).Reverse();
                    isTeamClicked = false;
                }
            }
        }

        private void points_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isGoalDifferenceClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isTiesClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isPointsClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByPoints(selectedSerie.Id);
                    isPointsClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByPoints(selectedSerie.Id).Reverse();
                    isPointsClicked = false;
                }
            }
        }

        private void goalDifference_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isTiesClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isGoalDifferenceClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByGoalDifference(selectedSerie.Id);
                    isGoalDifferenceClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByGoalDifference(selectedSerie.Id).Reverse();
                    isGoalDifferenceClicked = false;
                }
            }
        }

        private void wins_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isLossesClicked = false;
                isTiesClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isWinsClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfWins(selectedSerie.Id);
                    isWinsClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfWins(selectedSerie.Id).Reverse();
                    isWinsClicked = false;
                }
            }
        }

        private void ties_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isLossesClicked = false;
                isWinsClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isTiesClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfTies(selectedSerie.Id);
                    isTiesClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfTies(selectedSerie.Id).Reverse();
                    isTiesClicked = false;
                }
            }
        }

        private void losses_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isTiesClicked = false;
                isWinsClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isLossesClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfLosses(selectedSerie.Id);
                    isLossesClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfLosses(selectedSerie.Id).Reverse();
                    isLossesClicked = false;
                }
            }
        }

        private void goalsFor_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isTiesClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isGoalsAgainstClicked = false;
                isMatchesPlayedClicked = false;

                if (!isGoalsForClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsFor(selectedSerie.Id);
                    isGoalsForClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsFor(selectedSerie.Id).Reverse();
                    isGoalsForClicked = false;
                }
            }
        }

        private void goalsAgainst_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isTiesClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isGoalsForClicked = false;
                isMatchesPlayedClicked = false;

                if (!isGoalsAgainstClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsAgainst(selectedSerie.Id);
                    isGoalsAgainstClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfGoalsAgainst(selectedSerie.Id).Reverse();
                    isGoalsAgainstClicked = false;
                }
            }
        }

        private void matchesPlayed_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isPointsClicked = false;
                isGoalDifferenceClicked = false;
                isTiesClicked = false;
                isWinsClicked = false;
                isLossesClicked = false;
                isGoalsForClicked = false;
                isGoalsAgainstClicked = false;

                if (!isMatchesPlayedClicked)
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfMatchesPlayed(selectedSerie.Id);
                    isMatchesPlayedClicked = true;
                }
                else
                {
                    tableStatsListbox.ItemsSource = ServiceLocator.Instance.TeamService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse();
                    isMatchesPlayedClicked = false;
                }
            }
        }
        private void tableStatsListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Team selectedTeam = (Team)tableStatsListbox.SelectedItem;
            var infoPopUp = new TeamInoPopUp(selectedTeam);
            infoPopUp.ShowDialog();
        }
    }


}

