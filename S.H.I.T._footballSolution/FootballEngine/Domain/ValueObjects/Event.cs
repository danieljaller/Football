using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            PlayerId = playerId;
            TimeOfEvent = timeOfEvent;
        }
    }
}
