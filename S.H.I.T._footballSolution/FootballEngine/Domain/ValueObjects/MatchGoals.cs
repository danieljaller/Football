using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class MatchGoals
    {
        public int Value { get; set; }
        public MatchGoals() { }

        public MatchGoals(int goals)
        {
            if (goals >= 0 && goals <=50)
                Value = goals;
            else
                throw new ArgumentOutOfRangeException("Invalid number of goals");
        }

        public static bool TryParse(int goals, out MatchGoals result)
        {
            try
            {
                result = new MatchGoals(goals);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                result = null;
                return false;
            }
        }
    }
}
