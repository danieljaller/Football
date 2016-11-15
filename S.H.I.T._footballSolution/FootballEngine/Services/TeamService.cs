using FootballEngine.Domain.Entities;
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
        PlayerService playerService = new PlayerService();

        public void Add(Team team)
        {
            _teamRepository.Add(team);
        }

        public void Delete(Guid id)
        {
            foreach (var player in GetAllPlayersBy(id))
            {
                playerService.Delete(player.Id);
            }
            _teamRepository.Delete(id);
        }

        public IEnumerable<Player> GetAllPlayersBy(Guid id)
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

        public void OrderByTeamName()
        {
            GetAll().OrderBy(t => t.Name.Value);
        }

        public IEnumerable<Team> OrderByPoints()
        {
            return GetAll().OrderByDescending(t => t.Points);
        }

        public IEnumerable<Team> OrderByNumberOfGoalsFor()
        {
            return GetAll().OrderByDescending(t => t.GoalsFor);
        }

        public IEnumerable<Team> OrderByNumberOfGoalsAgainst()
        {
            return GetAll().OrderByDescending(t => t.GoalsAgainst);
        }

        public IEnumerable<Team> OrderByNumberOfMatchesPlayed()
        {
            return GetAll().OrderByDescending(t => t.MatchIds.Count());
        }

        public IEnumerable<Team> OrderByGoalDifference()
        {
            return GetAll().OrderByDescending(t => t.GoalDifference);
        }

        public IEnumerable<Team> OrderByNumberOfWins()
        {
            return GetAll().OrderByDescending(t => t.Wins);
        }

        public IEnumerable<Team> OrderByNumberOfLosses()
        {
            return GetAll().OrderByDescending(t => t.Losses);
        }

        public IEnumerable<Team> OrderByNumberOfTies()
        {
            return GetAll().OrderByDescending(t => t.Ties);
        }
    }
}
