using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for PlayerPage.xaml
    /// </summary>
    ///
    public partial class PlayerPage : Page
    {
        private Serie selectedSerie;
        private ObservableCollection<Player> players;
        private bool isNameClicked, isTeamClicked, isAgeClicked, isMatchesPlayedClicked, isGoalsClicked;
        private bool isRedCardsClicked, isYellowCardsClicked, isStatusClicked, isAssistsClicked;

        public PlayerPage()
        {
        }

        public PlayerPage(Serie selectedSerie)
        {
            this.selectedSerie = selectedSerie;
            InitializeComponent();
            SetPlayerData(selectedSerie);
        }

        private void SetPlayerData(Serie selectedSerie)
        {
            if (selectedSerie != null)
            {
                players = new ObservableCollection<Player>(ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id));
                playerStatsListbox.ItemsSource = players;
            }
        }

        private void playerName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isTeamClicked = false;
                isAgeClicked = false;
                isMatchesPlayedClicked = false;
                isRedCardsClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isNameClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id);
                    isNameClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id).Reverse();
                    isNameClicked = false;
                }
            }
        }

        private void playerTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isAgeClicked = false;
                isMatchesPlayedClicked = false;
                isRedCardsClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isTeamClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id);
                    isTeamClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id).Reverse();
                    isTeamClicked = false;
                }
            }
        }

        private void playerAge_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isMatchesPlayedClicked = false;
                isRedCardsClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isAgeClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id);
                    isAgeClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id).Reverse();
                    isAgeClicked = false;
                }
            }
        }

        private void playerMatchesPlayed_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isRedCardsClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isMatchesPlayedClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id);
                    isMatchesPlayedClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse();
                    isMatchesPlayedClicked = false;
                }
            }
        }

        private void playerGoals_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isRedCardsClicked = false;
                isMatchesPlayedClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isGoalsClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id);
                    isGoalsClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id).Reverse();
                    isGoalsClicked = false;
                }
            }
        }

        private void playerAssists_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isGoalsClicked = false;
                isRedCardsClicked = false;
                isMatchesPlayedClicked = false;
                isYellowCardsClicked = false;
                isStatusClicked = false;

                if (!isAssistsClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id);
                    isAssistsClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id).Reverse();
                    isAssistsClicked = false;
                }
            }
        }

        private void playerYellowCards_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isGoalsClicked = false;
                isRedCardsClicked = false;
                isMatchesPlayedClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isYellowCardsClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id);
                    isYellowCardsClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id).Reverse();
                    isYellowCardsClicked = false;
                }
            }
        }

        private void playerRedCards_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isMatchesPlayedClicked = false;
                isStatusClicked = false;
                isAssistsClicked = false;

                if (!isRedCardsClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id);
                    isRedCardsClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id).Reverse();
                    isRedCardsClicked = false;
                }
            }
        }

        private void playerStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                isNameClicked = false;
                isTeamClicked = false;
                isAgeClicked = false;
                isGoalsClicked = false;
                isYellowCardsClicked = false;
                isMatchesPlayedClicked = false;
                isRedCardsClicked = false;
                isAssistsClicked = false;

                if (!isStatusClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id);
                    isStatusClicked = true;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id).Reverse();
                    isStatusClicked = false;
                }
            }
        }
    }
}