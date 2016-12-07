﻿using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System.Globalization;
using System.Windows.Controls;

namespace AdminApp.Converters
{
    internal class GeneralNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            GeneralName generalName;
            if (GeneralName.TryParse(value.ToString(), out generalName) && !ServiceLocator.Instance.TeamService.NameExist(generalName.Value))
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