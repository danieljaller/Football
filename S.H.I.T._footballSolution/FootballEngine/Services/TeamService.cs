using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Services
{
    public class TeamService : IService<Team>
    {
        private readonly TeamRepository _teamRepository = TeamRepository.Instance;
        private static PlayerService playerService;
        private static MatchService matchService;

        private static readonly object CreationLock = new object();
        private static TeamService _instance;
        public static TeamService Default
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new TeamService();
                            playerService = ServiceLocator.Default.PlayerService;
                            matchService = ServiceLocator.Default.MatchService;
                        }
                    }
                }

                return _instance;
            }
        }

        public void Add(Team team)
        {
            _teamRepository.Add(team);
        }

        public void Delete(Guid id)
        {
            foreach (var player in GetAllPlayersByTeam(id))
            {
                playerService.Delete(player.Id);
            }
            _teamRepository.Delete(id);
        }
        public IEnumerable<Team> GetAllTeamsBySerie(Guid serieId)
        {
            return GetAll().Where(t => t.SeriesIds.Contains(serieId));
        }

        public IEnumerable<Player> GetAllPlayersByTeam(Guid id)
        {   
            return playerService.GetAll().Where(p => p.TeamId == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return _teamRepository.GetAll();
        }

        public Team GetBy(string name)
        {
           return _teamRepository.GetBy(name);
        }

        public Team GetBy(Guid id)
        {
            return _teamRepository.GetBy(id);
        }

        public void Save()
        {
            _teamRepository.Save();
        }

        public IEnumerable<Team> OrderByTeamName(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderBy(t => t.Name.Value);
        }

        public IEnumerable<Team> OrderByPoints(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Points);
        }

        public IEnumerable<Team> OrderByNumberOfGoalsFor(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.GoalsFor);
        }

        public IEnumerable<Team> OrderByNumberOfGoalsAgainst(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.GoalsAgainst);
        }

        public IEnumerable<Team> OrderByNumberOfMatchesPlayed(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.MatchIds.Where(m => matchService.GetBy(m).Date.Date < DateTime.Today).Count());
        }

        public IEnumerable<Team> OrderByGoalDifference(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.GoalDifference);
        }

        public IEnumerable<Team> OrderByNumberOfWins(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Wins);
        }

        public IEnumerable<Team> OrderByNumberOfLosses(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Losses);
        }

        public IEnumerable<Team> OrderByNumberOfTies(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Ties);
        }
    }
}
