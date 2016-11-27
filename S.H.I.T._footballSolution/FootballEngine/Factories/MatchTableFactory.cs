using System;
using System.Collections.Generic;
using System.Linq;
using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;

namespace FootballEngine.Factories
{
    public static class MatchTableFactory
    {
        private static DateTime _latestDate;

        public static readonly int NumberOfMatchesThatWillBeCreated = 240;

        public static HashSet<Match> CreateMatchTable(HashSet<Team> teams, DateTime startDate)
        {
            if (teams == null)
                throw new ArgumentNullException($"{nameof(teams)} can not be null.");
            if (!teams.Any() || teams.Count() != TeamFactory.NumberOfPlayerListsRequired)
                throw new ArgumentOutOfRangeException($"{nameof(teams)} must contain {TeamFactory.NumberOfPlayerListsRequired} players.");
            foreach (Team team in teams)
                if (team == null)
                    throw new ArgumentNullException($"{nameof(team)} is null.");

            if (startDate.Date < DateTime.Now.Date)
                throw new ArgumentOutOfRangeException($"{nameof(startDate)} ({startDate}) can not be earlier than today ({DateTime.Now}).");
            
            _latestDate = startDate.AddDays(-1);
            HashSet<Match> matches = new HashSet<Match>();

            for (int i = 0; i < 2*(teams.Count() - 1); i++)
            {
                matches.UnionWith(RoundGenerator(teams, i));
                teams = ListShuffler(teams);
            }

            return matches;
        }

        private static HashSet<Match> RoundGenerator(HashSet<Team> teams, int i)
        {
            HashSet<Match> matches = new HashSet<Match>();

            for (int j = 0; j < teams.Count / 2; j++)
            {
                Team[] pairing = new Team[2];
                if (i < teams.Count / 2)
                {
                    pairing[0] = teams.ElementAt(j);
                    pairing[1] = teams.ElementAt(teams.Count - 1 - j);
                }
                else
                {
                    pairing[1] = teams.ElementAt(j);
                    pairing[0] = teams.ElementAt(teams.Count - 1 - j);
                }

                if (j == teams.Count / 4 || j == 0)
                {
                    _latestDate = _latestDate.AddDays(1);
                }

                if (_latestDate.DayOfWeek != DayOfWeek.Saturday && _latestDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    _latestDate = _latestDate.AddDays((int)DayOfWeek.Saturday - (int)_latestDate.DayOfWeek);
                }

                Match match = new Match(new MatchDate(_latestDate), pairing[0].Id, pairing[1].Id, pairing[0].HomeArena);
                pairing[0].MatchIds.Add(match.Id);
                pairing[1].MatchIds.Add(match.Id);

                matches.Add(match);
            }

            return matches;
        }

        private static HashSet<Team> ListShuffler(HashSet<Team> teams)
        {
            var tempTeams = new HashSet<Team>();

            for (int j = 0; j < teams.Count; j++)
            {
                if (j == 0)
                {
                    tempTeams.Add(teams.ElementAt(0));
                }
                else if (j == teams.Count - 1)
                {
                    tempTeams.Add(teams.ElementAt(1));
                }
                else
                {
                    tempTeams.Add(teams.ElementAt(j + 1));
                }
            }

            return teams = tempTeams;
        }

    }
}

