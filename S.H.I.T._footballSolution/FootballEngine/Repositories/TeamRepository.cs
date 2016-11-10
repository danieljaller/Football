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
            try
            {
                if (TryGetFilePath.InProjectDirectory("Teams.xml", "Resources", false, out path))
                {
                    teams = (List<Team>)XmlHandler.LoadFrom(path, typeof(List<Team>));
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Teams.xml\", \"Resources\", false, out path) failed");
                }
            }
            catch
            {
                teams = new List<Team>();
            }
        }

        public void Save()
        {
            try
            {
                string path;
                if (TryGetFilePath.InProjectDirectory("Teams.xml", "Resources", true, out path))
                {
                    XmlHandler.SaveTo(path, teams);
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Teams.xml\", \"Resources\", false, out path) failed");
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("Could not save file", innerException);
            }
        }
    }
}
