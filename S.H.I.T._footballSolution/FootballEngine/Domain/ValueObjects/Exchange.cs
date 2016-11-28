using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class Exchange
    {
        public Guid PlayerOutId;
        public Guid PlayerInId;
        public MatchMinute TimeOfExchange;

        public Exchange()
        { }

        public Exchange(Guid playerOutId, Guid playerInId, MatchMinute timeOfExchange)
        {
            PlayerOutId = playerOutId;
            PlayerInId = playerInId;
            TimeOfExchange = timeOfExchange;
        }
    }
}
