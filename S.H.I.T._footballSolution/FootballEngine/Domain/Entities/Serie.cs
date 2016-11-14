using FootballEngine.Domain.ValueObjects;
using FootballEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Serie
    {
        public readonly int NumberOfTeams;
        public readonly int NumberOfMatches;
        public Guid Id { get; set; }
        public GeneralName Name { get; set; }
        public List<Guid> TeamTable { get; set; }
        public List<Guid> MatchTable { get; set; }

        public Serie() { }

        public Serie(GeneralName name, List<Guid> teamTable, List<Guid> matchTable)
        {
            NumberOfTeams = 16;
            NumberOfMatches = 240;
            ValidateInparameters(name, teamTable, matchTable);
            Id = Guid.NewGuid();
            Name = name;
            TeamTable = teamTable;
            MatchTable = matchTable;
        }

        private void ValidateInparameters(GeneralName name, List<Guid> teamTable, List<Guid> matchTable)
        {
            if (name == null)
                throw new Exception($"{nameof(name)} can not be null.");
            try
            {
                GeneralName generalName = new GeneralName(name.Value);
            }
            catch (InvalidNameException e)
            {
                throw e;
            }
            if (teamTable == null)
                throw new Exception($"{nameof(teamTable)} can not be null.");
            if (teamTable.Count != NumberOfTeams)
                throw new Exception($"Number of Guids in {nameof(teamTable)} must be {NumberOfTeams}.");
            if (matchTable == null)
                throw new Exception($"{nameof(matchTable)} can not be null.");
            if (matchTable.Count != NumberOfMatches)
                throw new Exception($"Number of Guids in {nameof(matchTable)} must be {NumberOfMatches}.");
        }
    }
}
