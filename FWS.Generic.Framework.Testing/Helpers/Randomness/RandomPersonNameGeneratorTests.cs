using System;
using System.Collections.Generic;
using FWS.Generic.Framework.Helpers.Randomness;
using FWS.Generic.Framework.Helpers.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.Randomness
{
    [TestClass]
    public class RandomPersonNameGeneratorTests
    {
        [TestMethod]
        public void GetPersonNameShallNotBeNull()
        {
            //Setup
            var randomPersonNameGenerator = new RandomPersonNameGenerator();
            var personNames = new List<string>();

            //Exersice
            for (int i = 0; i < 100; i++)
                personNames.Add(randomPersonNameGenerator.GetPersonName());

            //Verify
            foreach (var personName in personNames)
                Assert.IsNotNull(personName);
        }
    }
}
