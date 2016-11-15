using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserApp.Utilities.JoeCoffeeStore.StockManagement.App.Utility;

namespace UserApp.ViewModels
{
    class PlayerInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private TeamService teamService;

        private Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { SetField(ref _selectedPlayer, value); }
        }
        private Team _playersTeam;
        public Team PlayersTeam
        {
            get { return _playersTeam; }
            set { SetField(ref _playersTeam, value); }
        }
        public string Team { get { return PlayersTeam.Name.Value; } }
        public string Age { get { return (DateTime.Now.Year - SelectedPlayer.DateOfBirth.Year).ToString(); } }
        public string DateOfBirth { get { return SelectedPlayer.DateOfBirth.ToShortDateString(); } }
        public string Goals { get { return SelectedPlayer.Goals.Count.ToString(); } }
        public string Assists { get { return SelectedPlayer.Assists.Count.ToString(); } }
        public string RedCards { get { return SelectedPlayer.RedCards.Count.ToString(); } }
        public string YellowCards { get { return SelectedPlayer.YellowCards.Count.ToString(); } }
        public string MatchesPlayed { get { return SelectedPlayer.MatchesPlayed.ToString(); } }
        public string PlayerStatus { get { return SelectedPlayer.PlayerStatus.ToSwedishString(); } }

        public PlayerInfoViewModel(TeamService teamService)
        {
            this.teamService = teamService;
            Messenger.Default.Register<Player>(this, OnPlayerRecived);
        }

        public void OnPlayerRecived(Player player)
        {
            SelectedPlayer = player;
            PlayersTeam = teamService.GetBy(SelectedPlayer.TeamId);
        }
    }
}
