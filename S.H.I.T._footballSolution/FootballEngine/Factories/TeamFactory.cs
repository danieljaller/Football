﻿using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballEngine.Factories
{
    public static class TeamFactory
    {
        public static readonly int NumberOfPlayerListsRequired = 16;
        public static readonly int MinTeamNameStartValue = 1;

        public static List<Team> CreateTeamsAndSetPlayersTeamId(List<List<Player>> playersLists, int teamNameStartValue)
        {
            if (playersLists == null)
                throw new ArgumentNullException($"{nameof(playersLists)} is null");
            if (playersLists.Count != NumberOfPlayerListsRequired)
                throw new ArgumentOutOfRangeException($"{nameof(playersLists)} must contain {NumberOfPlayerListsRequired} List<Player>'s.");
            foreach (var playerList in playersLists)
            {
                if (playerList == null)
                    throw new ArgumentNullException($"{nameof(playerList)} is null.");
                if (24 > playerList.Count || playerList.Count() > 30)
                    throw new ArgumentOutOfRangeException($"{nameof(playerList)} must contain between 24 and 30 Player's.");
            }
            if (teamNameStartValue < MinTeamNameStartValue)
                throw new ArgumentOutOfRangeException($"{nameof(teamNameStartValue)} must be larger than {MinTeamNameStartValue}.");

            List<Team> teams = new List<Team>();

            for (int i = teamNameStartValue; i < (playersLists.Count() + teamNameStartValue); i++)
            {
                GeneralName name = new GeneralName($"Team-{i}");
                GeneralName homeArena = new GeneralName($"{name.Value}'s Arena");
                Team team = new Team(name, homeArena);
                HashSet<Guid> playerIds = new HashSet<Guid>();
                foreach (Player player in playersLists[i - teamNameStartValue])
                {
                    player.TeamId = team.Id;
                    playerIds.Add(player.Id);
                }
                team.PlayerIds = playerIds;
                teams.Add(team);
            }

            return teams;
        }
    }
}