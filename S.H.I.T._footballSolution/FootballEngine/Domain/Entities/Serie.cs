using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace FootballEngine.Domain.Entities
{
    public class Serie
    {
        public static readonly int NumberOfTeams = 16;
        public static readonly int NumberOfMatches = 240;
        public Guid Id { get; set; }
        public GeneralName Name { get; set; }
        public List<Guid> TeamTable { get; set; }
        public List<Guid> MatchTable { get; set; }

        public Serie() { }

        public Serie(GeneralName name, List<Guid> teamTable, List<Guid> matchTable)
        {
            ValidateInparameters(name, teamTable, matchTable);
            Id = Guid.NewGuid();
            Name = name;
            TeamTable = teamTable;
            MatchTable = matchTable;
        }

        private void ValidateInparameters(GeneralName name, List<Guid> teamTable, List<Guid> matchTable)
        {
            if (name == null)
                throw new ArgumentNullException($"{nameof(name)} can not be null.");
            try
            {
                GeneralName generalName = new GeneralName(name.Value);
            }
            catch (ArgumentNullException ANE)
            {
                throw ANE;
            }
            catch (ArgumentException AE)
            {
                throw AE;
            }
            if (teamTable == null)
                throw new ArgumentNullException($"{nameof(teamTable)} can not be null.");
            if (teamTable.Count != NumberOfTeams)
                throw new ArgumentOutOfRangeException($"Number of Guids in {nameof(teamTable)} must be {NumberOfTeams}.");
            if (matchTable == null)
                throw new ArgumentNullException($"{nameof(matchTable)} can not be null.");
            if (matchTable.Count != NumberOfMatches)
                throw new ArgumentOutOfRangeException($"Number of Guids in {nameof(matchTable)} must be {NumberOfMatches}.");
        }
    }
}
