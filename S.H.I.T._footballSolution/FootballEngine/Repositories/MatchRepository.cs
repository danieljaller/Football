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
            if (TryGetFilePath.InProjectDirectory("Matches.xml", "Resorces", false, out path))
            {
                try
                {
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Match>));
                        matches = xmlSerializer.Deserialize(stream) as List<Match>;
                    }
                }
                catch
                {
                    matches = new List<Match>();
                }
            }
        }

        public void Save()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Matches.xml", "Resorces", true, out path))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Match>));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        xmlSerializer.Serialize(stream, matches);
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
