using FootballEngine.Domain.Entities;
using FootballEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Add(Serie entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Serie entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Serie> GetAll()
        {
            throw new NotImplementedException();
        }

        public Serie GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
