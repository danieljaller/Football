using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper
{
    public class SerieAndMatchGenerator
    {
        private MatchService matchService = new MatchService();
        private Guid[] pairing;
        private DateTime date;
        private Match match;
        private DateTime latestDate;
        public List<Guid> matches;
        

        public List<Guid> SerieGenerator(List<Guid> teams, DateTime startDate)
        {
            latestDate = startDate.AddDays(-1);
            matches = new List<Guid>();

            for (int i = 0; i < 2*(teams.Count - 1); i++)
            {
                RoundGenerator(teams, i);
                teams = ListShuffler(teams);
            }
            return matches;
        }

        private void RoundGenerator(List<Guid> teams, int i)
        {
            for (int j = 0; j < teams.Count / 2; j++)
            {
                pairing = new Guid[2];
                if (i < teams.Count / 2)
                {
                    pairing[0] = teams[j];
                    pairing[1] = teams[teams.Count - 1 - j];
                }
                else
                {
                    pairing[1] = teams[j];
                    pairing[0] = teams[teams.Count - 1 - j];
                }

                if (j == teams.Count / 4 || j == 0)
                {
                    latestDate = latestDate.AddDays(1);
                }

                if (latestDate.DayOfWeek != DayOfWeek.Saturday && latestDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    latestDate = latestDate.AddDays((int)DayOfWeek.Saturday - (int)latestDate.DayOfWeek);
                }

                date = latestDate;

                match = new Match(date, pairing[0], pairing[1]);
                matchService.Add(match);
                matches.Add(match.Id);
            }
        }

        private List<Guid> ListShuffler(List<Guid> teams)
        {
            var tempTeams = new List<Guid>();
            for (int j = 0; j < teams.Count; j++)
            {
                if (j == 0)
                {
                    tempTeams.Add(teams[0]);
                }
                else if (j == teams.Count - 1)
                {
                    tempTeams.Add(teams[1]);
                }
                else
                {
                    tempTeams.Add(teams[j + 1]);
                }
            }
            return teams = tempTeams;
        }

    }
}

