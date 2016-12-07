using FootballEngine.Helper;
using System;
using System.Globalization;
using System.Windows.Data;
using static FootballEngine.Domain.Entities.Player;

namespace UserApp.Converters
{
    public class StatusToSwedishStringConverter : IValueConverter
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