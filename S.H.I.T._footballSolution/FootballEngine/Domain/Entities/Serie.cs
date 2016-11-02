using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    class Serie
    {
        public Guid Id { get; set; }
        public GeneralName SerieName {get; set;}
        public List<Team> TeamTable;
        public List<Match> MatchTable;

        public Serie() {}

        public Serie(GeneralName serieName)
                :this(Guid.NewGuid(), serieName)
        {
        }

        public Serie(Guid id, GeneralName seriename)
        {
            Id = id;
            SerieName = seriename;
            TeamTable = new List<Team>();
            MatchTable = new List<Match>();
        }
    }
}
