using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects
{
    public class Exchange
    {
        public Guid PlayerOutId { get; set; }
        public Guid PlayerInId { get; set; }
        public uint TimeOfExchange { get; set; }

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
