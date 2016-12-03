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
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
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
                throw new ArgumentNullException($"{nameof(match)} cannot be null.");
            if (_matches.Select(s => s.Id).Contains(match.Id))
                throw new ArgumentException($"A {nameof(match)} with the id '{match.Id}' already exsist in the repository.");

            _matches.Add(match);
        }

        public void AddRange(IEnumerable<Match> matches)
        {
            if (matches == null)
                throw new ArgumentNullException($"{nameof(matches)} cannot be null.");
            if (matches.Count() == 0)
                throw new ArgumentOutOfRangeException($"{nameof(matches)} cannot be null.");
            if (matches.Contains(null))
                throw new ArgumentException($"{nameof(matches)} cannot contain null elements.");

            foreach (Match match in matches)
                Add(match);
        }

        public void Delete(Guid id)
        {
            if (_matches.Select(s => s.Id).Contains(id))
                _matches.Remove(_matches.Find(s => s.Id == id));
        }

        public IEnumerable<Match> GetAll()
        {
            return _matches;
        }

        public Match GetBy(Guid id)
        {
            foreach(var match in _matches)
                if (match.Id == id)
                    return match;

            return null;
        }

        public Match GetBy(string location)
        {
            if (location != null)
                foreach (var match in _matches)
                    if (match.Location.Value == location)
                        return match;

            return null;
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
            {
                throw a;
            }
        }
    }
}
