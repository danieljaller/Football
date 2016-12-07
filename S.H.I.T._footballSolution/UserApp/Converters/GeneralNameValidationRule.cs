using FootballEngine.Domain.ValueObjects;
using System.Globalization;
using System.Windows.Controls;

namespace UserApp.Converters
{
    internal class GeneralNameValidationRule : ValidationRule
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