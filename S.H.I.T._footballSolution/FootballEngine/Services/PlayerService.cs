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
    }
}
