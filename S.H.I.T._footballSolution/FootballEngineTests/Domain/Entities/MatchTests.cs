using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballEngine.Exceptions;

namespace FootballEngine.Domain.Entities.Tests
{
    [TestClass()]
    public class MatchTests
    {
        private Match match;

        [TestInitialize]
        public void Init()
        {
            Match_CreateNewValidMatch();
        }

        [TestMethod]
        public void Match_CreateNewValidMatch()
        {
            match = new Match(DateTime.Now, Guid.NewGuid(), Guid.NewGuid());
            Assert.IsNotNull(match);
        }

        [TestMethod]
        public void Match_ValidateNewMatch()
        {
            Assert.AreNotEqual(Guid.Empty, match.Id);
            Assert.IsNotNull(match.Assists);
            Assert.AreEqual(0, match.Assists.Count);
            // Date?
            Assert.IsNotNull(match.Goals);
            Assert.AreEqual(0, match.Goals.Count);
            Assert.IsNotNull(match.HomeGoals);
            Assert.IsNotNull(match.HomeLineup);
            Assert.AreEqual(0, match.HomeLineup.Count);
            Assert.IsNotNull(match.Injuries);
            Assert.AreEqual(0, match.Injuries.Count);
            // Location?
            // MatchTimeInMinutes?
            Assert.IsNotNull(match.RedCards);
            Assert.AreEqual(0, match.RedCards.Count);
            Assert.IsNotNull(match.VisitorGoals);
            Assert.IsNotNull(match.VisitorLineup);
            Assert.AreEqual(0, match.VisitorLineup.Count);
            Assert.IsNotNull(match.YellowCards);
            Assert.AreEqual(0, match.YellowCards.Count);
        }

        [TestMethod()]
        public void Match_GetMatchResultAsStringTest()
        {
            Assert.AreEqual($"{match.HomeGoals.Value} - {match.VisitorGoals.Value}", match.GetMatchResultAsString());
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Match_CreateInvalidMatch1()
        {            
            Match match = new Match(DateTime.Now.AddDays(-1), Guid.NewGuid(), Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Match_CreateInvalidMatch2()
        {
            Match match = new Match(Match.EndDateForMatchCreation.AddDays(1), Guid.NewGuid(), Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch3()
        {
            Match match = new Match(DateTime.Now, Guid.Empty, Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch4()
        {
            Match match = new Match(DateTime.Now, Guid.NewGuid(), Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch5()
        {
            Match match = new Match(DateTime.Now, Guid.Empty, Guid.NewGuid());
        }
    }
}