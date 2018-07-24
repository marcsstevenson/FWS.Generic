using System;
using System.Collections.Generic;
using System.Reflection;
using FWS.Generic.Framework.Annotations;
using FWS.Generic.Framework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Linq;
using FluentAssertions;

namespace FWS.Generic.Framework.Testing.Helpers.Enums
{
    [TestClass]
    public class EnumHelperTests
    {
        private enum HiImAnEnum
        {
            [System.ComponentModel.Description("Hello")]
            Hola,
            Neit,
            Sup
        }

        [TestMethod]
        public void GettingDescriptionValueShallReturnCorrectly()
        {
            var resultDescription = HiImAnEnum.Hola.GetDescription();

            Assert.AreEqual(resultDescription, "Hello");
        }

        [TestMethod]
        public void GettingDescriptionWithoutValueShallReturnCorrectly()
        {
            var resultDescription = HiImAnEnum.Neit.GetDescription();

            Assert.AreEqual(resultDescription, null);
        }

        [TestMethod]
        public void ListsAreEqualShallReturnTrueForEqualLists()
        {
            //Setup
            var list1 = new List<HiImAnEnum> { HiImAnEnum.Hola, HiImAnEnum.Neit };
            var list2 = new List<HiImAnEnum> { HiImAnEnum.Hola, HiImAnEnum.Neit };

            //Exercise
            var listsAreEqual = EnumHelper.ListsAreEqual(list1, list2);

            //Verify
            Assert.IsTrue(listsAreEqual);
        }

        [TestMethod]
        public void ListsAreEqualShallReturnFalseWhenOneListIsLarger()
        {
            //Setup
            var list1 = new List<HiImAnEnum> { HiImAnEnum.Hola, HiImAnEnum.Neit };
            var list2 = new List<HiImAnEnum> { HiImAnEnum.Hola, HiImAnEnum.Neit, HiImAnEnum.Sup };

            //Exercise
            var listsAreEqual = EnumHelper.ListsAreEqual(list1, list2);

            //Verify
            Assert.IsFalse(listsAreEqual);
        }

        [TestMethod]
        public void ListsAreEqualShallReturnFalseWhenThereIsNoCommonality()
        {
            //Setup
            var list1 = new List<HiImAnEnum> { HiImAnEnum.Hola };
            var list2 = new List<HiImAnEnum> { HiImAnEnum.Neit, HiImAnEnum.Sup };

            //Exercise
            var listsAreEqual = EnumHelper.ListsAreEqual(list1, list2);

            //Verify
            Assert.IsFalse(listsAreEqual);
        }

        [TestMethod]
        public void MissingFromShallReturnItemsMissingFromList()
        {
            //Setup
            var list1 = new List<HiImAnEnum> { HiImAnEnum.Hola, HiImAnEnum.Sup };
            var list2 = new List<HiImAnEnum> { HiImAnEnum.Hola };

            //Exercise
            var missingFrom = list1.MissingFrom(list2);

            //Verify
            Assert.AreEqual(1, missingFrom.Count);
            Assert.AreEqual(HiImAnEnum.Sup, missingFrom.First());
        }

        [TestMethod]
        public void ToEnumerable_ShouldReturnNull_WhenNullIsPassedIn()
        {
            // Arrange
            string s = null;

            // Act
            var result = s.ToEnumerable<HiImAnEnum>();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void ToEnumerable_ShouldReturnSingleResult_WhenSingleEnumIsSentIn()
        {
            // Act
            var result = "Hola".ToEnumerable<HiImAnEnum>();

            // Assert
            result.Should().BeEquivalentTo(new[] {HiImAnEnum.Hola});
        }

        [TestMethod]
        public void ToEnumerable_ShouldReturnResult_WhenStringIsLowerCase()
        {
            // Act
            var result = "sup".ToEnumerable<HiImAnEnum>();

            // Assert
            result.Should().BeEquivalentTo(new[] { HiImAnEnum.Sup });
        }

        [TestMethod]
        public void ToEnumerable_ShouldReturnTheCorrectNumberOfResults_WhenMultipleNamesArePassedIn()
        {
            // Act
            var result = "Hola,sup".ToEnumerable<HiImAnEnum>();

            // Assert
            result.Should().BeEquivalentTo(new[] {HiImAnEnum.Hola, HiImAnEnum.Sup});
        }

        [TestMethod]
        public void ToEnumerable_ShouldNotThrow_WhenPassingInvalidName()
        {
            // Act
            var result = "Ho,neit".ToEnumerable<HiImAnEnum>();

            // Assert
            result.Should().BeEquivalentTo(new[] {HiImAnEnum.Neit});
        }
    }
}
