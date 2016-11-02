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
        public Team(string name, string homeArena)
        {
            Id = new Guid();
            Name = new GeneralName(name);
            HomeArena = new GeneralName(homeArena);
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
        List<Guid> PlayerIds;
        List<Guid> MatchIds;

        public int GoalDifferens { get; set; }
    }
}
        
           
        
       


