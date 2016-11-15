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
    public class SerieTests
    {
        private Serie serie;

        [TestInitialize]
        public void Init()
        {
            Serie_CreateNewSerie();
        }

        [TestMethod()]
        public void Serie_CreateNewSerie()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < Serie.NumberOfTeams; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            List<Guid> matchIds = new List<Guid>();
            for (int i = 0; i < Serie.NumberOfMatches; i++)
            {
                matchIds.Add(Guid.NewGuid());
            }
            serie = new Serie(new ValueObjects.GeneralName("Serie"), teamIds, matchIds);
            Assert.IsNotNull(serie);
        }

        [TestMethod]
        public void Serie_ValidateNewSerie()
        {
            Assert.AreNotEqual(Guid.Empty, serie.Id);
            Assert.IsNotNull(serie.Name);
            Assert.IsNotNull(serie.MatchTable);
            Assert.AreEqual(0, serie.MatchTable.Count);
            Assert.IsNotNull(serie.TeamTable);
            Assert.AreEqual(0, serie.TeamTable.Count);
        }
    }
}