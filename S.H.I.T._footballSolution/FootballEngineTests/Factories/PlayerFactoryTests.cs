using System;
using System.Collections.Generic;
using FootballEngine.Domain.Entities;
using FootballEngine.Factories;
using FootballEngine.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballEngine.Factories
{
    [TestClass()]
    public class PlayerFactoryTests
    {
        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestIfOutputIsNotNull()
        {
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue);
            Assert.IsNotNull(listOfPlayerLists);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_Min_Valid()
        {
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(PlayerFactory.MinPlayersRequired, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_1_Valid()
        {
            int amount = PlayerFactory.MinPlayersRequired + 1;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_2_Valid()
        {
            int amount = PlayerFactory.MinPlayersRequired + 2;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_3_Valid()
        {
            int amount = PlayerFactory.MinPlayersRequired + 3;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_4_Valid()
        {
            int amount = PlayerFactory.MinPlayersRequired + 4;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_5_Valid()
        {
            int amount = PlayerFactory.MinPlayersRequired + 5;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam1_Max_Valid()
        {
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(PlayerFactory.MaxPlayersRequired, listOfPlayerLists.Count);
        }



        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateListOfPlayerLists_TestInparam1_1_Invalid()
        {
            int amount = PlayerFactory.MinPlayersRequired - 1;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateListOfPlayerLists_TestInparam1_2_Invalid()
        {
            int amount = PlayerFactory.MaxPlayersRequired + 1;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(amount, PlayerFactory.MinPlayerNameStartValue);
            Assert.AreEqual(amount, listOfPlayerLists.Count);
        }



        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_Min_Valid()
        {
            int lastNameAsInt = PlayerFactory.MinPlayerNameStartValue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, PlayerFactory.MinPlayerNameStartValue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual("Player", player.FirstName.Value);
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                Assert.AreEqual("Player" + " " + lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.FullName);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_1_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 1,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_2_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 5,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_3_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 10,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_4_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 100,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_5_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 1000,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MinPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_6_Valid()
        {
            int lastNameAsInt = PlayerFactory.MinPlayerNameStartValue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, PlayerFactory.MinPlayerNameStartValue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual("Player", player.FirstName.Value);
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                Assert.AreEqual("Player" + " " + lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.FullName);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_7_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 2,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }

        [TestMethod()]
        public void CreateListOfPlayerLists_TestInparam2_8_Valid()
        {
            int playerNameStartvalue = PlayerFactory.MinPlayerNameStartValue + 5,
                lastNameAsInt = playerNameStartvalue;
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, playerNameStartvalue);
            foreach (Player player in listOfPlayerLists)
            {
                Assert.AreEqual(lastNameAsInt.NumberToWords().FirstToUpper().Trim(), player.LastName.Value);
                lastNameAsInt++;
            }
        }



        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateListOfPlayerLists_TestInparam2_1_Invalid()
        {
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateListOfPlayerLists_TestInparam2_2_Invalid()
        {
            List<Player> listOfPlayerLists = PlayerFactory.CreateListOfPlayerLists(PlayerFactory.MaxPlayersRequired, - 1);
        }
    }
}