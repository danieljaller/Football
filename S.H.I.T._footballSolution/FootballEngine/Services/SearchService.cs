using FootballEngine.Helper;
using FootballEngine.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Services
{
    public class SearchService
    {
        private readonly MatchRepository _matchRepository = MatchRepository.Instance;
        private readonly PlayerRepository _playerRepository = PlayerRepository.Instance;
        private readonly SerieRepository _serieRepository = SerieRepository.Instance;
        private readonly TeamRepository _teamRepository = TeamRepository.Instance;

        private static readonly object CreationLock = new object();
        private static SearchService _instance;
        public static SearchService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SearchService();
                        }
                    }
                }

                return _instance;
            }
        }

        public IEnumerable<object> Search(string searchText, bool matchDateSearch, bool playerSearch, bool serieSearch, bool teamSearch, bool ignoreCase)
        {
            IEnumerable<object> result = new List<object>();

            if (!matchDateSearch && !playerSearch && !serieSearch && !teamSearch)
            {
                matchDateSearch = true;
                playerSearch = true;
                serieSearch = true;
                teamSearch = true;
            }

            if (matchDateSearch)
            {
                IEnumerable<object> matchResult = _matchRepository.GetAll().Where(m => m.Date.Value.ToShortDateString().Contains(searchText, ignoreCase));
                result = result.Concat(matchResult);
            }

            if (playerSearch)
            {
                IEnumerable<object> playerResult = _playerRepository.GetAll().Where(p => p.FullName.Contains(searchText, ignoreCase) ||
                                                         p.DateOfBirth.ToShortDateString().Contains(searchText, ignoreCase) ||

                                                         _teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.PlayerIds.Contains(p.Id))
                                                        );

                result = result.Concat(playerResult);
            }

            if (serieSearch)
            {
                IEnumerable<object> serieResult = _serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase) ||

                                                        _teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.SerieIds.Contains(s.Id))
                                                        );
                result = result.Concat(serieResult);
            }

            if (teamSearch)
            {
                IEnumerable<object> teamResult = _teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase) ||
                                                         t.HomeArena.Value.Contains(searchText, ignoreCase) ||

                                                         _playerRepository.GetAll().Where(p => p.FullName.Contains(searchText, ignoreCase))
                                                            .Any(p => p.TeamId == t.Id) ||

                                                         _serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(s => s.TeamTable.Contains(t.Id))
                                                        );
                result = result.Concat(teamResult);
            }



            return result;
        }
    }
}
