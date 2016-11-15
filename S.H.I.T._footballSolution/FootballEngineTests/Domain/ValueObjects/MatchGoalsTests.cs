using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.ValueObjects.Tests
{
    [TestClass()]
    public class MatchGoalsTests
    {
        #region Create invalid MatchGoals
        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void MatchGoals_CreateInvalidMatchGoals1()
        {
            MatchGoals m = new MatchGoals(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void MatchGoals_CreateInvalidMatchGoals2()
        {
            MatchGoals m = new MatchGoals(51);
        }
        #endregion

        #region Create valid MatchGoals
        [TestMethod()]
        public void MatchGoals_CreateValidMatchGoals1()
        {
            Assert.AreEqual(0, (new MatchGoals(0)).Value);
        }

        [TestMethod()]
        public void MatchGoals_CreateValidMatchGoals2()
        {
            Assert.AreEqual(50, (new MatchGoals(50)).Value);
        }
        #endregion
    }
}