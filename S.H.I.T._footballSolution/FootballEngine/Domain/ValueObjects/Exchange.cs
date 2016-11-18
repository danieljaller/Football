using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    class Exchange
    {
        public Guid PlayerOutId;
        public Guid PlayerInId;
        public uint TimeOfExchange;

        public Exchange()
        { }

        public Exchange(Guid playerOutId, Guid playerInId, uint timeOfExchange)
        {
            PlayerOutId = playerOutId;
            PlayerInId = playerInId;
            TimeOfExchange = timeOfExchange;
        }
    }
}
