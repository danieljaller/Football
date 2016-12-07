using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class DateOfBirth
    {
        public static readonly int MaxAge = 100;
        public static readonly DateTime MaxDateOfBirth = DateTime.Now.AddYears(-MaxAge).Date;
        public static readonly int MinAge = 16;
        public static readonly DateTime MinDateOfBirth = DateTime.Now.AddYears(-MinAge).Date;

        public DateTime Value { get; set; }

        public DateOfBirth()
        {
        }

        public DateOfBirth(DateTime dateOfBirth)
        {
            if (IsValidInparameter(dateOfBirth))
                Value = dateOfBirth;
        }

        private bool IsValidInparameter(DateTime dateOfBirth)
        {
            if (dateOfBirth.Date > MinDateOfBirth.Date && dateOfBirth.Date < MaxDateOfBirth.Date)
                throw new ArgumentException(
                    $"{nameof(dateOfBirth)} must be between {MinDateOfBirth.ToShortDateString()} and {MaxDateOfBirth.ToShortDateString()}");

            return true;
        }

        public static bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            try
            {
                new DateOfBirth(dateOfBirth);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        public static bool TryParse(DateTime date, out DateOfBirth dateOfBirth)
        {
            try
            {
                dateOfBirth = new DateOfBirth(date);
                return true;
            }
            catch (Exception)
            {
                dateOfBirth = null;
                return false;
            }
        }

        public static bool TryParse(string date, out DateOfBirth dateOfBirth)
        {
            if (date.Length == 8)
            {
                int tmp;
                if (int.TryParse(date, out tmp))
                {
                    date = date.Insert(6, "-");
                    date = date.Insert(4, "-");
                }
            }
            DateTime _date;
            if (DateTime.TryParse(date, out _date))
            {
                try
                {
                    dateOfBirth = new DateOfBirth(_date);
                    return true;
                }
                catch (Exception)
                {
                    dateOfBirth = null;
                    return false;
                }
            }

            dateOfBirth = null;
            return false;
        }

        public override string ToString()
        {
            return Value.ToShortDateString();
        }
    }
}