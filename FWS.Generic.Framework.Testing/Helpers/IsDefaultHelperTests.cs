using System.Collections.Generic;
using FWS.Generic.Framework.Enumerations;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers
{
    [TestClass]
    public class IsDefaultHelperTests
    {
        [TestMethod]
        public void TheTestSubjectsShallPopulateCorrectly()
        {
            const int trueIndex = 2;

            //Setup
            var testList = TestSubject.GetTestSubjects(trueIndex);
            
            //Exercise and assert
            for (int i = 0; i < testList.Count; i++)
                Assert.IsTrue(testList[i].IsDefault == (trueIndex == i));
            Assert.IsTrue(testList.IsValidDefaultList());
        }

        [TestMethod]
        public void SettingDefaultShallWork()
        {
            const int trueIndexInitial = 2;
            const int trueIndexFinal = 3;

            //Setup
            var testList = TestSubject.GetTestSubjects(trueIndexInitial);

            //Exercise and assert
            var modified = testList.SetDefault(testList[trueIndexFinal]);

            for (int i = 0; i < testList.Count; i++)
                Assert.IsTrue(testList[i].IsDefault == (trueIndexFinal == i));

            Assert.IsTrue(testList.IsValidDefaultList());
            Assert.IsTrue(modified.Count == 2);
            Assert.IsTrue(modified.Contains(testList[trueIndexInitial]));
            Assert.IsTrue(modified.Contains(testList[trueIndexFinal]));
        }

        private class TestSubject : IIsDefault
        {
            public int Id { get; set; }
            public bool IsDefault { get; set; }

            public override string ToString()
            {
                return "Id:" + Id + " IsDefault:" + IsDefault.ToString();
            }

            public static List<TestSubject> GetTestSubjects(int? trueIndex = null)
            {
                var testList = new List<TestSubject>();

                for (int i = 0; i < 5; i++)
                    testList.Add(new TestSubject { Id = i });

                if (trueIndex.HasValue && trueIndex > 0 && trueIndex < testList.Count)
                    testList[trueIndex.Value].IsDefault = true;

                return testList;
            }
        }
    }
}
