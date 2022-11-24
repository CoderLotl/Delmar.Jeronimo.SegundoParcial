using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Library;
using System;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class GameTrucoTest
    {
        [TestMethod]
        public void GenerateDeckReturnsDeck()
        {
            List<Card> deck1 = null;
            GameTruco game1 = new GameTruco();

            deck1 = game1.GenerateDeck();

            Assert.IsNotNull(deck1);
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        public void GenerateDeckCreatesBackupDeck()
        {
            List<Card> deck1 = null;
            GameTruco game1 = new GameTruco();

            deck1 = game1.GenerateDeck();

            Assert.IsTrue(File.Exists(".\\" + "TrucoDeck.json"));
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        public void ShuffleDeckReturnsDeck()
        {
            List<Card> deck1 = null;
            GameTruco game1 = new GameTruco();
            deck1 = game1.GenerateDeck();

            deck1 = game1.ShuffleDeck(deck1);

            Assert.IsNotNull(deck1);            
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShuffleDeckReturnsArgumentNullExceptionIfDeckIsNull()
        {
            //ARRANGE
            List<Card> deck1 = null;
            GameTruco game1 = new GameTruco();

            //ACT
            game1.ShuffleDeck(deck1);

            //ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DrawCardThrowsNullExceptionOnEmptyDeck2()
        {
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = new List<Card>();
            GameTruco game1 = new GameTruco();

            game1.DrawCard(deck1, deck2);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DrawCardThrowsNullExceptionOnNullDeck()
        {
            List<Card> deck1 = null;
            List<Card> deck2 = new List<Card>();
            GameTruco game1 = new GameTruco();

            game1.DrawCard(deck1, deck2);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DrawCardThrowsNullExceptionOnNullDeckBIS()
        {
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = null;
            GameTruco game1 = new GameTruco();

            game1.DrawCard(deck1, deck2);

            Assert.Fail();
        }

        [TestMethod]
        public void DrawCardWorksSuccessfullyWithNonNullDecks()
        {
            GameTruco game1 = new GameTruco();
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = game1.GenerateDeck();
            game1.DrawCard(deck1, deck2);
            bool methodWorked = false;

            if(deck1.Count > 0)
            {
                methodWorked = true;
            }

            Assert.IsTrue(methodWorked);
        }

    }
}
