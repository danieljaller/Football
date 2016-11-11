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
        public MatchRepository()
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

        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InProjectDirectory("Matches.xml", "Resources", false, out path))
                {
                    matches = (List<Match>)XmlHandler.LoadFrom(path, typeof(List<Match>));
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Matches.xml\", \"Resources\", false, out path) failed");
                }
            }
            catch
            {
                matches = new List<Match>();
            }
        }

        public void Save()
        {
            try
            {
                string path;
                if (TryGetFilePath.InProjectDirectory("Matches.xml", "Resources", true, out path))
                {
                    XmlHandler.SaveTo(path, matches);
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Matches.xml\", \"Resources\", true, out path) failed");
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("Could not save file", innerException);
            }
        }
    }
}
