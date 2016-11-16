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
    public class EventTests
    {
        private Event validEvent;
        private Guid validGuid = Guid.NewGuid();

        [TestInitialize]
        public void Init()
        {
            Event_CreateNewValidEvent();
        }

        [TestMethod]
        public void Event_CreateNewValidEvent()
        {
            validEvent = new Event(validGuid, 30);
            Assert.IsNotNull(validEvent);
        }

        [TestMethod]
        public void Event_ValidateNewEvent()
        {
            Assert.AreNotEqual(Guid.Empty, validEvent.PlayerId);
            // TimeOfEvent, max value?
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Event_CreateInvalidEvent1()
        {
            Event _event = new Event(Guid.Empty, 0);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Event_CreateInvalidEvent2()
        //{
        //    // Create Event with large timeOfEvent
        //}
    }
}