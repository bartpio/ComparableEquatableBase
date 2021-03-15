using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ComparableEquatableBase.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CTestSomestuff()
        {
            CTesthelper<Somestuff>();
        }




        public void CTesthelper<T>() where T : ISomestuff, new()
        {
            var list = new List<T>();
            var rand = new Random();
            for (var idx = 0; idx < 1000; idx++)
            {
                var a = rand.Next(0, 9);
                var b = rand.Next(0, 9);
                list.Add(new T { A = a, B = b });
            }

            var expected = list.OrderBy(x => x.Expanded).ToList();
            var act = list.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expected, act);
        }


        [TestMethod]
        public void ETestSomestuff()
        {
            ETesthelper<Somestuff>();
        }

        [TestMethod]
        public void ETestSomestuffBroken()
        {
            ETesthelper<SomestuffBroken>(true);
        }

        public void ETesthelper<T>(bool flipped = false) where T : ISomestuff, new()
        {
            var list = new List<T>();
            var rand = new Random();
            for (var idx = 0; idx < 1000; idx++)
            {
                var a = rand.Next(0, 9);
                var b = rand.Next(0, 9);
                list.Add(new T { A = a, B = b });
            }

            foreach (var entry in list)
            {
                var cloned = new T { A = entry.A, B = entry.B };
                //Assert.IsTrue(((IEquatable<T>)cloned).Equals((IEquatable<T>)entry));
                if (!flipped)
                {
                    Assert.AreEqual(entry, cloned);
                }
                else
                {
                    Assert.AreNotEqual(entry, cloned);
                }
            }
        }

        [TestMethod]
        public void Testhashok_Somestuff()
        {
            Testhashok<Somestuff>();
         
        }

        [TestMethod]
        public void Testhashok_SomestuffBroken()
        {
            Testhashok<SomestuffBroken>();
        }

        //[TestMethod]
        public void Testhashok<T>() where T : ISomestuff, new()
        {
            var list = new List<T>();
            var rand = new Random();
            for (var idx = 0; idx < 100000; idx++)
            {
                var a = rand.Next(0, 5);
                var b = rand.Next(0, 5);
                list.Add(new T { A = a, B = b });
            }

            var sett = new HashSet<bool>();

            for (var idx = 0; idx < 100000; idx += 2)
            {
                var some0 = list[idx];
                var some1 = list[idx + 1];
                var eq = some0.Equals(some1);
                var heq = some0.GetHashCode() == some1.GetHashCode();
                Assert.AreEqual(eq, heq);
                sett.Add(eq);
            }

            CollectionAssert.Contains(sett.ToList(), false);

            for (var idx = 0; idx < 100000; idx += 2)
            {
                var some0 = list[idx];
                var some1 = some0;
                var eq = some0.Equals(some1);
                var heq = some0.GetHashCode() == some1.GetHashCode();
                Assert.AreEqual(eq, heq);
                sett.Add(eq);
            }

            Assert.AreEqual(2, sett.Count);  //we should have seen true and fals
        }
    }
}
