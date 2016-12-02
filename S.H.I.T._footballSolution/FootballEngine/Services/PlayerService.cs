using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Services
{
    public class PlayerService : IService<Player>
    {
        private readonly PlayerRepository _playerRepository = PlayerRepository.Instance;

        private static readonly object CreationLock = new object();
        private static PlayerService _instance;
        internal static PlayerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PlayerService();
                        }
                    }
                }

                return _instance;
            }
        }

        internal PlayerService() { }

        public void Add(Player player)
        {
            _playerRepository.Add(player);
        }

        public void AddRange(IEnumerable<Player> players)
        {
            _playerRepository.AddRange(players);
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
            return ServiceLocator.Instance.TeamService.GetAllTeamsBySerie(serieId).SelectMany(t => ServiceLocator.Instance.TeamService.GetAllPlayersByTeam(t.Id));
        }

        public IEnumerable<Player> OrderByFirstName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.FirstName.Value);
        }
        public IEnumerable<Player> OrderByLastName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.LastName.Value);
        }
        public IEnumerable<Player> OrderByDateOfBirth(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => p.DateOfBirth.Value);
        }
        public IEnumerable<Player> OrderByTeamName(Guid serieId)
        {
            return GetAllPlayersBySerie(serieId).OrderBy(p => ServiceLocator.Instance.TeamService.GetBy(p.TeamId).Name.Value);
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
