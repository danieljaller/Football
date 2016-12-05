using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static FootballEngine.Domain.Entities.Player;

namespace AdminApp.Converters
{
    class StatusToSwedishStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.GetType() == typeof(Status))
                return ((Status)value).ToSwedishString();

            return "?";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
