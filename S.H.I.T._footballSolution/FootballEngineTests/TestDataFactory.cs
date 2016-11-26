using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine
{
    public static class TestDataFactory
    {
        public static List<Guid> CreateListWithGuids(int numberOfGuids)
        {
            List<Guid> guidList = new List<Guid>();
            for (int i = 0; i < numberOfGuids; i++)
            {
                guidList.Add(Guid.NewGuid());
            }
            return guidList;
        }
    }
}
