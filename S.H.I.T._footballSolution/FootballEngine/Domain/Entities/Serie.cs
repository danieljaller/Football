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
        public List<Guid> TeamTable;
        public List<Guid> MatchTable;

        public Serie() {}

        public Serie(GeneralName serieName)
                :this(Guid.NewGuid(), serieName)
        {
        }

        public Serie(Guid id, GeneralName seriename)
        {
            Id = id;
            SerieName = seriename;
            TeamTable = new List<Guid>();
            MatchTable = new List<Guid>();
        }
    }
}
