using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballEngine.Repositories
{
    class PlayerRepository : IRepository<Player>
    {
        private List<Player> players;

        private PlayerRepository()
        {
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

        string[] directories = new string[2] { "FootballEngine", "Resources" };
        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InSolutionDirectory("Players.xml", directories, false, out path))
                {
                    players = (List<Player>)XmlHandler.LoadFrom(path, typeof(List<Player>));
                }
               
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
                string path;
                if (TryGetFilePath.InSolutionDirectory("Players.xml", directories, true, out path))
                {
                    XmlHandler.SaveTo(path, players);
                }
                
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
