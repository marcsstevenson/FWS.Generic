using System;
using FWS.Generic.Framework.Helpers.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.Math
{
    [TestClass]
    public class RounderTests
    {
        [TestMethod]
        public void RoundingDecimalUpShallReturnCorrectResult()
        {
            //Setup
            const decimal unrounded = 0.006m;
            const decimal expectedResult = 0.01m;

            //Exercise
            decimal actualResult = unrounded.Round2();

            //Verify
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RoundingDecimalDownShallReturnCorrectResult()
        {
            //Setup
            const decimal unrounded = 0.005m;
            const decimal expectedResult = 0.00m;

            //Exercise
            decimal actualResult = unrounded.Round2();

            //Verify
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
