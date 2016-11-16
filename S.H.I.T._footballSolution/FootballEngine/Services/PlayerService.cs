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
  public  class PlayerService : IService<Player>
    {
        private readonly PlayerRepository _playerRepository = PlayerRepository.Instance;
        TeamService teamService = new TeamService();
        public void Add(Player player)
        {
            _playerRepository.Add(player);
        }

        public void Delete(Guid id)
        {
            _playerRepository.Delete(id);
        }

        public IEnumerable<Player> GetAll()
        {
            return _playerRepository.GetAll();
        }

        public Player GetBy(string name)
        {
            return _playerRepository.GetBy(name);
        }

        public Player GetBy(Guid id)
        {
            return _playerRepository.GetBy(id);
        }

        public void Save()
        {
            _playerRepository.Save();
        }
        public IEnumerable<Player> OrderByFirstName()
        {
            return GetAll().OrderBy(p => p.FirstName);
        }
        public IEnumerable<Player> OrderByLastName()
        {
            return GetAll().OrderBy(p => p.LastName);
        }
        public IEnumerable<Player> OrderByDateOfBirth()
        {
            return GetAll().OrderBy(p => p.DateOfBirth);
        }
        public IEnumerable<Player> OrderByTeamName()
        {
            return GetAll().OrderBy(p => teamService.GetBy(p.TeamId).Name);
        }
        public IEnumerable<Player> OrderByNumberOfMatchesPlayed()
        {
            return GetAll().OrderByDescending(p => p.MatchesPlayed);
        }
        public IEnumerable<Player> OrderByNumberOfGoals()
        {
            return GetAll().OrderByDescending(p => p.Goals.Count());
        }
        public IEnumerable<Player> OrderByNumberOfAssists()
        {
            return GetAll().OrderByDescending(p => p.Assists.Count());
        }
        public IEnumerable<Player> OrderByNumberOfRedCards()
        {
            return GetAll().OrderByDescending(p => p.RedCards.Count());
        }
        public IEnumerable<Player> OrderByNumberOfYellowCards()
        {
            return GetAll().OrderByDescending(p => p.YellowCards.Count());
        }
        public IEnumerable<Player> OrderByPlayerStatus()
        {
            return GetAll().OrderByDescending(p => (int)p.PlayerStatus);
        }
    }
}
