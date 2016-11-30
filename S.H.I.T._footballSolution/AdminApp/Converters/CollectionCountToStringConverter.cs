using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminApp.Converters
{
    class CollectionCountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(List<Guid>))
            {
                List<Guid> result = (List<Guid>)value;
                return result.Count().ToString();              
            }
            else if (value.GetType() == typeof(List<Event>))
            {
                List<Event> result = (List<Event>)value;
                return result.Count().ToString();
            }
            else if (value.GetType() == typeof(HashSet<Event>))
            {
                HashSet<Event> result = (HashSet<Event>)value;
                return result.Count().ToString();
            }
            else if (value.GetType() == typeof(HashSet<Guid>))
            {
                HashSet<Guid> result = (HashSet<Guid>)value;
                return result.Count().ToString();
            }
            else
            {
                return "?";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
