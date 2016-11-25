using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FootballEngine.Repositories
{
    public class MatchRepository : IRepository<Match>
    {
        private readonly string _path;
        private List<Match> _matches;

        private MatchRepository()
        {
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _path = Path.Combine(_path, "Resources");
            _path = Path.Combine(_path, "Matches.xml");
            Load();
        }

        private static MatchRepository _instance;
        public static MatchRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MatchRepository();

                return _instance;
            }
        }
        public void Add(Match match)
        {
            if (match == null)
                throw new ArgumentNullException($"Match cannot be null.");
            if (!_matches.Select(s => s.Id).Contains(match.Id))
                _matches.Add(match);
        }

        public void Delete(Guid id)
        {
            if (_matches.Select(s => s.Id).Contains(id))
            {
                _matches.Remove(_matches.Find(s => s.Id == id));
            }
        }

        public IEnumerable<Match> GetAll()
        {
            return _matches;
        }

        public Match GetBy(Guid id)
        {
            return _matches.Find(s => s.Id == id);
        }

        public Match GetBy(string name)
        {
            return _matches.Find(s => s.Location.Value == name);
        }

        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Matches.xml", "Resources", false, out path))
                //{
                _matches = (List<Match>)XmlHandler.LoadFrom(_path, typeof(List<Match>));
                //}
            }
            catch (LoadFailedException)
            {
                _matches = new List<Match>();
            }
        }

        public void Save()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Matches.xml", "Resources", true, out path))
                //{
                XmlHandler.SaveTo(_path, _matches);
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
