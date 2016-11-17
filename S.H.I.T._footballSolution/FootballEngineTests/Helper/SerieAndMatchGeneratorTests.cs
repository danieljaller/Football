using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Helper;
using FootballEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Helper.Tests
{
    [TestClass()]
    public class SerieAndMatchGeneratorTests
    {
        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerieAndMatchGenerator_InvalidInputTest1()
        {
            SerieAndMatchGenerator.SerieGenerator(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SerieAndMatchGenerator_InvalidInputTest2()
        {
            SerieAndMatchGenerator.SerieGenerator(new List<Guid>(), DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SerieAndMatchGenerator_InvalidInputTest3()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 14; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SerieAndMatchGenerator_InvalidInputTest4()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 17; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SerieAndMatchGenerator_InvalidInputTest5()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 16; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now.AddDays(-1));
        }

        



        [TestMethod]
        public void SerieAndMatchGenerator_CreateValidMatchList()
        {
            List<Guid> teamIds = new List<Guid>();

            for (int i = 0; i < 16; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }

            var matches = SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);

            Assert.AreEqual(240, matches.Count);
        }
    }
}