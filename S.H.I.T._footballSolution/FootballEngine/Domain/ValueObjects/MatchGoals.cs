using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    class MatchGoals
    {
        public int Value { get; set; }
        public MatchGoals() { }

        public MatchGoals(int goals)
        {
            if (goals >= 0 && goals <=50)
            {
                Value = goals;
            }
            else
            {
                throw new Exception("Invalid number of goals");
            }
        }

        public static bool TryParse(int goals, out MatchGoals result)
        {
            try
            {
                result = new MatchGoals(goals);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
        }
    }
}
