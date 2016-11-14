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
        private readonly string teamName = "Team";
        private readonly string homeArena = "Arena";

        [TestInitialize]
        public void Init()
        {
            Team_CreateNewTeam();
        }

        [TestMethod()]
        public void Team_CreateNewTeam()
        {
            team = new Team(new ValueObjects.GeneralName(teamName), new ValueObjects.GeneralName(homeArena));
            Assert.IsNotNull(team);
        }

        [TestMethod]
        public void Team_ValidateNewTeam()
        {
            Assert.AreEqual(0, team.GoalDifferens);
            Assert.IsNotNull(team.HomeArena);
            Assert.AreEqual(homeArena, team.HomeArena.Value);
            Assert.AreEqual(0, team.Losses);
            Assert.IsNotNull(team.MatchIds);
            Assert.AreEqual(0, team.MatchIds.Count);
            Assert.IsNotNull(team.Name);
            Assert.AreEqual(teamName, team.Name.Value);
            Assert.IsNotNull(team.PlayerIds);
            Assert.AreEqual(0, team.PlayerIds.Count);
            Assert.AreEqual(0, team.Points);
            Assert.IsNotNull(team.SeriesIds);
            Assert.AreEqual(0, team.SeriesIds.Count);
            Assert.AreEqual(0, team.Ties);
            Assert.AreEqual(0, team.Wins);
        }
    }
}