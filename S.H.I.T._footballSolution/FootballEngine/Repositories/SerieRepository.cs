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
    public class SerieRepository : IRepository<Serie>
    {
        private List<Serie> series;
        public SerieRepository()
        {
            Load();
        }

        private static SerieRepository _instance;
        public static SerieRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SerieRepository();
                }

                return _instance;
            }
        }
        public void Add(Serie serie)
        {
            if (series != null && serie != null)
            {
                if (!series.Select(s => s.Id).Contains(serie.Id))
                {
                    series.Add(serie);
                }
            }
        }

        public void Delete(Guid id)
        {
            if (series != null)
            {
                if (series.Select(s => s.Id).Contains(id))
                {
                    series.Remove(series.Find(s => s.Id == id));
                }
            }
        }

        public IEnumerable<Serie> GetAll()
        {
            if (series != null)
            {
                return series as IEnumerable<Serie>;
            }

            return null;
        }

        public Serie GetBy(Guid id)
        {
            if (series != null)
            {
                return series.Find(s => s.Id == id);
            }

            return null;
        }

        public Serie GetBy(string name)
        {
            if (series != null)
            {
                return series.Find(s => s.Name.Value == name);
            }

            return null;
        }

        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InProjectDirectory("Series.xml", "Resources", false, out path))
                {
                    series = (List<Serie>)XmlHandler.LoadFrom(path, typeof(List<Serie>));
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Series.xml\", \"Resources\", false, out path) failed");
                }
            }
            catch
            {
                series = new List<Serie>();
            }
        }

        public void Save()
        {
            try
            {
                string path;
                if (TryGetFilePath.InProjectDirectory("Series.xml", "Resources", true, out path))
                {
                    XmlHandler.SaveTo(path, series);
                }
                else
                {
                    throw new Exception("TryGetFilePath.InProjectDirectory(\"Series.xml\", \"Resources\", true, out path) failed");
                }
            }
            catch (Exception innerException)
            {
                throw new Exception("Could not save file", innerException);
            }
        }
    }
}
