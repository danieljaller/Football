using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdminApp.Converters
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            PlayerName playerName;
            if (PlayerName.TryParse(value.ToString(), out playerName))
            {
                return new ValidationResult(true, "");
            }
            else
            {
                return new ValidationResult(false, "Not a valid Name");
            }
        }
    }
}
