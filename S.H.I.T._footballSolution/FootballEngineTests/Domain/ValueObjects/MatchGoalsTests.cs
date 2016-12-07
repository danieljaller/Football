using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FootballEngine.Domain.ValueObjects.Tests
{
    [TestClass()]
    public class MatchGoalsTests
    {
        #region Create invalid MatchGoals

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MatchGoals_CreateInvalidMatchGoals1()
        {
            //MatchGoals m = new MatchGoals(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MatchGoals_CreateInvalidMatchGoals2()
        {
            //MatchGoals m = new MatchGoals(51);
        }

        #endregion Create invalid MatchGoals

        #region Create valid MatchGoals

        [TestMethod()]
        public void MatchGoals_CreateValidMatchGoals1()
        {
            //Assert.AreEqual(0, (new MatchGoals(0)).Value);
        }

        [TestMethod()]
        public void MatchGoals_CreateValidMatchGoals2()
        {
            //Assert.AreEqual(50, (new MatchGoals(50)).Value);
        }

        #endregion Create valid MatchGoals
    }
}