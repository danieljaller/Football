using System;

namespace FootballEngine.Helper
{
    public static class CharUtil
    {
        public static char ToUpper(this char c)
        {
            return Convert.ToChar(c.ToString().ToUpper());
        }
    }
}