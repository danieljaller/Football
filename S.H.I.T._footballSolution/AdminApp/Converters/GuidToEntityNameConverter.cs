using FootballEngine.Helper;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AdminApp.Converters
{
    public class GuidToEntityNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Guid))
            {
                string teamName = ServiceLocator.Instance.TeamService.GetBy((Guid)value).Name.Value;
                if (teamName != null)
                {
                    return teamName;
                }

                string playerName = ServiceLocator.Instance.PlayerService.GetBy((Guid)value).FullName;
                if (playerName != null)
                {
                    return playerName;
                }

                string serieName = ServiceLocator.Instance.SerieService.GetBy((Guid)value).Name.Value;
                if (serieName != null)
                {
                    return serieName;
                }
            }
            return "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}