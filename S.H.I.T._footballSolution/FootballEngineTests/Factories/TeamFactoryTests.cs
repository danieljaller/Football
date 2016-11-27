using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using FootballEngine.Domain.Entities;
using FootballEngine.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballEngine.Factories
{
    [TestClass()]
    public class TeamFactoryTests
    {
        [TestMethod()]
        public void CreateTeamsAndSetPlayersTeamId_TestIfOutputIsNotNull()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            Assert.IsNotNull(TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue));
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_1_Valid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
            Assert.AreEqual(TeamFactory.NumberOfPlayerListsRequired, teamList.Count);
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_2_Valid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
            Assert.AreEqual(TeamFactory.NumberOfPlayerListsRequired, teamList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_1_Invalid()
        {
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(null, TeamFactory.MinTeamNameStartValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_2_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(null);
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_3_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            listOfPlayerLists.Remove(listOfPlayerLists.ElementAt(9));
            listOfPlayerLists.Add(null);
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_4_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired - 1; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam1_5_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired + 1; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
        }



        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_1_Valid()
        {
            int teamNumber = TeamFactory.MinTeamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, TeamFactory.MinTeamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_2_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 1,
                teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_3_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 3,
                teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_4_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 5,
                   teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_5_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 10,
                   teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_6_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 100,
                   teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }

        [TestMethod]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_7_Valid()
        {
            int teamNameStartValue = TeamFactory.MinTeamNameStartValue + 1000,
                   teamNumber = teamNameStartValue;
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, teamNameStartValue);
            for (int i = 0; i < teamList.Count; i++)
            {
                Assert.AreEqual($"Team-{teamNumber}", teamList.ToList()[i].Name.Value);
                Assert.AreEqual($"Team-{teamNumber}'s Arena", teamList.ToList()[i].HomeArena.Value);
                teamNumber++;
            }
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_1_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateTeamsAndSetPlayersTeamId_TestInparam2_2_Invalid()
        {
            HashSet<HashSet<Player>> listOfPlayerLists = new HashSet<HashSet<Player>>();
            for (int i = 0; i < TeamFactory.NumberOfPlayerListsRequired; i++)
            {
                listOfPlayerLists.Add(PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue));
            }
            HashSet<Team> teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayerLists, -1);
        }
    }
}
