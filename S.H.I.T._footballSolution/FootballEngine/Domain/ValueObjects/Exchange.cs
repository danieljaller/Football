using System;

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
