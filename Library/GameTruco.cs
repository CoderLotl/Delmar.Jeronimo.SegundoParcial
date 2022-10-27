using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public enum Suit
    {
        Cups,
        Golds,
        Clubs,
        Swords
    }

    public class GameTruco : Game, ICardGame
    {
        // --- CALLS FLAGS
        bool truco;
        bool reTruco;
        bool valeCuatro;
        bool envido;
        bool realEnvido;
        bool faltaEnvido;
        bool flor;

        public List<Card> Deck { get; set; }
        public List<Card> HandPlayerOne { get; set; }
        public List<Card> HandPlayerTwo { get; set; }
        public List<Card> PlayedPlayerOne { get; set; }
        public List<Card> PlayedPlayerTwo { get; set; }

        public GameTruco()
        {            
            this.Deck = GenerateDeck();
            this.Log = "";
            this.PlayerOneScore = 0;
            this.PlayerTwoScore = 0;
            this.Turn = 0;
            this.Match = 0;
        }

        public List<Card> GenerateDeck()
        {
            List<Card> newDeck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                for (int i = 0; i < 7; i++)
                {
                    int relativeValue = CalculateRelativeValue(i + 1, suit);

                    Card newCard = new Card(i + 1, relativeValue, suit);
                    newDeck.Add(newCard);
                }
                for (int i = 10; i < 13; i++)
                {
                    int relativeValue = CalculateRelativeValue(i, suit);

                    Card newCard = new Card(i, relativeValue, suit);
                    newDeck.Add(newCard);
                }
            }

            return newDeck;
        }

        public List<Card> ShuffleDeck(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            Random randomNumber = new Random();
            int cards = deck.Count;

            for (int i = 0; i < 40; i++)
            {
                int randomCard = randomNumber.Next(cards);
                shuffledDeck.Add(deck[randomCard]);
                deck.Remove(deck[randomCard]);
                cards--;
            }

            return shuffledDeck;
        }

        private static int CalculateRelativeValue(int rank, Suit suit)
        {
            int relativeValue = 0;

            if (rank <= 6 && rank >= 4)
            {
                relativeValue = rank - 3;
            }
            else if (rank == 7)
            {
                if (suit == Suit.Clubs || suit == Suit.Cups)
                {
                    relativeValue = 4;
                }
                else if (suit == Suit.Golds)
                {
                    relativeValue = 11;
                }
                else
                {
                    relativeValue = 12;
                }
            }
            else if (rank >= 10 && rank <= 12)
            {
                relativeValue = rank - 5;
            }
            else if (rank == 1)
            {
                if (suit == Suit.Cups || suit == Suit.Golds)
                {
                    relativeValue = 8;
                }
                else if (suit == Suit.Clubs)
                {
                    relativeValue = 13;
                }
                else
                {
                    relativeValue = 14;
                }
            }
            else if (rank >= 2 && rank <= 3)
            {
                relativeValue = rank + 7;
            }

            return relativeValue;
        }

        public void DrawCard(List<Card> playerCards, List<Card> deck)
        {
            if (deck.Count > 0)
            {
                playerCards.Add(deck[0]);
                deck.Remove(deck[0]);
            }
        }

        public void GiveCards(List<Card> deck, List<Card> playerCards)
        {
            Random dice = new Random();
            int cards = deck.Count;

            for (int i = 0; i < 3; i++)
            {
                int cardNumber = dice.Next(cards);

                playerCards.Add(deck[cardNumber]);
                deck.Remove(deck[cardNumber]);
                cards--;
            }
        }

        public void PlayCard(Player player, List<Card> stack)
        {
            throw new NotImplementedException();
        }

        public override void EndGame()
        {
            this.PlayedPlayerOne.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerOne.Clear();
            this.PlayedPlayerOne.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerTwo.Clear();
            
        }

        public override void Play(string Text)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                str.AppendLine(Text);
                this.Log = str.ToString();
                Thread.Sleep(2000);
            }
        }

        private void RealPlay(Player player1, Player player2)
        {
            Player playerOne;
            Player playerTwo;

            // --- PRELIMINALS

            if(ChoosePlayerOne() == 1)
            {
                playerOne = player1;
                playerTwo = player2;
            }
            else
            {
                playerOne = player2;
                playerTwo = player1;
            }

            // --- START
            this.Deck = ShuffleDeck(this.Deck); // DECK SHUFFLING

            GiveCards(this.Deck, this.HandPlayerOne);
            GiveCards(this.Deck, this.HandPlayerTwo);



        }

        private int ChoosePlayerOne()
        {
            Random coin = new Random();

            return coin.Next(1, 3);
        }
    }
}
