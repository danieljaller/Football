using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class PlayerName
    {
        public PlayerName() { }
        public PlayerName(string name)
        {
            if (ValidName(name))
                Value = name;
            else
            {
                throw new ArgumentException("Not a valid Name");
            }
        }

        public static bool TryParse(string name, out PlayerName result)
        {
            try
            {
                result = new PlayerName(name);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }
        public static bool ValidName(string name)
        {
            return Regex.IsMatch(name, @"\A[a-zA-Z´¨åäöÅÄÖ]+\s?-?[a-zA-Z´¨åäöÅÄÖ]*\z", RegexOptions.IgnoreCase);
        }
        public string Value { get; set; }
    }
}

