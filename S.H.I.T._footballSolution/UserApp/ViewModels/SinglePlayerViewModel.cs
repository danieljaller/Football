using FootballEngine.Domain.Entities;
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
    class SinglePlayerViewModel : INotifyPropertyChanged
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
        public string PlayerName { get { return (SelectedPlayer != null) ? SelectedPlayer.FullName : ""; } }

        public SinglePlayerViewModel(TeamService teamService)
        {
            this.teamService = teamService;
            Messenger.Default.Register<ObjectHolder<Player, Team>>(this, OnObjectHolderRecived);
        }

        public void OnObjectHolderRecived(ObjectHolder<Player, Team> playerAndTeamHolder)
        {
            SelectedPlayer = playerAndTeamHolder.FirstObject;
            PlayersTeam = teamService.GetBy(SelectedPlayer.TeamId);
        }
    }
}
