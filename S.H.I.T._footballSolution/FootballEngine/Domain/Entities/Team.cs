using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Team
    {
        public Team()
        {

        }
        public Team(GeneralName name, GeneralName homeArena)
        {
            Id = Guid.NewGuid();
            Name = name;
            HomeArena = homeArena;
            PlayerIds = new List<Guid>();
            MatchIds = new List<Guid>();
        }
        public Guid Id { get; set; }
        public GeneralName Name { get; set; }
        public GeneralName HomeArena { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int Points
        {
            get
            {
                return (Wins * 3) + (Ties * 1);
            }
        }
        public List<Guid> PlayerIds { get; set; }
        public List<Guid> MatchIds { get; set; }

        public int GoalDifferens { get; set; }
    }
}






