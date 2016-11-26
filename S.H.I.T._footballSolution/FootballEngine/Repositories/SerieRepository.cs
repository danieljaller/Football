using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FootballEngine.Repositories
{
    public class SerieRepository : IRepository<Serie>
    {
        private readonly string _path;
        private List<Serie> _series;

        private SerieRepository()
        {
            _path = AppDomain.CurrentDomain.BaseDirectory;
            _path = Path.Combine(_path, "Resources");
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            _path = Path.Combine(_path, "Series.xml");
            Load();
        }

        private static SerieRepository _instance;
        public static SerieRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SerieRepository();

                return _instance;
            }
        }
        public void Add(Serie serie)
        {
            if (serie == null)
                return;
            if (!_series.Select(s => s.Id).Contains(serie.Id)) // Should check for name too!
                _series.Add(serie);
        }

        public void Delete(Guid id)
        {
            if (_series.Select(s => s.Id).Contains(id))
            {
                _series.Remove(_series.Find(s => s.Id == id));
            }
        }

        public IEnumerable<Serie> GetAll()
        {
            return _series;
        }

        public Serie GetBy(Guid id)
        {
            return _series.Find(s => s.Id == id);
        }

        public Serie GetBy(string name)
        {
            return _series.Find(s => s.Name.Value == name);
        }

        public void Load()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Series.xml", "Resources", false, out path))
                //{
                _series = (List<Serie>)XmlHandler.LoadFrom(_path, typeof(List<Serie>));
                //}

            }
            catch (LoadFailedException)
            {
                _series = new List<Serie>();
            }
        }

        public void Save()
        {
            try
            {
                //if (TryGetFilePath.InSolutionDirectory("Series.xml", "Resources", true, out path))
                //{
                XmlHandler.SaveTo(_path, _series);
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
