using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library;
using System;

namespace UnitTests
{
    [TestClass]
    public class GameTrucoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod1()
        {
            //ARRANGE

            List<Card> deck1 = null;
            GameTruco game1 = new GameTruco();

            //ACT

            game1.ShuffleDeck(deck1);

            //ASSERT

            Assert.Fail();
        }
    }
}
