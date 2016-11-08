using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper
{
    class SerieAndMatchGenerator
    {
        private MatchService matchService = new MatchService();

        public List<Guid> SerieGenerator(List<Guid> teams)
        {
            var pairings = teams.Join(teams,
                a => a,
                b => b,
                (a, b) => new { team1Id = a, team2Id = b }).Where(m => m.team1Id != m.team2Id);

            List<Guid> matches = new List<Guid>();
            DateTime lastDate = DateTime.Today;
            int matchNumberCheck = 0;

            foreach(var pairing in pairings)
            {
                DateTime date;
                if (matchNumberCheck < 4)
                {
                    matchNumberCheck++;
                }

                else
                {
                    lastDate = lastDate.AddDays(1);
                    matchNumberCheck = 0;
                }

                if (lastDate.DayOfWeek != DayOfWeek.Saturday && lastDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    lastDate = lastDate.AddDays((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek);
                }

                date = lastDate;

                Match match = new Match(date, pairing.team1Id, pairing.team2Id);
                matchService.Add(match);
                matches.Add(match.Id);
            }

            return matches;
        }

        
    }
}
