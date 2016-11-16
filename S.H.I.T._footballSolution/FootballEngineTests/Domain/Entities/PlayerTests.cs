using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FootballEngine.Domain.Entities.Player;

namespace FootballEngine.Domain.Entities.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        private Player player;

        [TestInitialize]
        public void Init()
        {
            Player_CreateNewValidPlayer();
        }

        [TestMethod()]
        public void Player_CreateNewValidPlayer()
        {
            player = new Player(new ValueObjects.PlayerName("Kalle"), new ValueObjects.PlayerName("Anka"), DateTime.Now.AddYears(-20));
            Assert.IsNotNull(player);
        }

        [TestMethod()]
        public void Player_ValidateNewPlayer()
        {
            Assert.AreNotEqual(Guid.Empty, player.Id);
            Assert.IsNotNull(player.Assists);
            // DateOfBirth?
            Assert.IsNotNull(player.FirstName);
            Assert.IsNotNull(player.LastName);
            Assert.AreEqual($"{player.FirstName} {player.LastName}", player.FullName);
            Assert.IsNotNull(player.Goals);
            Assert.AreEqual(0, player.Goals.Count);
            Assert.IsNotNull(player.MatchesPlayedIds);
            Assert.AreEqual(0, player.MatchesPlayed);
            Assert.IsTrue(player.Playable);
            Assert.AreEqual(Status.Available, player.PlayerStatus);
            Assert.IsNotNull(player.RedCards);
            Assert.AreEqual(0, player.RedCards.Count);
            Assert.IsNotNull(player.YellowCards);
            Assert.AreEqual(0, player.YellowCards.Count);
        }

        // InvalidNameException

        // InvalidNameException

        // InvalidNameException

        // InvalidDateTimeException
    }
}