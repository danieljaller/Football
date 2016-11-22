using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Helper
{
    public static class StringUtil
    {
        public static string FirstToUpper(this string str)
        {
            return str[0].ToUpper() + str.Substring(1);
        }
    }
}
