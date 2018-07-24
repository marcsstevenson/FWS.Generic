using System;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers
{
    [TestClass]
    public class IGuidIdHelperTests
    {
        private class TestClass : IGuidId
        {
            public Guid Id { get; set; }
        }

        [TestMethod]
        public void NullIGuidIdShallMatchWithNullGuid()
        {
            //Setup
            TestClass testClass = null;
            Guid? id = null;
            
            //Verify
            Assert.IsTrue(testClass.IsSameId(id));
        }

        [TestMethod]
        public void NullIGuidIdShallNotMatchWithGuid()
        {
            //Setup
            TestClass testClass = null;
            Guid? id = Guid.Empty;

            //Verify
            Assert.IsFalse(testClass.IsSameId(id));
        }

        [TestMethod]
        public void SameIGuidIdShallMatchWithGuid()
        {
            //Setup
            Guid? id = Guid.NewGuid();
            TestClass testClass = new TestClass()
            {
                Id = id.Value
            };

            //Verify
            Assert.IsTrue(testClass.IsSameId(id));
        }

        [TestMethod]
        public void DifferentIGuidIdShallNotMatchWithGuid()
        {
            //Setup
            Guid? id = Guid.NewGuid();
            TestClass testClass = new TestClass()
            {
                Id = Guid.NewGuid()
            };

            //Verify
            Assert.IsFalse(testClass.IsSameId(id));
        }
    }
}