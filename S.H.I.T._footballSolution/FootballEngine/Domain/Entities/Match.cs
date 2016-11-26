﻿using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using FootballEngine.Helper;

namespace FootballEngine.Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public bool IsPlayed { get; set; }
        public MatchDate Date { get; set; }
        public GeneralName Location { get; set; }
        public uint MatchTimeInMinutes { get; set; }
        public string Result { get { return IsPlayed ? $"{HomeGoals.Count} - {VisitorGoals.Count}" : "-"; } }
        public List<Event> HomeAssists { get; set; }
        public List<Exchange> HomeExchanges { get; set; }
        public List<Event> HomeGoals { get; set; }
        public Guid HomeTeamId { get; set; }
        public List<Guid> HomeLineup { get; set; }
        public List<Event> HomeRedCards { get; set; }
        public List<Event> HomeYellowCards { get; set; }
        public List<Event> VisitorAssists { get; set; }
        public List<Exchange> VisitorExchanges { get; set; }
        public List<Event> VisitorGoals { get; set; }
        public Guid VisitorTeamId { get; set; }
        public List<Guid> VisitorLineup { get; set; }
        public List<Event> VisitorRedCards { get; set; }
        public List<Event> VisitorYellowCards { get; set; }
        public static DateTime EndDateForMatchCreation { get { return DateTime.Now.AddYears(5); } }

        public Match() { }

        public Match(MatchDate date, Guid homeTeamId, Guid visitorTeamId, GeneralName location)
        {
            Id = Guid.NewGuid();
            if (IsValidInparameter(date, homeTeamId, visitorTeamId, location))
            {
                Date = date;
                MatchTimeInMinutes = 0;
                Location = location;

                HomeAssists = new List<Event>();
                HomeExchanges = new List<Exchange>();
                HomeGoals = new List<Event>();
                HomeTeamId = homeTeamId;
                HomeLineup = new List<Guid>();
                HomeRedCards = new List<Event>();
                HomeYellowCards = new List<Event>();

                VisitorAssists = new List<Event>();
                VisitorExchanges = new List<Exchange>();
                VisitorGoals = new List<Event>();
                VisitorTeamId = visitorTeamId;
                VisitorLineup = new List<Guid>();
                VisitorRedCards = new List<Event>();
                VisitorYellowCards = new List<Event>();
            }
        }

        private bool IsValidInparameter(MatchDate date, Guid homeTeamId, Guid visitorTeamId, GeneralName location)
        {
            if (date.Value.Date < DateTime.Now.Date)
                throw new ArgumentOutOfRangeException($"{nameof(date)} is out of range can only be between now and {EndDateForMatchCreation} years from now.");

            if (date.Value.Date > EndDateForMatchCreation.Date)
                throw new ArgumentOutOfRangeException($"{nameof(date)} is out of range can only be between now and {EndDateForMatchCreation} years from now.");

            if (homeTeamId == Guid.Empty)
                throw new ArgumentException($"{nameof(homeTeamId)} cannot be a empty Guid.");

            if (visitorTeamId == Guid.Empty)
                throw new ArgumentException($"{nameof(visitorTeamId)} cannot be a empty Guid.");

            if (location == null)
                throw new ArgumentNullException($"{nameof(location)} cannot be null.");

            return true;
        }

        public string GetMatchResultAsString()
        {
            return $"{HomeGoals.Count} - {VisitorGoals.Count}";
        }
    }



}
