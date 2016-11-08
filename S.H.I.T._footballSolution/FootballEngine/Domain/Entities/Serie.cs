using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Serie
    {
        public Guid Id { get; set; }
        public GeneralName Name {get; set;}
        public List<Team> TeamTable;
        public List<Match> MatchTable;

        public Serie() {}

        public Serie(GeneralName name)
        {
            Id = Guid.NewGuid();
            Name = name;
            TeamTable = new List<Team>();
            MatchTable = new List<Match>();
        }
    }
}
