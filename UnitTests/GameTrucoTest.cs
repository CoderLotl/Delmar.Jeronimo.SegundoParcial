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
        private GameTruco game1;
        private Player player1;
        private Player player2;

        [TestInitialize]
        public void Initialize()
        {
            player1 = new Player();
            player1.Name = "Pepe";
            player2 = new Player();
            player2.Name = "Maria";
            game1 = new GameTruco(player1, player2);
        }

        [TestMethod]
        public void GameTruco_OK()
        {
            Assert.IsNotNull(game1.Deck);
            Assert.AreEqual(40, game1.Deck.Count);
            Assert.IsNotNull(game1.HandPlayerOne);
            Assert.IsNotNull(game1.PlayedPlayerOne);
        }

        [TestMethod]
        public void GenerateDeckReturnsDeck()
        {
            List<Card> deck1 = game1.GenerateDeck();

            Assert.IsNotNull(deck1);
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        public void GenerateDeckCreatesBackupDeck()
        {
            List<Card> deck1 = game1.GenerateDeck();

            Assert.IsTrue(File.Exists(".\\" + "TrucoDeck.json"));
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        public void ShuffleDeckReturnsDeck()
        {
            List<Card> deck1 = game1.GenerateDeck();

            deck1 = game1.ShuffleDeck(deck1);

            Assert.IsNotNull(deck1);            
            File.Delete(".\\" + "TrucoDeck.json");
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyDeckException))]
        public void DrawCardThrowsEmptyDeckException()
        {
            List<Card> deck1 = new List<Card>();
            List<Card> deck2 = new List<Card>();            

            game1.DrawCard(deck1, deck2);

            Assert.Fail();
        }

        [TestMethod]
        public void DrawCard_OK()
        {   
            game1.DrawCard(game1.HandPlayerOne, game1.Deck);

            Assert.AreEqual(1, game1.HandPlayerOne.Count);
        }

        [TestMethod]
        public void GiveCards_OK()
        {
            game1.GiveCards(game1.Deck, game1.HandPlayerOne);

            Assert.AreEqual(3, game1.HandPlayerOne.Count);
        }

        [TestMethod]
        public void PlayCard_OK()
        {
            game1.GiveCards(game1.Deck, game1.HandPlayerOne);
            List<Card> tableStack = new List<Card>();

            game1.PlayCard(player1, game1.HandPlayerOne, tableStack);

            Assert.AreEqual(1, tableStack.Count);
        }

        [TestMethod]
        public void EndTurn_OK()
        {
            game1.GiveCards(game1.Deck, game1.HandPlayerOne);
            List<Card> tableStack = new List<Card>();
            game1.PlayCard(player1, game1.HandPlayerOne, tableStack);

            game1.EndRound();

            Assert.AreEqual(0, game1.HandPlayerOne.Count);
            Assert.AreEqual(0, game1.PlayedPlayerOne.Count);
        }

        [TestMethod]
        public void EndGame_OK()
        {
            game1.EndGame();

            Assert.IsTrue(game1.CancelToken.IsCancellationRequested);
        }
    }
}
