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
    public class PlayerNameTests
    {
        #region Create invalid names
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName1()
        {
            PlayerName p = new PlayerName(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName2()
        {
            PlayerName p = new PlayerName("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName3()
        {
            PlayerName p = new PlayerName("#");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName4()
        {
            PlayerName p = new PlayerName("1");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName5()
        {
            PlayerName p = new PlayerName(" ");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName6()
        {
            PlayerName p = new PlayerName("Aaaaaaaaaaaaaaaaaaaaaaaaaa"); // 26
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName7()
        {
            PlayerName p = new PlayerName("a");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName8()
        {
            PlayerName p = new PlayerName(" aaa");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName9()
        {
            PlayerName p = new PlayerName("a  ");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName10()
        {
            PlayerName p = new PlayerName("a  \n");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName11()
        {
            PlayerName p = new PlayerName("+");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName12()
        {
            PlayerName p = new PlayerName("^");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName13()
        {
            PlayerName p = new PlayerName("´");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName14()
        {
            PlayerName p = new PlayerName("`");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerName_CreateInvalidName15()
        {
            PlayerName p = new PlayerName("¨");
        }
        #endregion

        #region Create valid names
        [TestMethod()]
        public void PlayerName_CreateValidName1()
        {
            Assert.AreEqual("Kalle Anka", (new PlayerName("Kalle Anka")).Value);
        }
        
        [TestMethod()]
        public void PlayerName_CreateValidName2()
        {
            Assert.AreEqual("Åke And", (new PlayerName("Åke And")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName3()
        {
            Assert.AreEqual("Anders Ärlig", (new PlayerName("Anders Ärlig")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName4()
        {
            Assert.AreEqual("Örjan Sill", (new PlayerName("Örjan Sill")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName5()
        {
            Assert.AreEqual("Günther Fri", (new PlayerName("Günther Fri")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName6()
        {
            Assert.AreEqual("Estrella Maña", (new PlayerName("Estrella Maña")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName7()
        {
            Assert.AreEqual("Chékov Gry", (new PlayerName("Chékov Gry")).Value);
        }

        [TestMethod()]
        public void PlayerName_CreateValidName8()
        {
            Assert.AreEqual("Ìvar Hungrig", (new PlayerName("Ìvar Hungrig")).Value);
        }
        #endregion
    }
}