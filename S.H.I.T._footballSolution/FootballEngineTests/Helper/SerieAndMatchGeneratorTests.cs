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
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            s.SerieGenerator(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest1()
        {
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            s.SerieGenerator(null, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest2()
        {
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            //var teamIds = new List<Guid>();
            s.SerieGenerator(new List<Guid>(), DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest3()
        {
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 14; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            s.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest4()
        {
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 17; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            s.SerieGenerator(teamIds, DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SerieAndMatchGenerator_SerieGeneratorTest5()
        {
            SerieAndMatchGenerator s = new SerieAndMatchGenerator();
            List<Guid> teamIds = new List<Guid>();
            for (int i = 0; i < 16; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }
            s.SerieGenerator(teamIds, DateTime.Now.AddDays(-1));
        }


        [TestMethod]
        public void SerieAndMatchGenerator_CreateValidMatchList()
        {
            List<Guid> teamIds = new List<Guid>();

            for (int i = 0; i < 16; i++)
            {
                teamIds.Add(Guid.NewGuid());
            }

            SerieAndMatchGenerator s = new SerieAndMatchGenerator();

            var matches = s.SerieGenerator(teamIds, DateTime.Now);

            Assert.AreEqual(240, matches.Count);
        }
    }
}