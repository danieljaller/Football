﻿using FootballEngine.Domain.Entities;
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
            if (TryGetFilePath.InProjectDirectory("Series.xml", "Resources", false, out path))
            {
                try
                {
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Serie>));
                        series = xmlSerializer.Deserialize(stream) as List<Serie>;
                    }
                }
                catch
                {
                    series = new List<Serie>();
                }
            }
        }

        public void Save()
        {
            string path;
            if (TryGetFilePath.InProjectDirectory("Series.xml", "Resources", true, out path))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Serie>));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        xmlSerializer.Serialize(stream, series);
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
