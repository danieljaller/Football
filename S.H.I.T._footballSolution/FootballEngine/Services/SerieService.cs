using FootballEngine.Domain.Entities;
using FootballEngine.Interfaces;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Services
{
    public class SerieService : IService<Serie>
    {
        private readonly SerieRepository _serieRepository = SerieRepository.Instance;

        public void Add(Serie serie)
        {
            _serieRepository.Add(serie);
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
    }
}
