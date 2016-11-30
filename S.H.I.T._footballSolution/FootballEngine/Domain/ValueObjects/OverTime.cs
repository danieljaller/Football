using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class OverTime
    {
        public int Value { get; set; }
        public static int MaxValue { get { return 30; } }

        public OverTime() { }

        public OverTime(int overTime)
        {
            if (0 > overTime || overTime > MaxValue)
                throw new ArgumentOutOfRangeException($"{nameof(overTime)} must be between 0 and {MaxValue}");

            Value = overTime;
        }
    }
}
