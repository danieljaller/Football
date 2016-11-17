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
    public class MatchRepository : IRepository<Match>
    {
        private List<Match> matches;
        private MatchRepository()
        {
            Load();
        }

        private static MatchRepository _instance;
        public static MatchRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MatchRepository();
                }

                return _instance;
            }
        }
        public void Add(Match match)
        {
            if (match == null)
            { throw new ArgumentNullException($"Match cannot be null."); }
            if (matches != null && match != null)
            {
                if (!matches.Select(s => s.Id).Contains(match.Id))
                {
                    matches.Add(match);
                }
            }

        }

        public void Delete(Guid id)
        {
            if (matches != null)
            {
                if (matches.Select(s => s.Id).Contains(id))
                {
                    matches.Remove(matches.Find(s => s.Id == id));
                }
            }
        }

        public IEnumerable<Match> GetAll()
        {
            if (matches != null)
            {
                return matches as IEnumerable<Match>;
            }

            return null;
        }

        public Match GetBy(Guid id)
        {
            if (matches != null)
            {
                return matches.Find(s => s.Id == id);
            }

            return null;
        }

        public Match GetBy(string name)
        {
            if (matches != null)
            {
                return matches.Find(s => s.Location.Value == name);
            }

            return null;
        }
        string[] directories = new string[2] { "FootballEngine", "Resources" };
        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InSolutionDirectory("Matches.xml", directories, false, out path))
                {
                    matches = (List<Match>)XmlHandler.LoadFrom(path, typeof(List<Match>));
                }
            }
            catch (LoadFailedException)
            {
                matches = new List<Match>();
            }
        }

        public void Save()
        {
            try
            {
                string path;

                if (TryGetFilePath.InSolutionDirectory("Matches.xml", directories, true, out path))
                {
                    XmlHandler.SaveTo(path, matches);
                }
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
