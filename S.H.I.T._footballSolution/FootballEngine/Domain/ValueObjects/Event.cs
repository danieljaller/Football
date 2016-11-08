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
        public Guid PlayerId;
        public uint TimeOfEvent;

        public Event()
        { }

        public Event(Guid playerId, uint timeOfEvent)
        {
            PlayerId = playerId;
            TimeOfEvent = timeOfEvent;
        }
    }
}
