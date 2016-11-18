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
        private TeamRepository()
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
                if (!teams.Select(t => t.Id).Contains(team.Id) && !teams.Select(t => t.Name).Contains(team.Name))
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
        string[] directories = new string[2] { "FootballEngine", "Resources" };
        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InSolutionDirectory("Teams.xml", directories, false, out path))
                {
                    teams = (List<Team>)XmlHandler.LoadFrom(path, typeof(List<Team>));
                }               
            }
            catch(LoadFailedException)
            {
                teams = new List<Team>();
            }
        }

        public void Save()
        {
            try
            {
                string path;
                if (TryGetFilePath.InSolutionDirectory("Teams.xml", directories, true, out path))
                {
                    XmlHandler.SaveTo(path, teams);
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
