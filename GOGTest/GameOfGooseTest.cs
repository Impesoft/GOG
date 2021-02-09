using GameOfGoose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GOGTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanInstantiateGoG()
        {
            GogGame gog = new GogGame();
        }
    }
}