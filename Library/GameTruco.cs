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
        // ********************* ☽

        // --- CALLS FLAGS
        bool truco;
        bool reTruco;
        bool valeCuatro;
        bool envido;
        bool realEnvido;
        bool faltaEnvido;
        bool flor;
        int temporaryPoints;

        public override event EventHandler NotifyLogUpdate;

        // ********************* ☽

        public List<Card> Deck { get; set; }
        public List<Card> HandPlayerOne { get; set; }
        public List<Card> HandPlayerTwo { get; set; }
        public List<Card> PlayedPlayerOne { get; set; }
        public List<Card> PlayedPlayerTwo { get; set; }

        // ********************* ✩

        public GameTruco()
        {            
            this.Deck = GenerateDeck();
            this.Log = "";
            this.CleanLog = "";
            this.PlayerOneScore = 0;
            this.PlayerTwoScore = 0;
            this.HandPlayerOne = new List<Card>();
            this.HandPlayerTwo = new List<Card>();
            this.PlayedPlayerOne = new List<Card>();
            this.PlayedPlayerTwo = new List<Card>();
            this.CancelToken = new CancellationTokenSource();
            this.Turn = 0;
            this.Match = 0;
        }

        // ********************* ✩

        // --------------------------

        public List<Card> GenerateDeck()
        {
            //SERIALIZE LOAD
            
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

            //SERIALIZE SAVE

            return newDeck;
        }

        // --------------------------

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

        // --------------------------

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

        // --------------------------

        public void DrawCard(List<Card> playerCards, List<Card> deck)
        {
            if (deck.Count > 0)
            {
                playerCards.Add(deck[0]);
                deck.Remove(deck[0]);
            }
        }

        // --------------------------

        public void GiveCards(List<Card> deck, List<Card> playerCards)
        {            
            int cards = deck.Count;

            for (int i = 0; i < 3; i++)
            {
                playerCards.Add(deck[i]);
                deck.Remove(deck[i]);
                cards--;
            }
        }

        // --------------------------

        public void PlayCard(Player player, List<Card> hand, List<Card> tableStack)
        {
            throw new NotImplementedException();
        }

        // --------------------------

        public override void EndRound()
        {
            this.PlayedPlayerOne.ForEach(card => this.Deck.Add(card));
            this.HandPlayerOne.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerOne.Clear();
            this.HandPlayerOne.Clear();
            
            this.PlayedPlayerOne.ForEach(card => this.Deck.Add(card));
            this.HandPlayerTwo.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerTwo.Clear();
            this.HandPlayerTwo.Clear();
            
        }

        // --------------------------

        public override void EndGame()
        {
            this.CancelToken.Cancel();
            Announce(@" \b Ending the game... \b0\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);            
        }

        // --------------------------

        public override void Play(Player player1, Player player2)
        {
            Player playerOne;
            Player playerTwo;

            // --- PRELIMINALS

            ChoosePlayerOne(player1, player2, out playerOne, out playerTwo);

            // --- START

            while(this.Turn < 700 && this.CancelToken.IsCancellationRequested != true)
            {
                PlayRound(playerOne, playerTwo);
                Thread.Sleep(2000);                
            }
            Announce(@" \b Game Finished. \b0\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
        }

        // --------------------------

        private void ChoosePlayerOne(Player player1, Player player2, out Player playerOne, out Player playerTwo)
        {
            Random coin = new Random();
            int flipCoin = coin.Next(1, 3);

            if(flipCoin == 1)
            {
                playerOne = player1;
                playerTwo = player2;
            }
            else
            {
                playerOne = player2;
                playerTwo = player1;
            }
        }

        // --------------------------

        private void Announce(string text)
        {
            this.CleanLog += text;
            this.Log = "";
            this.Log += @"{\rtf1\ansi" + CleanLog+ @"}";
        }

        // ----------------------------------------------------
        // -------------------------- [ GAME LOGIC ]
        //----------------------------------------------------

        private void ResetAll()
        {
            this.truco = this.reTruco = this.valeCuatro = this.envido = this.realEnvido = this.faltaEnvido = this.flor = false;
            this.temporaryPoints = 0;
        }

        //----------------------------------------------------

        private void PlayRound(Player player1, Player player2)
        {
            this.Turn++; //+1 TO THE TURNS

            Announce(@" \b Round " + this.Turn + @"\b0.\line\line"); // I ANNOUNCE THE START OF THE TURN

            Thread.Sleep(1000); // A SHORT PAUSE
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // I TRIGGER THE NOTIFY EVENT

            Announce(@" \b Shuffling the deck... \b0\line\line"); // I ANNOUNCE THE DECK SHUFFLING
            this.Deck = ShuffleDeck(this.Deck); // DECK SHUFFLING ITSELF
            Thread.Sleep(1000);// SHORT PAUSE
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // NOTIFY

            Announce(@" \b Giving cards... \b0\line\line");
            GiveCards(this.Deck, this.HandPlayerOne);
            GiveCards(this.Deck, this.HandPlayerTwo); // I GIVE THE CARDS TO THE PLAYERS
            Thread.Sleep(1000);
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // NOTIFY AGAIN

            Announce(@" \b " + player1.Name + @" \b0 got 3 cards.\line The \b " + HandPlayerOne[0].Rank.ToString() + @" \b0 of \b " + HandPlayerOne[0].Suit.ToString() + @"\b0, the \b " + HandPlayerOne[1].Rank.ToString() +
                @" \b0 of \b " + HandPlayerOne[1].Suit.ToString() + @"\b0, and the \b " + HandPlayerOne[2].Rank.ToString() + @" \b0 of \b " + HandPlayerOne[2].Suit.ToString() + @" \b0.\line\line");

            Thread.Sleep(1000);
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);

            Announce(@" \b " + player2.Name + @" \b0 got 3 cards.\line The \b " + HandPlayerTwo[0].Rank.ToString() + @" \b0 of \b " + HandPlayerTwo[0].Suit.ToString() + @"\b0, the \b " + HandPlayerTwo[1].Rank.ToString() +
                @" \b0 of \b " + HandPlayerTwo[1].Suit.ToString() + @"\b0, and the \b " + HandPlayerTwo[2].Rank.ToString() + @" \b0 of \b " + HandPlayerTwo[2].Suit.ToString() + @" \b0.\line\line");

            Thread.Sleep(1000);
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);


            for (int i = 0; i < 3; i++)
            {
                PlayTurnOfPlayer(player1, HandPlayerOne, PlayedPlayerOne);
                PlayTurnOfPlayer(player2, HandPlayerTwo, PlayedPlayerTwo);
            }
            
            EndRound(); // ALL CARTS RETURN TO THE DECK
            Announce(@"----------------------------------------------------------- \line");            
        }

        private void PlayTurnOfPlayer(Player player, List<Card> hand, List<Card> tableStack)
        {
            //envido check

            //truco check

            //PlayCard(player, hand, tableStack);

        }

    }
}
