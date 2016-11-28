using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class MatchMinute
    {
        public int Value { get; set; }
        public int matchLength{ get; set; }
        public static int maxLenght
        {
            get { return 120; }
        }

        public MatchMinute() { }

        public MatchMinute(int minute)
        {
            matchLength = 90;
            if (IsValidMinute(minute))
                Value = minute;
        }

        public MatchMinute(int minute, int length)
        {
            if (IsValidLength(length))
                matchLength = length;
            else
                matchLength = 90;

            if (IsValidMinute(minute))
                Value = minute;
        }


        public static bool IsValidLength(int length)
        {
            if (length >= 90 && length <= maxLenght)
                return true;
            else
                return false;
        }

        public static bool IsValidMinute(int minute)
        {
            if (minute <= 0)
                throw new ArgumentException("A match minute can not be equal to or less than 0");

            if (minute > maxLenght)
                throw new ArgumentException("A match minute can not be larger than the length of the match");

            return true;
           
        }

        public static bool TryParse(int minute, out MatchMinute result)
        {
           
                try
                {
                    result = new MatchMinute(minute);
                    return true;
                }
                catch (ArgumentException)
                {
                    result = null;
                    return false;
                }
            }
           
        public override string ToString()
        {
            return Value.ToString();
        }
        }

    }

