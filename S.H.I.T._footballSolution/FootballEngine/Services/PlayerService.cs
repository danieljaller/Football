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
        TeamService teamService;
        public PlayerService(TeamService teamService)
        {
            this.teamService = teamService;    
        }
        
        SerieService serieService = new SerieService();
        public void Add(Player player)
        {
            _playerRepository.Add(player);
        }

        public void Add(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                _playerRepository.Add(player);
            }
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

        public IEnumerable<Player> GetAllPlayersBySerie(Guid serieId)
        {
            return teamService.GetAllTeamsBySerie(serieId).SelectMany(t => teamService.GetAllPlayersByTeam(t.Id));
        }

        public IEnumerable<Player> OrderByFirstName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.FirstName);
        }
        public IEnumerable<Player> OrderByLastName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.LastName);
        }
        public IEnumerable<Player> OrderByDateOfBirth(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.DateOfBirth);
        }
        public IEnumerable<Player> OrderByTeamName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => teamService.GetBy(p.TeamId).Name);
        }
        public IEnumerable<Player> OrderByNumberOfMatchesPlayed(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => p.MatchesPlayed);
        }
        public IEnumerable<Player> OrderByNumberOfGoals(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => p.Goals.Count());
        }
        public IEnumerable<Player> OrderByNumberOfAssists(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => p.Assists.Count());
        }
        public IEnumerable<Player> OrderByNumberOfRedCards(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => p.RedCards.Count());
        }
        public IEnumerable<Player> OrderByNumberOfYellowCards(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => p.YellowCards.Count());
        }
        public IEnumerable<Player> OrderByPlayerStatus(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderByDescending(p => (int)p.PlayerStatus);
        }
    }
}
