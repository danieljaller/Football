using System.Linq;
using System.Text;

namespace FootballEngine.Helper
{
    public static class StringUtil
    {
        public static bool Contains(this string str, string str2, bool ignoreCase)
        {
            str = (ignoreCase) ? str.ToLower() : str;
            str2 = (ignoreCase) ? str2.ToLower() : str2;
            return str.Contains(str2);
        }

        public static bool ContainsOnlyDigits(this string str)
        {
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                    continue;

                return false;
            }

            return true;
        }

        public static string FirstToUpper(this string str)
        {
            return str[0].ToUpper() + str.Substring(1);
        }

        public static string FirstToUpper(this string str, bool firstLetterInEveryWord)
        {
            str = str.FirstToUpper();
            if (str.Contains(' ') || str.Contains('-'))
            {
                StringBuilder stringBuilder = new StringBuilder(str);
                for (int i = 1; i < str.Length; i++)
                {
                    if ((str[i] == ' ' || str[i] == '-') && (i + 1) < str.Length && char.IsLetter(str[i + 1]))
                    {
                        stringBuilder[i + 1] = str[i + 1].ToUpper();
                        i = ((i + 2) < str.Length) ? (i + 2) : str.Length;
                    }
                }
                return stringBuilder.ToString();
            }
            return str;
        }
    }
}
