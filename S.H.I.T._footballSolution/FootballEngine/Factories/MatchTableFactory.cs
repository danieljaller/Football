using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace FootballEngine.Factories
{
    public static class MatchTableFactory
    {
        private static DateTime _latestDate;

        public static readonly int NumberOfMatchesThatWillBeCreated = 240;

        public static List<Match> CreateMatchTable(List<Team> teams, DateTime startDate)
        {
            if (teams == null)
                throw new ArgumentNullException($"{nameof(teams)} can not be null.");
            if (teams.Count == 0 || teams.Count != TeamFactory.NumberOfPlayerListsRequired)
                throw new ArgumentOutOfRangeException($"{nameof(teams)} must contain {TeamFactory.NumberOfPlayerListsRequired} players.");
            foreach (Team team in teams)
                if (team == null)
                    throw new ArgumentNullException($"{nameof(team)} is null.");

            if (startDate.Date < DateTime.Now.Date)
                throw new ArgumentOutOfRangeException($"{nameof(startDate)} ({startDate}) can not be earlier than today ({DateTime.Now}).");

            _latestDate = startDate.AddDays(-1);
            List<Match> matches = new List<Match>();

            for (int i = 0; i < 2 * (teams.Count - 1); i++)
            {
                matches.AddRange(RoundGenerator(teams, i));
                teams = ListShuffler(teams);
            }

            return matches;
        }

        private static List<Match> RoundGenerator(List<Team> teams, int i)
        {
            List<Match> matches = new List<Match>();

            for (int j = 0; j < teams.Count / 2; j++)
            {
                Team[] pairing = new Team[2];
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

        private static List<Team> ListShuffler(List<Team> teams)
        {
            var tempTeams = new List<Team>();

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