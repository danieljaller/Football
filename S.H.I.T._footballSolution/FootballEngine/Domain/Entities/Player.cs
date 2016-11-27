﻿using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;


namespace FootballEngine.Domain.Entities
{
    public class Player
    {
        public enum Status
        {
            Available,
            Suspended,
            Injured,
            NationalTeam,
            Other
        }

        public Guid Id { get; set; }
        public PlayerName FirstName { get; set; }
        public PlayerName LastName { get; set; }
        public string FullName { get { return $"{FirstName.Value} {LastName.Value}"; } }
        public DateTime DateOfBirth { get; set; }
        public Status PlayerStatus { get; set; }
        public List<Guid> RedCards { get; set; }
        public List<Guid> YellowCards { get; set; }
        public List<Guid> Assists { get; set; }
        public List<Guid> Goals { get; set; }
        public int MatchesPlayed { get { return MatchesPlayedIds.Count; } }
        public Guid TeamId { get; set; }
        public List<Guid> MatchesPlayedIds { get; set; }
        public bool Playable { get { return (PlayerStatus == Status.Available); } }

        public Player() { }


        public Player(PlayerName firstName, PlayerName lastName, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PlayerStatus = Status.Available;
            MatchesPlayedIds = new List<Guid>();
            YellowCards = new List<Guid>();
            RedCards = new List<Guid>();
            Assists = new List<Guid>();
            Goals = new List<Guid>();
        }
    }
}
