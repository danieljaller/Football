using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FootballEngine.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private readonly string path;
        private List<Team> teams;

        private TeamRepository()
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "Resources");
            path = Path.Combine(path, "Teams.xml");
            Load();
        }

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
        
        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Teams.xml", "Resources", false, out path))
                //{
                    teams = (List<Team>)XmlHandler.LoadFrom(path, typeof(List<Team>));
                //}               
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
                //if (TryGetFilePath.InSolutionDirectory("Teams.xml", "Resources", true, out path))
                //{
                    XmlHandler.SaveTo(path, teams);
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
