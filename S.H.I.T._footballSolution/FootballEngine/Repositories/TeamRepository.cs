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
        private readonly string _path;
        private List<Team> _teams;

        private TeamRepository()
        {
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _path = Path.Combine(_path, "Resources");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            _path = Path.Combine(_path, "Teams.xml");
            Load();
        }

        private static TeamRepository _instance;

        public static TeamRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TeamRepository();

                return _instance;
            }
        }

        public void Add(Team team)
        {
            if (team == null)
                return;
            if (!_teams.Select(t => t.Id).Contains(team.Id) && !_teams.Select(t => t.Name).Contains(team.Name)) // Checking for name does not work!
                _teams.Add(team);
        }

        public void AddRange(IEnumerable<Team> teams)
        {
            if (teams == null)
                throw new ArgumentNullException($"{nameof(teams)} cannot be null.");
            if (teams.Count() == 0)
                throw new ArgumentOutOfRangeException($"{nameof(teams)} cannot be null.");
            if (teams.Contains(null))
                throw new ArgumentException($"{nameof(teams)} cannot contain null elements.");

            _teams.AddRange(teams);
        }

        public void Delete(Guid id)
        {
            if (_teams.Select(t => t.Id).Contains(id))
                _teams.Remove(_teams.Find(t => t.Id == id));
        }

        public IEnumerable<Team> GetAll()
        {
            return _teams;
        }

        public Team GetBy(Guid id)
        {
            foreach (Team team in _teams)
            {
                if (team.Id == id)
                    return team;
            }
            return null;
        }

        public Team GetBy(string name)
        {
            if (name != null)
            {
                foreach (Team team in _teams)
                {
                    if (team.Name.Value == name)
                        return team;
                }
            }
            return null;
        }

        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Teams.xml", "Resources", false, out path))
                //{
                _teams = (List<Team>)XmlHandler.LoadFrom(_path, typeof(List<Team>));
                //}               
            }
            catch (LoadFailedException)
            {
                _teams = new List<Team>();
            }
        }

        public void Save()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Teams.xml", "Resources", true, out path))
                //{
                XmlHandler.SaveTo(_path, _teams);
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
