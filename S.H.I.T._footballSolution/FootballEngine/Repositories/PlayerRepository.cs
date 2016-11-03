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

        public PlayerRepository()
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

        public void Load()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Players.xml", "Resorces", false, out path))
            {
                try
                {
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));
                        players = xmlSerializer.Deserialize(stream) as List<Player>;
                    }
                }
                catch
                {
                    players = new List<Player>();
                }
            }
        }

        public void Save()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Players.xml", "Resorces", true, out path))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        xmlSerializer.Serialize(stream, players);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
