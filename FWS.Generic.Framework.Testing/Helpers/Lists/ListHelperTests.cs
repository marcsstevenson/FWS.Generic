using System;
using System.Collections.Generic;
using FWS.Generic.Framework.Helpers.Lists;
using FWS.Generic.Framework.Helpers.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.Lists
{
    [TestClass]
    public class ListHelperTests
    {
        [TestMethod]
        public void ListsWithSameLengthButDifferentItemsShallNotBeEqual()
        {
            //Setup
            var list1 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("AA0D0BA5-536A-4DD8-AFF3-A3A4010E4759"), new Guid("0208887C-F8AA-4AF2-B06A-A3A4010E475B") };
            var list2 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("00D226C5-5CA1-4A9B-A9C1-A3A4010E4759"), new Guid("0208887C-F8AA-4AF2-B06A-A3A4010E475B") };
            
            //Exercise
            var areEqual = list1.HasEqualItems(list2);

            //Verify
            Assert.AreEqual(false, areEqual, "We did not expect the lists to be equal");
        }

        [TestMethod]
        public void ListsWithDifferentLengthsShallNotBeEqual()
        {
            //Setup
            var list1 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("AA0D0BA5-536A-4DD8-AFF3-A3A4010E4759") };
            var list2 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("00D226C5-5CA1-4A9B-A9C1-A3A4010E4759"), new Guid("0208887C-F8AA-4AF2-B06A-A3A4010E475B") };

            //Exercise
            var areEqual = list1.HasEqualItems(list2);

            //Verify
            Assert.AreEqual(false, areEqual, "We did not expect the lists to be equal");
        }

        [TestMethod]
        public void ListsWithSameItemsShallBeEqual()
        {
            //Setup
            var list1 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("00D226C5-5CA1-4A9B-A9C1-A3A4010E4759"), new Guid("0208887C-F8AA-4AF2-B06A-A3A4010E475B") };
            var list2 = new List<Guid> { new Guid("C5759476-380C-4C3D-B697-A3A4010E4758"), new Guid("00D226C5-5CA1-4A9B-A9C1-A3A4010E4759"), new Guid("0208887C-F8AA-4AF2-B06A-A3A4010E475B") };

            //Exercise
            var areEqual = list1.HasEqualItems(list2);

            //Verify
            Assert.AreEqual(true, areEqual, "We expected the lists to be equal");
        }
    }
}
