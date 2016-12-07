using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UserApp.Views;

namespace UserApp
{
    /// <summary>
    /// Interaction logic for TeamInfoPage.xaml
    /// </summary>
    public partial class TeamInfoPage : Page
    {
        private Frame _teamPageFrame;

        public TeamInfoPage()
        {
            InitializeComponent();
        }

        public TeamInfoPage(Team selectedTeam)
        {
            InitializeComponent();
            DataContext = selectedTeam;
            StringBuilder serieStringBuilder = new StringBuilder();
            foreach (var serieId in selectedTeam.SerieIds)
            {
                serieStringBuilder.Append($"{ServiceLocator.Instance.SerieService.GetBy(serieId).Name}, ");
            }
            seriesTextBox.Text = serieStringBuilder.ToString().TrimEnd(',', ' ');
            matchesNotPlayedTextBlock.Text = selectedTeam.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == false).Count().ToString();
        }

        public TeamInfoPage(Team selectedTeam, Frame teamPageFrame)
        {
            InitializeComponent();
            _teamPageFrame = teamPageFrame;
            DataContext = selectedTeam;
            StringBuilder serieStringBuilder = new StringBuilder();
            foreach (var serieId in selectedTeam.SerieIds)
            {
                serieStringBuilder.Append($"{ServiceLocator.Instance.SerieService.GetBy(serieId).Name}, ");
            }
            seriesTextBox.Text = serieStringBuilder.ToString().TrimEnd(',', ' ');
            matchesNotPlayedTextBlock.Text = selectedTeam.MatchIds.Where(x => ServiceLocator.Instance.MatchService.GetBy(x).IsPlayed == false).Count().ToString();
        }

        private void PlayersList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_teamPageFrame != null)
            {
                Guid playerId = (Guid)playersList.SelectedItem;
                Player selectedPlayer = ServiceLocator.Instance.PlayerService.GetBy(playerId);
                _teamPageFrame.Content = new PlayerInfoPage(selectedPlayer);
            }
        }
    }
}