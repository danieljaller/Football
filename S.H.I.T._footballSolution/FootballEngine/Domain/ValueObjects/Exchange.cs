using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class Exchange
    {
        public Guid PlayerOutId { get; set; }
        public Guid PlayerInId { get; set; }
        public MatchMinute TimeOfExchange;

        public Exchange()
        { }

        public Exchange(Guid playerOutId, Guid playerInId, MatchMinute timeOfExchange)
        {
            if (IsValidInparameters(playerOutId, playerInId, timeOfExchange))
            {
                PlayerOutId = playerOutId;
                PlayerInId = playerInId;
                TimeOfExchange = timeOfExchange;
            }
        }

        private bool IsValidInparameters(Guid playerOutId, Guid playerInId, int timeOfExchange)
        {
            if (playerOutId == Guid.Empty)
                throw new ArgumentException($"{nameof(playerOutId)} cannot be an empty Guid.");
            if (playerInId == Guid.Empty)
                throw new ArgumentException($"{nameof(playerOutId)} cannot be an empty Guid.");
            if (0 > timeOfExchange && timeOfExchange > MatchMinute.MaxValue)
                throw new ArgumentOutOfRangeException($"{nameof(timeOfExchange)} must be between 0 and {MatchMinute.MaxValue}");

            return true;
        }
    }
}
