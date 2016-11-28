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
                return;
            if (!_players.Select(s => s.Id).Contains(player.Id))
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

            _players.AddRange(players);
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
            return _players.Find(s => s.Id == id);
        }

        public Player GetBy(string name)
        {
            return _players.Find(s => s.FullName == name);
        }

        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Players.xml", "Resources", false, out path))
                //if( true)
                //{
                _players = (List<Player>)XmlHandler.LoadFrom(_path, typeof(List<Player>));
                //}

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
                //if (TryGetFilePath.InSolutionDirectory("Players.xml", "Resources", true, out path))
                //{
                XmlHandler.SaveTo(_path, _players);
                //}

            }
            catch (SaveFailedException s)
            {
                throw s;
            }
            catch (ArgumentException a)
            { throw a; }
        }
    }
}
