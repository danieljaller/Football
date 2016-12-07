using FootballEngine.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FootballEngine.Domain.Entities.Tests
{
    [TestClass()]
    public class SerieTests
    {
        private Serie validSerie;
        private ValueObjects.GeneralName validSerieName = new ValueObjects.GeneralName("Serie");
        private HashSet<Guid> validTeamTable = TestDataFactory.CreateListWithGuids(Serie.NumberOfTeams).ToHashSet();
        private HashSet<Guid> validMatchTable = TestDataFactory.CreateListWithGuids(Serie.NumberOfMatches).ToHashSet();

        [TestInitialize]
        public void Init()
        {
            Serie_CreateNewValidSerie();
        }

        [TestMethod()]
        public void Serie_CreateNewValidSerie()
        {
            validSerie = new Serie(validSerieName, validTeamTable, validMatchTable);
            Assert.IsNotNull(validSerie);
        }

        [TestMethod]
        public void Serie_ValidateNewSerie()
        {
            Assert.AreNotEqual(Guid.Empty, validSerie.Id);
            Assert.IsNotNull(validSerie.Name);
            Assert.AreEqual(validSerieName, validSerie.Name);
            Assert.IsNotNull(validSerie.MatchTable);
            Assert.AreEqual(Serie.NumberOfMatches, validSerie.MatchTable.Count);
            Assert.AreEqual(validTeamTable, validSerie.TeamTable);
            Assert.IsNotNull(validSerie.TeamTable);
            Assert.AreEqual(Serie.NumberOfTeams, validSerie.TeamTable.Count);
            Assert.AreEqual(validMatchTable, validSerie.MatchTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Serie_CreateInvalidSerie1()
        {
            Serie serie = new Serie(null, validTeamTable, validMatchTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serie_CreateInvalidSerie2()
        {
            Serie serie = new Serie(validSerieName, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serie_CreateInvalidSerie3()
        {
            Serie serie = new Serie(validSerieName, null, validMatchTable);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Serie_CreateInvalidSerie4()
        {
            Serie serie = new Serie(validSerieName, validTeamTable, null);
        }
    }
}