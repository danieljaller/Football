using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Helper
{
    public static class CharUtil
    {
        public static char ToUpper(this char c)
        {
            return Convert.ToChar(c.ToString().ToUpper());
        }
    }
}
