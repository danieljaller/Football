using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    class Team
    {
        public Team(string name, string homeArena)
        {
            Id = new Guid();
            Name = new TeamAndArenaName(name);
            HomeArena = new TeamAndArenaName(homeArena);
        }
        public Guid Id { get; set; }
        public TeamAndArenaName Name { get; set; }
        public TeamAndArenaName HomeArena { get; set; }
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
        List<Guid> PlayerIds;
        List<Guid> MatchIds;

        public int GoalDifferens { get; set; }
        
           
        
    }
}
       


