using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Helper;
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
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void SerieAndMatchGenerator_SerieGeneratorTest()
        {
            SerieAndMatchGenerator.SerieGenerator(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest1()
        {
            SerieAndMatchGenerator.SerieGenerator(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest2()
        {
            //var teamIds = new List<Guid>();
            SerieAndMatchGenerator.SerieGenerator(new List<Guid>(), DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest3()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 14; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest4()
        {
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 17; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest5()
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