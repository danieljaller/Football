using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballEngine.Exceptions;

namespace FootballEngine.Domain.ValueObjects.Tests
{
    [TestClass()]
    public class GeneralNameTests
    {
        #region Create invalid names
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName1()
        {
            GeneralName n = new GeneralName(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName2()
        {
            GeneralName n = new GeneralName("");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName3()
        {
            GeneralName n = new GeneralName("   ");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName4()
        {
            GeneralName n = new GeneralName("#");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName5()
        {
            GeneralName n = new GeneralName("Aaaaaaaaaaaaaaaaaaaaaaaaaa");      // 26
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName6()
        {
            GeneralName n = new GeneralName("´");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName7()
        {
            GeneralName n = new GeneralName("¨");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GeneralName_CreateInvalidName8()
        {
            GeneralName n = new GeneralName("`");
        }

        //[TestMethod()]
        //public void GeneralName_CreateInvalidName()
        //{
        //    try
        //    {
        //        GeneralName invalidName1 = new GeneralName(null);
        //        Assert.Fail("An exception should have been thrown");
        //    }
        //    catch (InvalidNameException invalidNameException)
        //    {
        //        Assert.IsInstanceOfType(invalidNameException, typeof(InvalidNameException));
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail($"Unexpected exception of type {e.GetType()} caught: {e.Message}");
        //    }
        //}
        #endregion

        #region Create valid names
        [TestMethod()]
        public void GeneralName_CreateValidName1()
        {
            Assert.AreEqual("Team", (new GeneralName("Team")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName2()
        {
            Assert.AreEqual("Team 2", (new GeneralName("Team 2")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName3()
        {
            Assert.AreEqual("Team-3", (new GeneralName("Team-3")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName4()
        {
            Assert.AreEqual("Team4", (new GeneralName("Team4")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName5()
        {
            Assert.AreEqual("  5Team", (new GeneralName("  5Team")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName6()
        {
            Assert.AreEqual("Aaaaaaaaaaaaaaaaaaaaaaaaa", (new GeneralName("Aaaaaaaaaaaaaaaaaaaaaaaaa")).Value);     // 25
        }

        [TestMethod()]
        public void GeneralName_CreateValidName7()
        {
            Assert.AreEqual("Maña 1", (new GeneralName("Maña 1")).Value);
        }

        [TestMethod()]
        public void GeneralName_CreateValidName8()
        {
            Assert.AreEqual("Güte 1", (new GeneralName("Güte 1")).Value);
        }
        #endregion
    }
}