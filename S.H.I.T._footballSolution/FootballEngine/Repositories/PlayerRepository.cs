using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FootballEngine.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private readonly string _path;
        private List<Player> _players;

        private PlayerRepository()
        {
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _path = Path.Combine(_path, "Resources");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            _path = Path.Combine(_path, "Players.xml");
            Load();
        }

        private static PlayerRepository _instance;

        public static PlayerRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerRepository();

                return _instance;
            }
        }

        public void Add(Player player)
        {
            if (player == null)
                throw new ArgumentNullException($"{nameof(player)} cannot be null.");
            if (_players.Select(s => s.Id).Contains(player.Id))
                throw new ArgumentException($"A {nameof(player)} with the id '{player.Id}' already exsist in the repository.");

            _players.Add(player);
        }

        public void AddRange(IEnumerable<Player> players)
        {
            if (players == null)
                throw new ArgumentNullException($"{nameof(players)} cannot be null.");
            if (players.Count() == 0)
                throw new ArgumentOutOfRangeException($"{nameof(players)} cannot be null.");
            if (players.Contains(null))
                throw new ArgumentException($"{nameof(players)} cannot contain null elements.");

            foreach (Player player in players)
                Add(player);
        }

        public void Delete(Guid id)
        {
            if (_players.Select(s => s.Id).Contains(id))
                _players.Remove(_players.Find(s => s.Id == id));
        }

        public IEnumerable<Player> GetAll()
        {
            return _players;
        }

        public Player GetBy(Guid id)
        {
            foreach (var player in _players)
                if (player.Id == id)
                    return player;

            return null;
        }

        public Player GetBy(string name)
        {
            if (name != null)
                foreach (Player player in _players)
                    if (player.FullName == name)
                        return player;

            return null;
        }

        public void Load()
        {
            try
            {
                _players = (List<Player>)XmlHandler.LoadFrom(_path, typeof(List<Player>));
            }
            catch (LoadFailedException)
            {
                _players = new List<Player>();
            }
        }

        public void Save()
        {
            try
            {
                XmlHandler.SaveTo(_path, _players);
            }
            catch (SaveFailedException s)
            {
                throw s;
            }
            catch (ArgumentException a)
            {
                throw a;
            }
        }
    }
}