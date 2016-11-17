using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Helper
{
    public static class IntUtil
    {
        //http://stackoverflow.com/questions/2729752/converting-numbers-in-to-words-c-sharp
        public static string NumberToWords(this int number, bool lowerCase = true, bool minusAsWord = true)
        {
            if (number == 0)
                return ((lowerCase) ? "z" : "Z") + "ero";

            if (number < 0)
                return ((minusAsWord) ? "minus " : "-") + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + ((lowerCase) ? " m" : " M") + "illion "; //" million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + ((lowerCase) ? " t" : " T") + "housand ";   //" thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + ((lowerCase) ? " h" : " H") + "undred "; //" hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                if (!lowerCase)
                {
                    string[] unitsMapUpperCase = new string[unitsMap.Length];
                    for (int i = 0; i < unitsMap.Length; i++)
                    {
                        unitsMapUpperCase[i] = unitsMap[i].Replace(unitsMap[i][0], unitsMap[i][0].ToUpper());
                    }
                    unitsMap = unitsMapUpperCase;
                }
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                if (!lowerCase)
                {
                    string[] tensMapUppercase = new string[tensMap.Length];
                    for (int i = 0; i < tensMap.Length; i++)
                    {
                        tensMapUppercase[i] = tensMap[i].Replace(tensMap[i][0], tensMap[i][0].ToUpper());
                    }
                    tensMap = tensMapUppercase;
                }

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
