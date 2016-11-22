using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper
{
    public class ServiceLocator
    {
        private static readonly object CreationLock = new object();
        private static ServiceLocator _instance;
        public static ServiceLocator Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ServiceLocator();

                            matchService = MatchService.Default;
                            playerService = PlayerService.Default;
                            searchService = SearchService.Default;
                            serieService = SerieService.Default;
                            teamService = TeamService.Default;
                        }
                    }
                }

                return _instance;
            }
        }

        private static MatchService matchService = new MatchService();
        private static PlayerService playerService = new PlayerService();
        private static SearchService searchService = new SearchService();
        private static SerieService serieService = new SerieService();
        private static TeamService teamService = new TeamService();

        public MatchService MatchService { get { return matchService; } }
        public PlayerService PlayerService { get { return playerService; } }
        public SearchService SearchService { get { return searchService; } }
        public SerieService SerieService { get { return serieService; } }
        public TeamService TeamService { get { return teamService; } }
    }
}
