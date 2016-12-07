using FootballEngine.Domain.Entities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UserApp.Converters
{
    public class SearchResultToStringConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Player))
            {
                var player = (Player)value;
                return player.FullName;
            }
            if (value.GetType() == typeof(Team))
            {
                var team = (Team)value;
                return team.Name;
            }
            if (value.GetType() == typeof(Serie))
            {
                var serie = (Serie)value;
                return serie.Name;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}