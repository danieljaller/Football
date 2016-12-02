using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Services
{
    public class MatchService : IService<Match>
    {
        private readonly MatchRepository _matchRepository = MatchRepository.Instance;

        private static readonly object CreationLock = new object();
        private static MatchService _instance;
        internal static MatchService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MatchService();
                        }
                    }
                }

                return _instance;
            }
        }

        internal MatchService() { }

        public void Add(Match match)
        {
            _matchRepository.Add(match);
        }

        public void AddRange(IEnumerable<Match> matches)
        {
            _matchRepository.AddRange(matches);
        }

        public void Delete(Guid id)
        {
            _matchRepository.Delete(id);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.GetAll();
        }

        public Match GetBy(string name)
        {
            return _matchRepository.GetBy(name);
        }

        public Match GetBy(Guid id)
        {
            return _matchRepository.GetBy(id);
        }

        public IEnumerable<Match> GetAllMatchesBySerie(Guid id)
        {
            return _matchRepository.GetAll().Where(m => ServiceLocator.Instance.SerieService.GetBy(id).MatchTable.Contains(m.Id));
        }

        public void Save()
        {
            _matchRepository.Save();
        }


        public HashSet<Guid> OrderByHomeTeam(HashSet<Guid> matchIds)
        {
            var matches = _matchRepository.GetAll().Where(m => matchIds.Contains(m.Id));
            return matches.OrderBy(m => ServiceLocator.Instance.TeamService.GetBy((m).HomeTeamId).Name.Value).Select(m => m.Id).ToHashSet();
        }

        public HashSet<Guid> OrderByVisitorTeam(HashSet<Guid> matchIds)
        {
            var matches = _matchRepository.GetAll().Where(m => matchIds.Contains(m.Id));
            return matches.OrderBy(m => ServiceLocator.Instance.TeamService.GetBy((m).VisitorTeamId).Name.Value).Select(m => m.Id).ToHashSet();
        }

        public HashSet<Guid> OrderByDate(HashSet<Guid> matchIds)
        {
            var matches = _matchRepository.GetAll().Where(m => matchIds.Contains(m.Id));
            return matches.OrderBy(m => m.Date.Value).Select(m => m.Id).ToHashSet();
        }
    }
}
