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
    public class GeneralNameValitationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            GeneralName generalName;
            if (GeneralName.TryParse(value.ToString(), out generalName))
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
