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
            int numberOfRounds = (teams.Count - 1) * 2;
            Guid[] pairing;
            DateTime lastDate = DateTime.Today;
            //var round = new List<Guid[]>();
            DateTime lastDate = DateTime.Today;
            var matches = new List<Match>();

            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < teams.Count / 2; j++)
                {
                    pairing = new Guid[2];
                    if (i * 2 > teams.Count)
                    {
                        pairing[0] = teams[j];
                        pairing[1] = teams[teams.Count - 1 - j];
                    }
                    else
                    {
                        pairing[1] = teams[j];
                        pairing[0] = teams[teams.Count - 1 - j];
                    }

                    DateGenerator(j, teams, lastDate);
                    Match match = new Match(date, pairing[0], pairing[1]);
                    matches.Add(match);
                }
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
                teams = tempTeams;
            }

            private DateTime DateGenerator(int j, List<Guid> teams, out DateTime lastDate) { 
                DateTime date;
                if (j == teams.Count/4 || j==0)
                { 
                    lastDate = lastDate.AddDays(1);
                }

                if (lastDate.DayOfWeek != DayOfWeek.Saturday && lastDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    lastDate = lastDate.AddDays((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek);
                }

                date = lastDate;

                matchService.Add(match);
                matches.Add(match.Id);
            }

            return matches;
        }

        
    }
}
