using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Services
{
    public class TeamService : IService<Team>
    {
        private readonly TeamRepository _teamRepository = TeamRepository.Instance;

        private static readonly object CreationLock = new object();
        private static TeamService _instance;
        internal static TeamService Default
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
                        }
                    }
                }

                return _instance;
            }
        }

        internal TeamService() { }

        public void Add(Team team)
        {
            _teamRepository.Add(team);
        }

        public void AddRange(IEnumerable<Team> teams)
        {
            _teamRepository.AddRange(teams);
        }

        public void Delete(Guid id)
        {
            foreach (var player in GetAllPlayersByTeam(id))
            {
                ServiceLocator.Instance.PlayerService.Delete(player.Id);
            }
            _teamRepository.Delete(id);
        }
        public IEnumerable<Team> GetAllTeamsBySerie(Guid serieId)
        {
            return GetAll().Where(t => t.SerieIds.Contains(serieId));
        }

        public IEnumerable<Player> GetAllPlayersByTeam(Guid id)
        {   
            return ServiceLocator.Instance.PlayerService.GetAll().Where(p => p.TeamId == id);
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

        public bool NameExist(string name)
        {
            if (_teamRepository.GetAll().Select(team => team.Name.Value).Contains(name))
                return true;

            return false;
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
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.MatchIds.Where(m => ServiceLocator.Instance.MatchService.GetBy(m).IsPlayed == true).Count());
        }

        public IEnumerable<Team> OrderByGoalDifference(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.GoalDifference);
        }

        public IEnumerable<Team> OrderByNumberOfWins(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Wins.Count);
        }

        public IEnumerable<Team> OrderByNumberOfLosses(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Losses.Count);
        }

        public IEnumerable<Team> OrderByNumberOfTies(Guid serieId)
        {
            return GetAllTeamsBySerie(serieId).OrderByDescending(t => t.Ties.Count);
        }
    }
}
