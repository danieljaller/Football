using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class MatchDate
    {
        private readonly DateTime defaultDate;
        public DateTime MaxDate { get { return defaultDate.AddMonths(1); } }
        public DateTime MinDate { get { return defaultDate.AddMonths(-1); } }
        public DateTime Value { get; set; }

        public MatchDate()
        {
        }

        public MatchDate(DateTime matchDate)
        {
            if (matchDate.Date < DateTime.Now.Date)
                throw new ArgumentOutOfRangeException("Date cannot be earlier than today.");

            Value = matchDate;
            defaultDate = matchDate;
        }

        public void EditMatchDate(DateTime newMatchDate)
        {
            if (newMatchDate.Date < MinDate.Date)
                throw new ArgumentOutOfRangeException($"Date cannot be edited to earlier than {MinDate.ToShortDateString()}");
            if (newMatchDate.Date > MaxDate.Date)
                throw new ArgumentOutOfRangeException($"Date cannot be edited to later than {MaxDate.ToShortDateString()}");

            Value = newMatchDate;
        }

        public override string ToString()
        {
            return Value.ToShortDateString();
        }
    }
}