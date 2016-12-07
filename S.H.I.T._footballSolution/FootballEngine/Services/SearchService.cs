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

        internal static SearchService Instance
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

        internal SearchService()
        {
        }

        public IEnumerable<object> Search(string searchText, bool serieSearch, bool playerSearch, bool teamSearch, bool matchDateSearch, bool ignoreCase)
        {
            IEnumerable<object> result = new List<object>();

            if (!serieSearch && !playerSearch && !teamSearch && !matchDateSearch)
            {
                serieSearch = true;
                playerSearch = true;
                teamSearch = true;
                matchDateSearch = true;
            }

            if (serieSearch)
            {
                IEnumerable<object> serieResult = _serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase) ||

                                                        _teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.SerieIds.Contains(s.Id))
                                                        );
                result = result.Concat(serieResult);
            }

            if (playerSearch)
            {
                IEnumerable<object> playerResult = _playerRepository.GetAll().Where(p => p.FullName.Contains(searchText, ignoreCase) ||
                                                         p.DateOfBirth.ToString().Contains(searchText, ignoreCase) ||

                                                         _teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.PlayerIds.Contains(p.Id))
                                                        );

                result = result.Concat(playerResult);
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

            if (matchDateSearch)
            {
                IEnumerable<object> matchResult = _matchRepository.GetAll().Where(m => m.Date.Value.ToShortDateString().Contains(searchText));
                result = result.Concat(matchResult);
            }

            return result;
        }
    }
}