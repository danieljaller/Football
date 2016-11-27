using System;
using System.Collections.Generic;
using System.Linq;
using FootballEngine.Domain.Entities;
using FootballEngine.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballEngine.Factories
{
    [TestClass()]
    public class MatchTableFactoryTests
    {
        private readonly int numberOfMatchesThatWillGetCreated = 240;

        [TestInitialize]
        public void Init()
        {

        }

        private HashSet<HashSet<Player>> GetValidListOfPlayerLists()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            return listOfPlayerLists;
        }

        private HashSet<Team> GetValidTeamList()
        {
            return TeamFactory.CreateTeamsAndSetPlayersTeamId(GetValidListOfPlayerLists(), TeamFactory.MinTeamNameStartValue);
        }

        private HashSet<Team> GetInvalidTeamList()
        {
            HashSet<Team> teamList = GetValidTeamList();
            teamList.Remove(teamList.ElementAt(7));
            teamList.Add(null);
            return teamList;
        }

        [TestMethod()]
        public void CreateMatchTable_TestIfOutputIsNotNull()
        {
            Assert.IsNotNull(MatchTableFactory.CreateMatchTable(GetValidTeamList(), DateTime.Today));
        }

        [TestMethod()]
        public void CreateMatchTable_TestInparam1_1_Valid()
        {
            Assert.AreEqual(numberOfMatchesThatWillGetCreated, MatchTableFactory.CreateMatchTable(GetValidTeamList(), DateTime.Today).Count);
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMatchTable_TestInparam1_1_Invalid()
        {
            MatchTableFactory.CreateMatchTable(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateMatchTable_TestInparam1_2_Invalid()
        {
            MatchTableFactory.CreateMatchTable(GetInvalidTeamList(), DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateMatchTable_TestInparam1_3_Invalid()
        {
            HashSet<Team> teamList = GetValidTeamList();
            teamList.Remove(teamList.ElementAt(5));
            MatchTableFactory.CreateMatchTable(teamList, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateMatchTable_TestInparam1_4_Invalid()
        {
            HashSet<Team> teamList = GetValidTeamList();
            teamList.UnionWith(GetValidTeamList());
            MatchTableFactory.CreateMatchTable(teamList, DateTime.Now);
        }



        [TestMethod()]
        public void CreateMatchTable_TestInparam2_1_Valid()
        {
            var matchTable = MatchTableFactory.CreateMatchTable(GetValidTeamList(), DateTime.Now);
        }
    }
}