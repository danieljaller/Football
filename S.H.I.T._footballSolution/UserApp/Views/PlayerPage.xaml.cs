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
    //public partial class PlayerPage : Page
    //{
    //    private List<Player> _players;

    //    public PlayerPage()
    //    {
    //        InitializeComponent();
    //    }

    //    public PlayerPage(Guid teamId)
    //    {
    //        InitializeComponent();
    //        _players = ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(teamId).ToList();
    //        //GenerateGridRowsAndSetRowColor();
    //    }

    //    private void GenerateGridRowsAndSetRowColor()
    //    {
    //        string[] testArray = new string[30];

    //        foreach (var position in testArray)
    //        {
    //            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto, MinHeight = 20 });
    //        }

    //        for (int i = 2; i < grid.RowDefinitions.Count(); i += 2)
    //        {
    //            Rectangle rect = new Rectangle();
    //            rect.Fill = new SolidColorBrush(Colors.LightGray);
    //            grid.Children.Add(rect);
    //            Grid.SetColumnSpan(rect, 9);
    //            Grid.SetRow(rect, i);
    //        }
    //    }
    //}
}
