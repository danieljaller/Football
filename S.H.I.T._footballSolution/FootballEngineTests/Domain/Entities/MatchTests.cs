using FootballEngine.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            match = new Match(new MatchDate(DateTime.Now), Guid.NewGuid(), Guid.NewGuid(), new GeneralName("Location"));
            Assert.IsNotNull(match);
        }

        [TestMethod]
        public void Match_ValidateNewMatch()
        {
            Assert.AreNotEqual(Guid.Empty, match.Id);
            //// Date?
            Assert.IsNotNull(match.HomeGoals);
            Assert.IsNotNull(match.HomeLineup);
            Assert.AreEqual(0, match.HomeLineup.Count);
            // Location?
            // MatchTimeInMinutes?
            Assert.IsNotNull(match.VisitorGoals);
            Assert.IsNotNull(match.VisitorLineup);
            Assert.AreEqual(0, match.VisitorLineup.Count);
        }

        [TestMethod()]
        public void Match_GetMatchResultAsStringTest()
        {
            Assert.AreEqual($"{match.HomeGoals.Count()} - {match.VisitorGoals.Count()}", match.GetMatchResultAsString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Match_CreateInvalidMatch1()
        {
            Match match = new Match(new MatchDate(DateTime.Now.AddDays(-1)), Guid.NewGuid(), Guid.NewGuid(), new GeneralName("Location"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Match_CreateInvalidMatch2()
        {
            Match match = new Match(new MatchDate(Match.EndDateForMatchCreation.AddDays(1)), Guid.NewGuid(), Guid.NewGuid(), new GeneralName("Location"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch3()
        {
            Match match = new Match(new MatchDate(DateTime.Now), Guid.Empty, Guid.Empty, new GeneralName("Location"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch4()
        {
            Match match = new Match(new MatchDate(DateTime.Now), Guid.NewGuid(), Guid.Empty, new GeneralName("Location"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Match_CreateInvalidMatch5()
        {
            Match match = new Match(new MatchDate(DateTime.Now), Guid.Empty, Guid.NewGuid(), new GeneralName("Location"));
        }
    }
}