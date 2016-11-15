using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdminApp
{
    public class TempMatchToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempMatchList = (List<TempMatch>)value;
            var schedule = new StringBuilder();
            foreach (var tempMatch in tempMatchList)
            {
                schedule.AppendLine($"{tempMatch.Date}          {tempMatch.Team1}                 {tempMatch.Team2}             {tempMatch.Location}             {tempMatch.Result}");
            }
            return schedule;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
