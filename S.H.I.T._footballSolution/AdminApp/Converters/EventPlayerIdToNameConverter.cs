using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminApp.Converters
{
    public class EventPlayerIdToNameConverter : IValueConverter
    {
        PlayerService playerService = new PlayerService(new TeamService());
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid playerId = (Guid)value;
            Player player = playerService.GetBy(playerId);
            return player.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
