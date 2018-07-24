using System.Collections.Generic;
using FWS.Generic.Framework.Enumerations;
using FWS.Generic.Framework.Helpers;
using FWS.Generic.Framework.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers
{
    [TestClass]
    public class IsOrderedTests
    {
        [TestMethod]
        public void TestingForIsContiguousShallReturnCorrectly()
        {
            //Setup
            var isContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isContiguous.Add(new TestSubject { Order = i });

            var isNotContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isNotContiguous.Add(new TestSubject { Order = i });

            isNotContiguous[2].Order = 100;

            //Exercise and assert
            Assert.IsTrue(IsOrderedHelper.IsContiguous(isContiguous));
            Assert.IsFalse(IsOrderedHelper.IsContiguous(isNotContiguous));
        }

        [TestMethod]
        public void MakingContiguousShallOrderCorrectly()
        {
            //Setup
            var isContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isContiguous.Add(new TestSubject());
            
            //Exercise
            IsOrderedHelper.MakeContiguous(isContiguous);

            //Test and assert
            Assert.IsTrue(IsOrderedHelper.IsContiguous(isContiguous));
        }

        [TestMethod]
        public void MovingAnItemUpWillMaintainContiguousness()
        {
            TestMove(OrderMovement.Up);
        }

        [TestMethod]
        public void MovingAnItemDownWillMaintainContiguousness()
        {
            TestMove(OrderMovement.Down);
        }

        private void TestMove(OrderMovement orderMovement)
        {
            //Setup
            var isContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isContiguous.Add(new TestSubject { Id = i, Order = i });

            var subject = isContiguous[2];

            //Exercise
            IsOrderedHelper.MoveWithinList(isContiguous, subject, orderMovement);

            //Test and assert
            Assert.IsTrue(IsOrderedHelper.IsContiguous(isContiguous));

            if (orderMovement == OrderMovement.Up)
                Assert.IsTrue(subject.Order == 1);
            else
                Assert.IsTrue(subject.Order == 3);
        }

        [TestMethod]
        public void MovingTheTopItemUpShallNotThrowAnException()
        {
            //Setup
            var isContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isContiguous.Add(new TestSubject { Id = i, Order = i });

            var subject = isContiguous[0];

            //Exercise
            IsOrderedHelper.MoveWithinList(isContiguous, subject, OrderMovement.Up);

            //Test and assert
            Assert.IsTrue(IsOrderedHelper.IsContiguous(isContiguous));
            Assert.IsTrue(subject.Order == 0);
        }

        [TestMethod]
        public void MovingTheBottomItemDownShallNotThrowAnException()
        {
            //Setup
            var isContiguous = new List<IIsOrdered>();

            for (int i = 0; i < 5; i++)
                isContiguous.Add(new TestSubject { Id = i, Order = i });

            var subject = isContiguous[4];

            //Exercise
            IsOrderedHelper.MoveWithinList(isContiguous, subject, OrderMovement.Down);

            //Test and assert
            Assert.IsTrue(IsOrderedHelper.IsContiguous(isContiguous));
            Assert.IsTrue(subject.Order == 4);
        }

        private class TestSubject : IIsOrdered
        {
            public int Id { get; set; }
            public int Order { get; set; }

            public override string ToString()
            {
                return "Id:" + Id + " Order:" + Order.ToString();
            }
        }
    }
}
