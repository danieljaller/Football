using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Factories
{
    public static class SerieFactory
    {
        // IEnumerable<IEnumerable<Guid>> teamTables and IEnumerable<IEnumerable<Match>> matchTables as inparameters in Create()?
        public static IEnumerable<Serie> Create(int amount)
        {
            amount = (amount > 500) ? 500 : amount;
            List<Serie> series = new List<Serie>();

            for (int i = 1; i <= amount; i++)
            {
                series.Add(new Serie(new GeneralName($"Serie {i}")));
            }

            return series;
        }
    }
}
