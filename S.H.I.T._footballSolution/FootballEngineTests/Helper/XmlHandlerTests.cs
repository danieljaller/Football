using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballEngine.Domain.Entities;

namespace FootballEngine.Helper.Tests
{
    [TestClass()]
    public class XmlHandlerTests
    {
        [TestMethod()]
        public void XmlHandler_LoadFromTest()
        {
            List<Player> players = XmlHandler.LoadFrom("", typeof(List<Player>)) as List<Player>;
        }

        [TestMethod()]
        public void XmlHandler_SaveToTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void XmlHandler_SaveToTest1()
        {
            throw new NotImplementedException();
        }
    }
}