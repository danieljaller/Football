using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class GeneralName
    {
        public GeneralName()
        {

        }
        public GeneralName(string name)
        {
            if (IsValidName(name))
            {
                //_value = name;
                Value = name;
            }
            else
            {
                throw new Exception("Invalid Name");
            }
        }
        public string Value { get; set; }
        //private string _value;
        //public string Value
        //{
        //    get { return _value; }
        //}
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
                        if (!char.IsLetterOrDigit(character) || character != '-' || character != ' ')
                        {
                            return false;
                        }
                    }
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
