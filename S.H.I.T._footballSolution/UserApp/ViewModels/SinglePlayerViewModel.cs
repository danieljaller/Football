using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserApp.InformationHolders;
using UserApp.Utilities;

namespace UserApp.ViewModels
{
    public class SinglePlayerViewModel : INotifyPropertyChanged
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

        public ICommand ViewPlayerInfoCommand { get; set; }
        public ICommand ViewPlayersTeamInfoCommand { get; set; }

        public Visibility ViewPlayerInfoVisibility { get; set; }
        public Visibility ViewPlayersTeamInfoVisibility { get; set; }

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

            LoadCommands();

            Messenger.Default.Register<ObjectHolder<Player, Team>>(this, OnObjectHolderRecived);
        }

        private void LoadCommands()
        {
            ViewPlayerInfoCommand = new CustomCommand(ViewPlayerInfo, CanViewPlayerInfo);
            ViewPlayersTeamInfoCommand = new CustomCommand(ViewPlayersTeamInfo, CanViewPlayersTeamInfo);

            ViewPlayerInfoVisibility = Visibility.Collapsed;
            ViewPlayersTeamInfoVisibility = Visibility.Collapsed;
        }

        private void ViewPlayerInfo(object obj)
        {
            Messenger.Default.Send(new ObjectHolder<Player, string>(SelectedPlayer, PlayersTeam.Name.Value));
            ViewPlayerInfoVisibility = Visibility.Visible;
        }

        private bool CanViewPlayerInfo(object obj)
        {
            if (SelectedPlayer != null)
                return true;

            return false;
        }

        private void ViewPlayersTeamInfo(object obj)
        {
            throw new NotImplementedException();
        }
        
        private bool CanViewPlayersTeamInfo(object obj)
        {
            if (PlayersTeam != null)
                return true;

            return false;
        }

        private void OnObjectHolderRecived(ObjectHolder<Player, Team> playerAndTeamHolder)
        {
            LoadCommands();
            SelectedPlayer = playerAndTeamHolder.FirstObject;
            PlayersTeam = teamService.GetBy(SelectedPlayer.TeamId);
        }
    }
}
