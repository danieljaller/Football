using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Factories
{
    public static class MatchFactory
    {
        public static IEnumerable<Match> Create(IEnumerable<DateTime> dates, IEnumerable<Guid> homeTeamIds, IEnumerable<Guid> visitorTeamIds)
        {
            if (dates == null)
                throw new Exception($"{nameof(dates)} is null");
            if (homeTeamIds == null)
                throw new Exception($"{nameof(homeTeamIds)} is null");
            if (visitorTeamIds == null)
                throw new Exception($"{nameof(visitorTeamIds)} is null");

            if (dates.Count() != homeTeamIds.Count())
                throw new Exception($"The {nameof(dates)} count is not equal to the {nameof(homeTeamIds)} count");
            if (homeTeamIds.Count() != visitorTeamIds.Count())
                throw new Exception($"The {nameof(homeTeamIds)} count is not equal to the {nameof(visitorTeamIds)} count");
            if (dates.Count() == 0 && homeTeamIds.Count() == 0 && visitorTeamIds.Count() == 0)
                throw new Exception("Can not create matches whitout any dates or teams", 
                    new Exception($"{nameof(homeTeamIds)}, {nameof(homeTeamIds)} and {nameof(visitorTeamIds)} are all empty"));

            List<Match> matches = new List<Match>();
            for (int i = 0; i < dates.Count(); i++)
            {
                Match match = new Match(dates.ElementAt(i), homeTeamIds.ElementAt(i), visitorTeamIds.ElementAt(i));
                matches.Add(match);
            }

            return matches;
        }
    }
}
