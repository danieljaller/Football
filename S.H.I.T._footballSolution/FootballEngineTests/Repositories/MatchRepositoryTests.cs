using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Repositories.Tests
{
    [TestClass()]
    public class MatchRepositoryTests
    {
        private MatchRepository _testRepository = MatchRepository.Instance;
        [TestMethod()]
        public void MatchRepository_MatchRepositoryTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MatchRepository_AddInvalidTest1()
        {
            _testRepository.Add(null);                   
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchRepository_DeleteTest()
        {
            _testRepository.Delete(Guid.Empty);
        }

        [TestMethod()]
        public void MatchRepository_GetAllTest()
        {
            Assert.IsNotNull(_testRepository.GetAll());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MatchRepository_GetByTest()
        {
            _testRepository.GetBy(Guid.Empty);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchRepository_GetByTest1()
        {
            _testRepository.GetBy(null);
        }
       

        //[TestMethod()]
       
        //public void MatchRepository_LoadTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void MatchRepository_SaveTest()
        //{
        //    throw new NotImplementedException();
        //}
    }
}