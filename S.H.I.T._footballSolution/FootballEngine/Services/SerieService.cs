﻿using FootballEngine.Domain.Entities;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Services
{
    public class SerieService : IService<Serie>
    {
        private readonly SerieRepository _serieRepository = SerieRepository.Instance;

        private static readonly object CreationLock = new object();
        private static SerieService _instance;

        internal static SerieService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (CreationLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SerieService();
                        }
                    }
                }

                return _instance;
            }
        }

        internal SerieService() { }

        public void Add(Serie serie)
        {
            _serieRepository.Add(serie);
        }

        public void AddRange(IEnumerable<Serie> series)
        {
            _serieRepository.AddRange(series);
        }

        public void Delete(Guid id)
        {
            _serieRepository.Delete(id);
        }

        public IEnumerable<Serie> GetAll()
        {
            return _serieRepository.GetAll();
        }

        public Serie GetBy(string name)
        {
            return _serieRepository.GetBy(name);
        }

        public Serie GetBy(Guid id)
        {
            return _serieRepository.GetBy(id);
        }

        public void Save()
        {
            _serieRepository.Save();
        }

        public bool NameExist(string name)
        {
            if (_serieRepository.GetAll().Select(serie => serie.Name.Value).Contains(name))
                return true;

            return false;
        }
    }
}
