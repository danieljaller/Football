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
    }
}
