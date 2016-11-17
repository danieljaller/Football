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
        string[] directories = new string[2] { "FootballEngine", "Resources" };
        public void Load()
        {
            string path;
            try
            {
                if (TryGetFilePath.InProjectDirectory("Series.xml", directories, false, out path))
                {
                    series = (List<Serie>)XmlHandler.LoadFrom(path, typeof(List<Serie>));
                }
                
            }
            catch(LoadFailedException l)
            {
                throw l;
            }
        }

        public void Save()
        {
            try
            {
                string path;
                if (TryGetFilePath.InProjectDirectory("Series.xml", directories, true, out path))
                {
                    XmlHandler.SaveTo(path, series);
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
