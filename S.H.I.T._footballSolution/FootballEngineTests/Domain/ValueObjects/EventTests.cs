//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using FootballEngine.Domain.ValueObjects;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FootballEngine.Domain.ValueObjects.Tests
//{
//    [TestClass()]
//    public class EventTests
//    {
//        private Event validEvent;
//        private Guid validGuid = Guid.NewGuid();

//        [TestInitialize]
//        public void Init()
//        {
//            Event_CreateNewValidEvent();
//        }

//        [TestMethod]
//        public void Event_CreateNewValidEvent()
//        {
//            validEvent = new Event(validGuid, new MatchMinute(30));
//            //Assert.IsNotNull(validEvent);
//        }

//        [TestMethod]
//        public void Event_ValidateNewEvent()
//        {
//            Assert.AreEqual(validGuid, validEvent.PlayerId);
//            Assert.AreEqual(Convert.ToUInt32(30), validEvent.TimeOfEvent);           
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException))]
//        public void Event_CreateInvalidEvent1()
//        {
//            Event _event = new Event(Guid.Empty, new MatchMinute(0));
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentOutOfRangeException))]
//        public void Event_CreateInvalidEvent2()
//        {
//            Event _event = new Event(Guid.NewGuid(), new MatchMinute(MatchMinute.MaxValue + 1));
//        }
//    }
//}