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
using UserApp.InformationHolders;
using UserApp.Utilities;

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
        private string _team;
        public string Team
        {
            get { return (_team != null) ? _team : ""; }
            set { _team = value; }
        }
        public string Age { get { return (SelectedPlayer != null) ? (DateTime.Now.Year - SelectedPlayer.DateOfBirth.Value.Year).ToString() : ""; } }
        public string DateOfBirth { get { return (SelectedPlayer != null) ? SelectedPlayer.DateOfBirth.ToString() : ""; } }
        public string Goals { get { return (SelectedPlayer != null) ? SelectedPlayer.Goals.Count.ToString() : ""; } }
        public string Assists { get { return (SelectedPlayer != null) ? SelectedPlayer.Assists.Count.ToString() : ""; } }
        public string RedCards { get { return (SelectedPlayer != null) ? SelectedPlayer.RedCards.Count.ToString() : ""; } }
        public string YellowCards { get { return (SelectedPlayer != null) ? SelectedPlayer.YellowCards.Count.ToString() : ""; } }
        public string MatchesPlayed { get { return (SelectedPlayer != null) ? SelectedPlayer.MatchesPlayed.ToString() : ""; } }
        public string PlayerStatus { get { return (SelectedPlayer != null) ? SelectedPlayer.PlayerStatus.ToSwedishString() : ""; } }

        public PlayerInfoViewModel(TeamService teamService)
        {
            this.teamService = teamService;
            Messenger.Default.Register<ObjectHolder<Player, string>>(this, OnInformationHolderRecived);
        }

        public void OnInformationHolderRecived(ObjectHolder<Player, string> playerAndTeamNameHolder)
        {
            SelectedPlayer = playerAndTeamNameHolder.FirstObject;
            Team = playerAndTeamNameHolder.SecondObject;
        }
    }
}
