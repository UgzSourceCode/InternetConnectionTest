using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using InternetConnectionTest.Extensions;

namespace InternetConnectionTest.Test.Extensions
{
    class ListExtensionsTest
    {

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        public void IsThisSameAfterClone(int countList)
        {
            Random rand = new Random();
            IList<string> baseList = new List<string>();
            for(int i = 0; i < countList; i++)
            {
                baseList.Add(rand.Next().ToString());
            }
            IList<string> clon = baseList.Clone<string>();
            for(int i = 0; i < baseList.Count; i++)
            {
                Assert.NotNull(clon[i]);
                Assert.AreEqual(baseList[i], clon[i]);
            }
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        public void CoutIsThisSameAfterClone(int countList)
        {
            Random rand = new Random();
            IList<string> baseList = new List<string>();
            for (int i = 0; i < countList; i++)
            {
                baseList.Add(rand.Next().ToString());
            }
            IList<string> clon = baseList.Clone<string>();

            Assert.AreEqual(baseList.Count, clon.Count);
        }
    }
}
