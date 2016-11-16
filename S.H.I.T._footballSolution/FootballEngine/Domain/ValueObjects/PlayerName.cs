using FootballEngine.Exceptions;
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
        public string Value { get; set; }
        public static int MaxLenght
        {
            get { return 25; }
        }
        public static int MinLenght
        {
            get { return 2; }
        }
        public PlayerName() { }
        public PlayerName(string name)
        {
            if (IsValidName(name))
                Value = name;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidNameException("The name is either null, empty or consists only of white-space characters.");

            if (name.StartsWith(" "))
                throw new InvalidNameException("A name can not start with ' '.");

            if (name.EndsWith(" "))
                throw new InvalidNameException("A name can not end with ' '.");

            if (name.Length > MaxLenght)
                throw new InvalidNameException($"Name is too long. Maximum length is {MaxLenght} characters.");

            if (name.Length < MinLenght)
                throw new InvalidNameException($"Name is too Short. Minimum length is {MinLenght} characters.");

            foreach (char character in name)
            {
                if (char.IsLetter(character) || character == '-' || character == ' ')
                    continue;
                else
                    throw new InvalidNameException("Name contains illegal characters. Can only contain letters, '-' and white-spaces.");
            }

            return true;
            //return Regex.IsMatch(name, @"\A[a-zA-Z´¨åäöÅÄÖ]+\s?-?[a-zA-Z´¨åäöÅÄÖ]*\z", RegexOptions.IgnoreCase);
        }

        public static bool TryParse(string name, out PlayerName result)
        {
            try
            {
                result = new PlayerName(name);
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

