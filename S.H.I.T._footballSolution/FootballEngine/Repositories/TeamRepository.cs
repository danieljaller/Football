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
    public class TeamRepository : IRepository<Team>
    {
        public TeamRepository()
        {
            Load();
        }

        private List<Team> teams;

        private static TeamRepository _instance;

        public static TeamRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TeamRepository();
                }
                return _instance;
            }
        }

        public void Add(Team team)
        {
            if (teams != null && team != null)
            {
                if (!teams.Select(t => t.Id).Contains(team.Id))
                {
                    teams.Add(team);
                }
            }
        }

        public void Delete(Guid id)
        {
            if (teams != null)
            {
                if (teams.Select(t => t.Id).Contains(id))
                {
                    teams.Remove(teams.Find(t => t.Id == id));
                }
            }
        }

        public IEnumerable<Team> GetAll()
        {
            if (teams != null)
            {
                return teams as IEnumerable<Team>;
            }
            return null;
        }

        public Team GetBy(Guid id)
        {
            if (teams != null)
            {
                return teams.Find(t => t.Id == id);
            }
            return null;
        }

        public Team GetBy(string name)
        {
            if (name != null)
            {
                return teams.Find(t => t.Name.Value == name);
            }
            return null;
        }

        public void Load()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Teams.xml", "Resources", false, out path))
            {
                try
                {
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Team>));
                        teams = xmlSerializer.Deserialize(stream) as List<Team>;
                    }
                }
                catch
                {
                    teams = new List<Team>();
                }
            }
        }

        public void Save()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Teams.xml", "Resources", true, out path))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Team>));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        xmlSerializer.Serialize(stream, teams);
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
