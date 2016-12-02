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

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for PlayerPage.xaml
    /// </summary>
    /// 
    public partial class PlayerPage : Page
    {
        Serie selectedSerie;
        ObservableCollection<Player> players;
        bool isMouseButtonClicked;
        public PlayerPage()
        {

        }
        public PlayerPage(Serie selectedSerie)
        {
            this.selectedSerie = selectedSerie;
            InitializeComponent();
            SetPlayerData(selectedSerie);
            isMouseButtonClicked = true;
            //GenerateGridRowsAndSetRowColor();
        }
        private void SetPlayerData(Serie selectedSerie)
        {

            if (selectedSerie != null)
            {
                players = new ObservableCollection<Player>(ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id));
                playerStatsListbox.ItemsSource = players;
                //playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.GetAllPlayersBySerie(selectedSerie.Id).ToObservableCollection();
            }

        }

        private void playerName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)   // orderByLast name eller orderByFirst name... inte som det är nu med båda...
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByLastName(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByFirstName(selectedSerie.Id);
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerTeam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByTeamName(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerAge_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByDateOfBirth(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerMatchesPlayed_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfMatchesPlayed(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerGoals_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfGoals(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerAssists_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfAssists(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerYellowCards_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfYellowCards(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerRedCards_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByNumberOfRedCards(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }

        private void playerStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedSerie != null)
            {
                if (isMouseButtonClicked)
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id);
                    isMouseButtonClicked = false;
                }
                else
                {
                    playerStatsListbox.ItemsSource = ServiceLocator.Instance.PlayerService.OrderByPlayerStatus(selectedSerie.Id).Reverse();
                    isMouseButtonClicked = true;
                }
            }
        }
    }
}
