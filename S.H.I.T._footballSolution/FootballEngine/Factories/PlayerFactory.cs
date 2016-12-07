using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;

namespace FootballEngine.Factories
{
    public static class PlayerFactory
    {
        private static readonly Random random = new Random();

        public static readonly int MinPlayersRequired = 24;
        public static readonly int MaxPlayersRequired = 30;
        public static readonly int MinPlayerNameStartValue = 1;

        public static List<Player> CreateListOfPlayerLists(int amount, int playerNameStartValue)
        {
            if (MinPlayersRequired > amount || amount > MaxPlayersRequired)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} must be between 24 and 30.");
            if (playerNameStartValue < MinPlayerNameStartValue)
                throw new ArgumentOutOfRangeException($"{nameof(playerNameStartValue)} must be larger than {MinPlayerNameStartValue}.");

            List<Player> players = new List<Player>();
            for (int i = playerNameStartValue; i <= (amount + playerNameStartValue - 1); i++)
            {
                PlayerName firstName = new PlayerName("Player");
                PlayerName lastName = new PlayerName(i.NumberToWords().FirstToUpper(true).Trim());

                int year = random.Next(DateTime.Now.Year - 50, DateTime.Now.Year - 17);
                int month = random.Next(1, 13);
                int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
                DateTime dateOfBirth = new DateTime(year, month, day);

                players.Add(new Player(firstName, lastName, new DateOfBirth(dateOfBirth)));
            }

            return players;
        }
    }
}