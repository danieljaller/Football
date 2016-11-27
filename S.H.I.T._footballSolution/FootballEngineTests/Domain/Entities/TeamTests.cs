using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities.Tests
{
    [TestClass()]
    public class TeamTests
    {
        private Team team;
        private readonly ValueObjects.GeneralName validTeamName = new ValueObjects.GeneralName("Team");
        private readonly ValueObjects.GeneralName validHomeArena = new ValueObjects.GeneralName("Arena");

        [TestInitialize]
        public void Init()
        {
            Team_CreateNewValidTeam();
        }

        [TestMethod()]
        public void Team_CreateNewValidTeam()
        {
            team = new Team(validTeamName, validHomeArena);
            Assert.IsNotNull(team);
        }

        [TestMethod]
        public void Team_ValidateNewTeam()
        {
            Assert.AreNotEqual(Guid.Empty, team.Id);
            Assert.AreEqual(0, team.GoalDifference);
            Assert.IsNotNull(team.HomeArena);
            Assert.AreEqual(validHomeArena, team.HomeArena);
            Assert.AreEqual(0, team.Losses);
            Assert.IsNotNull(team.MatchIds);
            Assert.AreEqual(0, team.MatchIds.Count);
            Assert.IsNotNull(team.Name);
            Assert.AreEqual(validTeamName, team.Name);
            Assert.IsNotNull(team.PlayerIds);
            Assert.AreEqual(0, team.PlayerIds.Count);
            Assert.AreEqual(0, team.Points);
            Assert.IsNotNull(team.SerieIds);
            Assert.AreEqual(0, team.SerieIds.Count);
            Assert.AreEqual(0, team.Ties);
            Assert.AreEqual(0, team.Wins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Team_CreateInvalidTeam1()
        {
            Team _team = new Team(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Team_CreateInvalidTeam2()
        {
            Team _team = new Team(validTeamName, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Team_CreateInvalidTeam3()
        {
            Team _team = new Team(null, validHomeArena);
        }
    }
}