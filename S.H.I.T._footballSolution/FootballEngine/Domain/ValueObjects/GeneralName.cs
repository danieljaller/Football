using FootballEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class GeneralName
    {
        public string Value { get; set; }
        public static int MaxLenght
        {
            get { return 25; }
        }
        public GeneralName()
        {

        }
        public GeneralName(string name)
        {
            if (IsValidName(name))
            {
                Value = name;
            }
            else
            {
                throw new InvalidNameException("The name is either null, empty or consists only of white-space characters.");
            }
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (name.Length > MaxLenght)
            {
                throw new InvalidNameException($"Name is too long. Maximum length is {MaxLenght} characters.");
            }

            foreach (char character in name)
            {
                if (char.IsLetterOrDigit(character) || character == '-' || character == ' ')
                {
                    continue;
                }
                else
                {
                    throw new InvalidNameException("Name contains illegal characters. Can only contain letters, digits, '-' and white-spaces.");
                }
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
            catch (InvalidNameException)
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
