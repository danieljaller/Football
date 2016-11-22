using FootballEngine.Helper;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Services
{
    public class SearchService
    {
        private MatchRepository matchRepository = MatchRepository.Instance;
        private PlayerRepository playerRepository = PlayerRepository.Instance;
        private SerieRepository serieRepository = SerieRepository.Instance;
        private TeamRepository teamRepository = TeamRepository.Instance;

        private static readonly object CreationLock = new object();
        private static SearchService _instance;
        public static SearchService Default
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
                //DateTime matchDate;
                //if (DateTime.TryParse(searchText, out matchDate))
                //{
                    //IEnumerable<object> matchResult = matchRepository.GetAll().Where(m => m.Date.Date == matchDate.Date);
                    //result = result.Concat(matchResult);
                //}
                IEnumerable<object> matchResult = matchRepository.GetAll().Where(m => m.Date.ToShortDateString().Contains(searchText, ignoreCase));
                result = result.Concat(matchResult);
            }

            if (playerSearch)
            {
                IEnumerable<object> playerResult = playerRepository.GetAll().Where(p => p.FullName.Contains(searchText, ignoreCase) ||
                                                         p.DateOfBirth.ToShortDateString().Contains(searchText, ignoreCase) ||

                                                         teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.PlayerIds.Contains(p.Id))
                                                        );

                result = result.Concat(playerResult);
            }

            if (serieSearch)
            {
                IEnumerable<object> serieResult = serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase) ||

                                                        teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.SeriesIds.Contains(s.Id))
                                                        );
                result = result.Concat(serieResult);
            }

            if (teamSearch)
            {
                IEnumerable<object> teamResult = teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase) ||
                                                         t.HomeArena.Value.Contains(searchText, ignoreCase) ||

                                                         playerRepository.GetAll().Where(p => p.FullName.Contains(searchText, ignoreCase))
                                                            .Any(p => p.TeamId == t.Id) ||

                                                         serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(s => s.TeamTable.Contains(t.Id))
                                                        );
                result = result.Concat(teamResult);
            }



            return result;
        }
    }
}
