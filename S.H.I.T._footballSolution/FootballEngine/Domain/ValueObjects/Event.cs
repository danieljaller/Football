using System;

namespace FootballEngine.Domain.ValueObjects
{
    public class Event
    {
        public Guid PlayerId { get; set; }
        public MatchMinute TimeOfEvent { get; set; }

        public Event()
        { }

        public Event(Guid playerId, MatchMinute timeOfEvent)
        {
            if (playerId == Guid.Empty)
                throw new ArgumentException($"{nameof(playerId)} cannot be an empty Guid.");

            PlayerId = playerId;
            TimeOfEvent = timeOfEvent;
        }
    }
}