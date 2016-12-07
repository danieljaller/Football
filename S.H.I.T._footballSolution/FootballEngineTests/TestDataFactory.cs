using System;
using System.Collections.Generic;

namespace FootballEngine
{
    public static class TestDataFactory
    {
        public static IEnumerable<Guid> CreateListWithGuids(int numberOfGuids)
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