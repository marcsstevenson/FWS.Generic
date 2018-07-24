using System;
using FWS.Generic.Framework.Helpers.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.Reflection
{
    [TestClass]
    public class PropertiesHelperTests
    {
        [TestMethod]
        public void ObjectPropertiesAreEqualShallReturnTrueWhenComparingTwoObjectsWithEqualPropertyValues()
        {
            var testSubject1 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 1 };
            var testSubject2 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 1 };

            Assert.IsTrue(testSubject1.ObjectPropertiesAreEqual(testSubject2).AreEqual);
        }
        
        [TestMethod]
        public void ObjectPropertiesAreEqualShallReturnFalseWhenComparingTwoObjectsWithDifferingPropertyValues()
        {
            var testSubject1 = new TestSubject { Created = new DateTime(2010, 10, 1), Name = "testSubject1", Id = 1 };
            var testSubject2 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 1 };

            Assert.IsFalse(testSubject1.ObjectPropertiesAreEqual(testSubject2).AreEqual);

            testSubject1 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 1 };
            testSubject2 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject2", Id = 1 };

            Assert.IsFalse(testSubject1.ObjectPropertiesAreEqual(testSubject2).AreEqual);

            testSubject1 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 1 };
            testSubject2 = new TestSubject { Created = new DateTime(2010, 10, 10), Name = "testSubject1", Id = 2 };

            Assert.IsFalse(testSubject1.ObjectPropertiesAreEqual(testSubject2).AreEqual);
        }

        private class TestSubject
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
        }
    }
}
