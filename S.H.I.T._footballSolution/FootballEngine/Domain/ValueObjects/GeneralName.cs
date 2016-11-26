using System;
using FootballEngine.Helper;

namespace FootballEngine.Domain.ValueObjects
{
    public class GeneralName
    {
        public string Value { get; set; }
        public static int MaxLenght
        {
            get { return 25; }
        }
        public static int MinLenght
        {
            get { return 2; }
        }
        public GeneralName() { }
        public GeneralName(string name)
        {
            if (IsValidName(name))
                Value = name;
        }

        private static bool IsValidName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name.Length > MaxLenght)
                throw new ArgumentOutOfRangeException($"{nameof(name)} is too long. Maximum length is {MaxLenght} characters.");

            if (name.Length < MinLenght)
                throw new ArgumentOutOfRangeException($"{nameof(name)} is too Short. Minimum length is {MinLenght} characters.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} is either empty or consists only of white-space characters.");

            if (name.StartsWith(" "))
                throw new ArgumentException($"{nameof(name)} cannot start with a white-space character.");

            if (name.EndsWith(" "))
                throw new ArgumentException($"{nameof(name)} cannot end with a white-space character.");
            
            if (name.ContainsOnlyDigits())
                throw new ArgumentException($"{nameof(name)} cannot only consist of digits.");
            
            foreach (char character in name)
            {
                if (char.IsLetterOrDigit(character) || character == '-' || character == '&' || character == '\'' || character == ' ')
                    continue;

                throw new ArgumentException($"{nameof(name)} contains illegal characters. Can only contain letters, digits, '-', '&', ''' and white-space characters.");
            }

            return true;
        }

        public static bool TryParse(string name, out GeneralName result)
        {
            try
            {
                result = new GeneralName(name);
                return true;
            }
            catch (ArgumentNullException)
            {
                result = null;
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                result = null;
                return false;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
