using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FootballEngine.Domain.Entities;
using FootballEngine.Helper;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for PlayerPage.xaml
    /// </summary>
    public partial class PlayerPage : Page
    {
        Serie selectedSerie;
        ObservableCollection<Player> players;
        bool isNameClicked, isTeamClicked, isAgeClicked, isMatchesPlayedClicked, isGoalsClicked;
        bool isRedCardsClicked, isYellowCardsClicked, isStatusClicked, isAssistsClicked;
        private Team _team;

        public PlayerPage()
        { }
        public PlayerPage(Serie selectedSerie)
        {
            this.selectedSerie = selectedSerie;
            InitializeComponent();
            SetPlayerData(selectedSerie);
        }
        public PlayerPage(Serie selectedSerie, Team team = null)
        {
            this.selectedSerie = selectedSerie;
            _team = team;
            InitializeComponent();
            SetPlayerData(selectedSerie);
        }

        private void SetPlayerData(Serie selectedSerie)
        {
            if (selectedSerie != null && _team != null)
            {
                players = new ObservableCollection<Player>(ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id)
                    .Where(p => p.TeamId == _team.Id));
                playerStatsListbox.ItemsSource = players;
            }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id)
                          .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id);
                    }
                    isNameClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFullName(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id);
                    }
                    isTeamClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id);
                    }
                    isAgeClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id).Reverse();
                    }
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

                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id);
                    }
                    isMatchesPlayedClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id);
                    }
                    isGoalsClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id);
                    }
                    isAssistsClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id);
                    }

                    isYellowCardsClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id);
                    }
                    isRedCardsClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id).Reverse();
                    }
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
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id)
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id);
                    }
                    isStatusClicked = true;
                }
                else
                {
                    if (selectedSerie != null && _team != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id).Reverse()
                        .Where(p => p.TeamId == _team.Id);
                    }
                    else if (selectedSerie != null)
                    {
                        playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id).Reverse();
                    }
                    isStatusClicked = false;
                }
            }
        }
    }
}
