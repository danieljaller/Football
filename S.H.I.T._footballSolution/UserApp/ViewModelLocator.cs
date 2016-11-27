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
        private static PlayerInfoViewModel _playerInfoViewModel = new PlayerInfoViewModel();
        private static PlayerViewModel _playerViewModel = new PlayerViewModel();
        private static SinglePlayerViewModel _singlePlayerViewModel = new SinglePlayerViewModel();

        static PlayerInfoViewModel PlayerInfoViewModle { get { return _playerInfoViewModel; } }
        static PlayerViewModel PlayerViewModel { get { return _playerViewModel; } }
        static SinglePlayerViewModel SinglePlayerViewModel { get { return _singlePlayerViewModel; } }
    }
}
