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
                throw new Exception("Invalid Name.");
            }
        }   
        
        public static bool IsValidName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                while (true)
                {
                    if (!name.StartsWith(" "))
                    {
                        break;
                    }

                    name = name.Substring(1);
                }
                if (name.Length <= MaxLenght)
                {
                    foreach (char character in name)
                    {
                        if (char.IsLetterOrDigit(character) || character == '-' || character == ' ')
                        {
                            continue;
                        }
                        else
                        {
                            throw new Exception("Name contains illegal characters. Can only contain letters, digits, \"-\" and white spaces");
                        }
                    }
                }
                else
                {
                    throw new Exception("Name is too long. Enter 1-25 characters.");
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
