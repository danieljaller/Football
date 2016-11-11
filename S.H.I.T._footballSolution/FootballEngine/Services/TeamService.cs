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
            foreach (var player in GetAllPlayers(id))
            {
                playerService.Delete(player.Id);
            }
            _teamRepository.Delete(id);
        }

        public IEnumerable<Player> GetAllPlayers(Guid id)
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
    }
}
