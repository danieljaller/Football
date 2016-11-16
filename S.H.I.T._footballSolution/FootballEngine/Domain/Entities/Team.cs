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
            SeriesIds = new List<Guid>();
            Wins = 0;
            Losses = 0;
            Ties = 0;
            GoalsFor = 0;
            GoalsAgainst = 0;
            GoalDifference = 0;
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
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public List<Guid> PlayerIds { get; set; }
        public List<Guid> MatchIds { get; set; }
        public List<Guid> SeriesIds { get; set; }
        public int GoalDifference { get; set; }
    }
}






