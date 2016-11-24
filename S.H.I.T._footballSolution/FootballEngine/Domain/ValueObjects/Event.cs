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
        public uint TimeOfEvent { get; set; }
        public static readonly int TimeOfEventMaximumValue = 110;

        public Event()
        { }

        public Event(Guid playerId, uint timeOfEvent)
        {
            if (IsValidParameter(playerId, timeOfEvent))
            { 
            PlayerId = playerId;
            TimeOfEvent = timeOfEvent;
            }
        }

        private bool IsValidParameter(Guid playerId, uint timeOfEvent)
        {
            if(timeOfEvent > TimeOfEventMaximumValue)
            { throw new ArgumentOutOfRangeException($"Value must not be greater than {TimeOfEventMaximumValue}"); }
            if(Guid.Empty == playerId)
            { throw new ArgumentException("PlayerId cannot be an empti Guid"); }
            return true;
        }
    }
}
