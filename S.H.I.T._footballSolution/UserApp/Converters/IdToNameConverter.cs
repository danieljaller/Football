using FootballEngine.Domain.Entities;
using FootballEngine.Helper;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UserApp.Converters
{
    public class IdToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid playerId = (Guid)value;
            Player player = ServiceLocator.Instance.PlayerService.GetBy(playerId);
            return player.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}