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
        public IEnumerable<object> Search(string searchText, bool ignoreCase, bool serieSearch, bool teamSearch, bool playerSearch)
        {
            IEnumerable<object> result = new List<object>();


            if (serieSearch)
            {
                IEnumerable<object> serieResult = serieRepository.GetAll().Where(s => s.Name.Value.Contains(searchText, ignoreCase) ||

                                                        teamRepository.GetAll().Where(t => t.Name.Value.Contains(searchText, ignoreCase))
                                                            .Any(t => t.SeriesIds.Contains(s.Id))
                                                        );
                result = result.Concat(serieResult);
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





        private PlayerRepository playerRepository
        {
            get { return PlayerRepository.Instance; }
        }

        private TeamRepository teamRepository
        {
            get { return TeamRepository.Instance; }
        }

        private SerieRepository serieRepository
        {
            get { return SerieRepository.Instance; }
        }
    }
}
