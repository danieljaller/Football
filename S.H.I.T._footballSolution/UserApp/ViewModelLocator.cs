using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.ViewModels;

namespace UserApp
{
    public class ViewModelLocator
    {
        private static MatchService matchService = new MatchService();
        private static TeamService teamService = new TeamService();
        private static PlayerService playerService = new PlayerService(teamService);
        private static SerieService serieService = new SerieService();

        private static PlayerInfoViewModel _playerInfoViewModel = new PlayerInfoViewModel(teamService);
        private static PlayerViewModel _playerViewModel = new PlayerViewModel();
        private static SinglePlayerViewModel _singlePlayerViewModel = new SinglePlayerViewModel(teamService);

        static PlayerInfoViewModel PlayerInfoViewModle { get { return _playerInfoViewModel; } }
        static PlayerViewModel PlayerViewModel { get { return _playerViewModel; } }
        static SinglePlayerViewModel SinglePlayerViewModel { get { return _singlePlayerViewModel; } }
    }
}
