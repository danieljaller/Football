using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UserApp.Converters
{
    public class ObjectToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "null";
            if (value is Match)
                return $"Match {(value as Match).Date.ToShortDateString()}";
            if (value is Player)
                return (value as Player).FullName;
            if (value is Serie)
                return (value as Serie).Name;
            if (value is Team)
                return (value as Team).Name;
            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
