using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper
{
    public static class IEnumerableUtil
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var c = new ObservableCollection<T>();
            foreach (var e in collection)
                c.Add(e);
            return c;
        }
    }
}
