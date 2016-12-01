using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace FootballEngine.Domain.Entities
{
    public class Team
    {
        public Team()
        {

        }
        public Team(GeneralName name, GeneralName homeArena)
        {
            if (IsValidParameters(name, homeArena))
            {
                Id = Guid.NewGuid();
                Name = name;
                HomeArena = homeArena;
                PlayerIds = new HashSet<Guid>();
                MatchIds = new HashSet<Guid>();
                SerieIds = new HashSet<Guid>();
                Wins = new HashSet<Guid>();
                Losses = new HashSet<Guid>();
                Ties = new HashSet<Guid>();
                GoalsFor = 0;
                GoalsAgainst = 0;
                MatchesPlayed = 0;
            }
        }
        private bool IsValidParameters(GeneralName name, GeneralName homeArena)
        {
            if(name == null)
            { throw new ArgumentNullException($"{nameof(name)} cannot be null"); }
            if (homeArena == null)
            { throw new ArgumentNullException($"{nameof(homeArena)} cannot be null"); }
            return true;
        }
        public Guid Id { get; set; }
        public GeneralName Name { get; set; }
        public GeneralName HomeArena { get; set; }
        public HashSet<Guid> Wins { get; set; }
        public HashSet<Guid> Losses { get; set; }
        public HashSet<Guid> Ties { get; set; }
        public int Points
        {
            get
            {
                return (Wins.Count * 3) + (Ties.Count * 1);
            }
        }
        public int MatchesPlayed
        {
            get
            {
                return Wins.Count + Losses.Count + Ties.Count;
            }
        }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public HashSet<Guid> PlayerIds { get; set; }
        public HashSet<Guid> MatchIds { get; set; }
        public HashSet<Guid> SerieIds { get; set; }
        public int GoalDifference
        {
            get
            {
                return GoalsFor - GoalsAgainst;
            }
        }
    }
}






