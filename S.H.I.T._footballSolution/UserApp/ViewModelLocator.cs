using FootballEngine.Helper;
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
        private ServiceLocator serviceLocator = ServiceLocator.Default;
        //private static MatchService matchService = new MatchService();
        //private static TeamService teamService = new TeamService();
        //private static PlayerService playerService = new PlayerService();
        //private static SerieService serieService = new SerieService();

        private static MainViewModel _mainViewModel = new MainViewModel();
        //private static PlayerInfoViewModel _playerInfoViewModel = new PlayerInfoViewModel(ServiceLocator.Default.TeamService);
        private static PlayerViewModel _playerViewModel = new PlayerViewModel();
        //private static SinglePlayerViewModel _singlePlayerViewModel = new SinglePlayerViewModel(ServiceLocator.Default.TeamService);

        public static MainViewModel MainViewModel { get { return _mainViewModel; } }
        //public static PlayerInfoViewModel PlayerInfoViewModel { get { return _playerInfoViewModel; } }
        public static PlayerViewModel PlayerViewModel { get { return _playerViewModel; } }
        //public static SinglePlayerViewModel SinglePlayerViewModel { get { return _singlePlayerViewModel; } }
    }
}
