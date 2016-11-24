using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FootballEngine.Repositories
{
    class PlayerRepository : IRepository<Player>
    {
        private readonly string path;
        private List<Player> players;

        private PlayerRepository()
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "Players.xml");
            Load();
        }
        private static PlayerRepository _instance;
        public static PlayerRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PlayerRepository();
                }

                return _instance;
            }
        }
        public void Add(Player player)
        {
            if (players != null && player != null)
            {
                if (!players.Select(s => s.Id).Contains(player.Id))
                {
                    players.Add(player);
                }
            }
        }

        public void Delete(Guid id)
        {
            if (players != null)
            {
                if (players.Select(s => s.Id).Contains(id))
                {
                    players.Remove(players.Find(s => s.Id == id));
                }
            }
        }

        public IEnumerable<Player> GetAll()
        {
            if (players != null)
            {
                return players as IEnumerable<Player>;
            }

            return null;
        }

        public Player GetBy(Guid id)
        {
            if (players != null)
            {
                return players.Find(s => s.Id == id);
            }

            return null;
        }

        public Player GetBy(string name)
        {
            if (players != null)
            {
                return players.Find(s => s.FullName == name);
            }

            return null;
        }
        
        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Players.xml", "Resources", false, out path))
                //if( true)
                //{
                    players = (List<Player>)XmlHandler.LoadFrom(path, typeof(List<Player>));
                //}
               
            }
            catch(LoadFailedException)
            {
                players = new List<Player>();
            }
        }

        public void Save()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Players.xml", "Resources", true, out path))
                //{
                    XmlHandler.SaveTo(path, players);
                //}
                
            }
            catch (SaveFailedException s)
            {
                throw s;
            }
            catch(ArgumentException a)
            { throw a; }
        }
    }
}
