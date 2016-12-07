using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class MatchMinute
    {
        public int Value { get; set; }
        public int MatchLength { get; set; }
        public static int MaxValue { get { return 120; } }

        public MatchMinute()
        {
        }

        public MatchMinute(int minute)
            : this(minute, 90)
        { }

        public MatchMinute(int minute, int length)
        {
            if (IsValidInparameters(minute, length))
            {
                MatchLength = length;
                Value = minute;
            }
        }

        private bool IsValidInparameters(int minute, int length)
        {
            if (minute < 0)
                throw new ArgumentOutOfRangeException("A match minute can not be less than 0");

            if (minute > MaxValue)
                throw new ArgumentOutOfRangeException("A match minute can not be larger than the length of the match");

            if (90 > length && length > MaxValue)
                throw new ArgumentOutOfRangeException($"{nameof(length)} must be between 90 and {MaxValue}.");

            return true;
        }

        public static bool IsValidMinute(int minute)
        {
            if (minute >= 90 && minute <= MaxValue)
                return true;

            return false;
        }

        public static bool IsValidMatchLength(int length)
        {
            if (length >= 90 && length <= MaxValue)
                return true;

            return false;
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

        public static bool TryParse(string stringMinute, out MatchMinute result)
        {
            int minute;
            if (int.TryParse(stringMinute, out minute))
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
            result = null;
            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}