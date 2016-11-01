using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    class TeamAndArenaName
    {
        public TeamAndArenaName(string name)
        {
            if (IsValidName(name))
            {
                _value = name;
            }
            else
            {
                throw new Exception("Invalid TeamName");
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
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
                if (name.Length < MaxLenght)
                {
                    foreach (char character in name)
                    {
                        if (char.IsLetterOrDigit(character) || character == '-' || character == ' ')
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool TryParse(string name, out TeamAndArenaName result)
        {
            try
            {
                result = new TeamAndArenaName(name);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }

        public static int MaxLenght
        {
            get { return 25; }
        }
        public static string AcceptedChars
        {
            get { return "Endast bokstäver och siffror"; }
        }
    }
}
