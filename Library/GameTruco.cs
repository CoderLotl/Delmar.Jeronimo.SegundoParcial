using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
        public override event EventHandler NotifyLogUpdate;
        public override event EventHandler NotifyEndGame;

        public List<Card> Deck { get; set; }
        public List<Card> HandPlayerOne { get; set; }
        public List<Card> HandPlayerTwo { get; set; }
        public List<Card> PlayedPlayerOne { get; set; }
        public List<Card> PlayedPlayerTwo { get; set; } 
        //public override 

        public GameTruco(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
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


        /// <summary>
        /// RETURNS A FULLY INSTANTIATED DECK.
        /// FIRST THE METHOD TRIES TO RETRIEVE A DECK FROM A JSON FILE. IF THERE'S NO FILE IN THE PARENT DIRECTORY
        /// THE METHOD CREATES A DECK AND THEN SERIALIZES THE FILE. THEN RETURNS THE DECK.
        /// ---
        /// RETORNA UN MAZO COMPLETAMENTE INSTANCIADO.
        /// EL METODO PRIMERO INTENTA DEVOLVER UN MAZO DESDE UN ARCHIVO JSON. SI EL ARCHIVO NO ESTA EN EL DIRECTORIO
        /// EL METODO CREA UN MAZO Y LUEGO LO SERIALIZA. DEVUELVE EL MAZO.
        /// </summary>
        /// <returns></returns>
        public List<Card> GenerateDeck()
        {
            List<Card> newDeck;

            JsonSerializer<List<Card>> jsonSerializer = new JsonSerializer<List<Card>>("TrucoDeck");

            newDeck = jsonSerializer.DeSerialize();

            if (newDeck == null)
            {
                newDeck = new List<Card>();

                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    for (int i = 1; i < 8; i++)
                    {
                        int relativeValue = CalculateRelativeValue(i, suit);

                        Card newCard = new Card(i, relativeValue, suit);
                        newDeck.Add(newCard);
                    }
                    for (int i = 10; i < 13; i++)
                    {
                        int relativeValue = CalculateRelativeValue(i, suit);

                        Card newCard = new Card(i, relativeValue, suit);
                        newDeck.Add(newCard);
                    }
                }

                jsonSerializer.Serialize(newDeck);
            }                        

            return newDeck;
        }

        /// <summary>
        /// SHUFFLES A FULL DECK AND RETURNS IT ONCE THE CARDS HAVE BEEN SHUFFLED.
        /// ---
        /// MEZCLA UN MAZO Y LO DEVUELVE UNA VEZ MEZCLADO.
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<Card> ShuffleDeck(List<Card> deck)
        {
            List<Card> shuffledDeck = new List<Card>();
            Random randomNumber = new Random();
            
            if(deck != null)
            {
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
            else
            {
                throw new ArgumentNullException("Argument is null");                
            }
        }

        
        /// <summary>
        /// CALCULATE THE RELATIVE VALUE OF THE CARD.
        /// ---
        /// CALCULA EL VALOR RELATIVO DE LA CARTA.
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        /// <returns></returns>
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

        
        /// <summary>
        /// PICKS A CARD FROM THE DECK AND PLACES IT IN THE PLAYER'S HAND.
        /// ---
        /// TOMA UNA CARTA DEL MAZO Y LA COLOCA EN LA MANO DEL JUGADOR.
        /// </summary>
        /// <param name="playerCards"></param>
        /// <param name="deck"></param>
        /// <exception cref="EmptyDeckException"></exception>
        public void DrawCard(List<Card> playerCards, List<Card> deck)
        {
            if (deck.Count > 0)
            {
                playerCards.Add(deck[0]);
                deck.Remove(deck[0]);
            }
            else
            {
                throw new EmptyDeckException("The deck is empty.");
            }
        }

        
        /// <summary>
        /// GIVES 3 CARDS TO THE PLAYER, PLACING THEM IN THE PLAYER'S HAND.
        /// ---
        /// LE DA 3 CARTAS AL JUGADOR, COLOC'ANDOLAS EN LA MANO DEL JUGADOR.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="playerCards"></param>
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

        
        /// <summary>
        /// PLAYS A RANDOM CARD AND PLACES IT DOWN IN THE GIVEN STACK OF CARDS.
        /// ---
        /// JUEGA UNA CARTA AL AZAR Y LA COLOCA EN EL STACK DE CARTAS DESIGNADO.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hand"></param>
        /// <param name="tableStack"></param>
        public void PlayCard(Player player, List<Card> hand, List<Card> tableStack)
        {
            Random randomNum = new Random();
            int cardToPlay = randomNum.Next(hand.Count);

            tableStack.Add(hand[cardToPlay]);

            Announce(@"\b " + player.Name + @" \b0 plays the \b " + hand[cardToPlay].Rank.ToString() + @" \b0 of \b " + hand[cardToPlay].Suit.ToString() + @" \b0\line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            hand.RemoveAt(cardToPlay);
            Thread.Sleep(1000);
        }

        
        /// <summary>
        /// RESETS THE PLAYER'S HAND AND THE CARDS THE PLAYER PLAYED, RETURNING ALL THE CARDS TO THE DECK.
        /// ---
        /// RESETEA LA MANO DEL JUGADOR Y LAS CARTAS QUE JUGO, DEVOLVIENDOLAS AL MAZO.
        /// </summary>
        public override void EndRound()
        {
            this.PlayedPlayerOne.ForEach(card => this.Deck.Add(card));
            this.HandPlayerOne.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerOne.Clear();
            this.HandPlayerOne.Clear();
            
            this.PlayedPlayerTwo.ForEach(card => this.Deck.Add(card));
            this.HandPlayerTwo.ForEach(card => this.Deck.Add(card));
            this.PlayedPlayerTwo.Clear();
            this.HandPlayerTwo.Clear();            
        }

        
        /// <summary>
        /// SETS THE CANCEL TOKEN TO TRUE. ANNOUNCES THE IMMINENT END OF THE GAME.
        /// ---
        /// SETEA EL CANCEL TOKEN A VERDADERO. ANUNCIA EL FINAL INMINENTE DEL JUEGO.
        /// </summary>
        public override void EndGame()
        {
            this.CancelToken.Cancel();
            Announce(@" \line\b\i [ Finishing the game after this round... ] \b0\i0\line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);            
        }

        
        /// <summary>
        /// STARTS PLAYING THE GAME.
        /// ---
        /// COMIENZA A JUGAR EL JUEGO.
        /// </summary>
        public override void Play()
        {
            Player playerOne = this.Player1;
            Player playerTwo;
            
            while(this.Turn < 4 && this.CancelToken.IsCancellationRequested != true)
            {
                ChoosePlayerOne(Player1, Player2, out playerOne, out playerTwo);
                this.Turn++;
                PlayRound(playerOne, playerTwo);
                Thread.Sleep(2000);                
            }

            Announce(@" \b\i Game Finished. - - - Please wait...\i0\b0\line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(2000);

            DecideWinner(Player1, Player2);

            Announce(@" \b\i Updating players statics...\i0\b0\line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            UpdatePlayersStats(Player1, Player2);
                        
            NotifyEndGame?.Invoke(this, EventArgs.Empty);

        }


        /// <summary>
        /// DECIDES THE WINNER BASED ON THEIR SCORE AND ADDS THE CORRESPONDING POINTS TO THEIR COUNTERS, THEN ANNOUNCED THE WINNER.
        /// ---
        /// DECIDE EL GANADOR BASADO EN SU PUNTAJE Y AGREGA LOS PUNTOS CORRESPONDIENTES A SUS CONTADORES, LUEGO ANUNCIA EL GANADOR.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void DecideWinner(Player player1, Player player2)
        {
            player1.GamesPlayed++;
            player2.GamesPlayed++;

            Announce(@" \b " + player1.Name + @" \b0 score is: " + PlayerOneScore.ToString() + @" \line" +
                     @" \b " + player2.Name + @" \b0 score is: " + PlayerTwoScore.ToString() + @" \line");

            if(PlayerOneScore > PlayerTwoScore)
            {                
                player1.GamesWon++;
                player2.GamesLost++;                
                Announce(@" \b " + player1.Name + @" is the Winner!\b0\line");
                NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(2000);
            }
            else if (PlayerOneScore < PlayerTwoScore)
            {
                player2.GamesWon++;
                player1.GamesLost++;
                Announce(@" \b " + player2.Name + @" is the Winner!\b0\line");
                NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(2000);
            }
            else
            {
                player1.GamesTied++;
                player2.GamesTied++;
                Announce(@" \b Both players ended in ties.\b0\line");
                NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(2000);
            }
        }

        
        /// <summary>
        /// CONNECTS TO THE DATABASE AND UPDATES THE PLAYERS'S STATISTICS. - IF THE DATABASE IS UNREACHABLE
        /// THE METHOD POPS A MESSAGEBOX INFORMING THE USER ABOUT THIS.
        /// ---
        /// SE CONECTA CON LA BASE DE DATOS Y ACTUALIZA LAS ESTADISTICAS DE LOS JUGADORES. - SI LA BASE DE DATOS ES INALCANZABLE
        /// EL METODO LANZA UNA VENTANA INFORMANDO AL USUARIO SOBRE ESTO.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void UpdatePlayersStats(Player player1, Player player2)
        {
            DataAccess dataAccess = new DataAccess(SystemManager.ConnectionString);

            if(dataAccess.TestConnection() == true)
            {
                dataAccess.UpdatePlayer(player1);
                dataAccess.UpdatePlayer(player2);
            }
            else
            {
                Action("Unable to connect to the Database.\nThe players won't be updated.");
            }
        }

        
        /// <summary>
        /// CHOOSES THE PLAYER ONE AND TWO AT RANDOM.
        /// ---
        /// ELIGE EL JUGADOR UNO Y DOS AL AZAR.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
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

        
        /// <summary>
        /// ADDS SOME TEXT TO THE LOG AND THE CLEAN LOG. - THE CLEAN LOG HAS NO RTF FORMAT, WHILE THE LOG HAS.
        /// ---
        /// AGREGA ALGUN TEXTO AL LOG Y AL LOG LIMPIO. - EL LOG LIMPIO NO TIENE FORMATO DE TEXTO ENRIQUECIDO,
        /// MIENTRAS QUE EL LOG SI.
        /// </summary>
        /// <param name="text"></param>
        private void Announce(string text)
        {
            this.CleanLog += text;
            this.Log = "";
            this.Log += @"{\rtf1\ansi" + CleanLog+ @"}";
        }

        
        /// <summary>
        /// PLAYS A ROUND BETWEEN 2 PLAYERS.
        /// ---
        /// JUEGA UNA RONDA ENTRE 2 JUGADORES.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void PlayRound(Player player1, Player player2)
        {
            bool winnerRound = false;
            Player isHand = player1;
            Player lastToPlay = player2;
            bool checkIsNeeded = false;
            bool envidoCalled = false;
            int envidoWanted = 0;
            bool trucoCalled = false;
            int trucoWanted = 0;
            int player1EnvidoPoints = 0;
            int player2EnvidoPoints = 0;
            int player1HandScore = 0;
            int player2HandScore = 0;
                                    

            AnnounceRoundPreamble(player1, player2);
            

            while (winnerRound == false && this.CancelToken.IsCancellationRequested != true)
            {
                if (isHand == player1)
                {                    
                    PlayTurnOfPlayer(player1, player2, ref lastToPlay, isHand, 1, PlayedPlayerOne, ref envidoCalled, ref envidoWanted, ref trucoCalled, ref trucoWanted,
                        ref player1EnvidoPoints, ref player2EnvidoPoints, ref winnerRound, ref checkIsNeeded);

                    if (winnerRound == true)
                    {
                        break;
                    }

                    PlayTurnOfPlayer(player1, player2, ref lastToPlay, isHand, 2, PlayedPlayerTwo, ref envidoCalled, ref envidoWanted, ref trucoCalled, ref trucoWanted,
                        ref player1EnvidoPoints, ref player2EnvidoPoints, ref winnerRound, ref checkIsNeeded);
                    if (checkIsNeeded == true)
                    {
                        CheckThisHandWinner(player1, player2, ref lastToPlay, ref isHand, ref checkIsNeeded, ref player1HandScore, ref player2HandScore, ref winnerRound, trucoWanted);
                    }

                }
                else
                {                    
                    PlayTurnOfPlayer(player1, player2, ref lastToPlay, isHand, 2, PlayedPlayerTwo, ref envidoCalled, ref envidoWanted, ref trucoCalled, ref trucoWanted,
                        ref player1EnvidoPoints, ref player2EnvidoPoints, ref winnerRound, ref checkIsNeeded);

                    if (winnerRound == true)
                    {
                        break;
                    }

                    PlayTurnOfPlayer(player1, player2, ref lastToPlay, isHand, 1, PlayedPlayerOne, ref envidoCalled, ref envidoWanted, ref trucoCalled, ref trucoWanted,
                        ref player1EnvidoPoints, ref player2EnvidoPoints, ref winnerRound, ref checkIsNeeded);
                    if (checkIsNeeded == true)
                    {
                        CheckThisHandWinner(player2, player1, ref lastToPlay, ref isHand, ref checkIsNeeded, ref player1HandScore, ref player2HandScore, ref winnerRound, trucoWanted);
                    }
                }                                
            }

            EndRound();
            Announce(@"----------------------------------------------------------- \line");
        }


        /// <summary>
        /// ANNOUNCES THE PREAMBLE OF THE GAME, SHOWING THE CARDS DRAWN OF EACH PLAYER.
        /// ---
        /// ANUNCIA EL PREAMBULO DEL JUEGO, MOSTRANDO LAS CARTAS SACADAS DE CADA JUGADOR.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void AnnounceRoundPreamble(Player player1, Player player2)
        {
            Announce(@" \b Round " + this.Turn + @"\b0.\line\line"); // I ANNOUNCE THE START OF THE TURN            

            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // I TRIGGER THE NOTIFY EVENT
            Thread.Sleep(1000); // A SHORT PAUSE

            Announce(@" \b Shuffling the deck... \b0\line\line"); // I ANNOUNCE THE DECK SHUFFLING
            this.Deck = ShuffleDeck(this.Deck); // DECK SHUFFLING ITSELF
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // NOTIFY
            Thread.Sleep(1000);// SHORT PAUSE

            Announce(@" \b Giving cards... \b0\line\line");
            GiveCards(this.Deck, this.HandPlayerOne);
            GiveCards(this.Deck, this.HandPlayerTwo); // I GIVE THE CARDS TO THE PLAYERS
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty); // NOTIFY AGAIN
            Thread.Sleep(1000);

            Announce(@" \b " + player1.Name + @" \b0 got 3 cards.\line The \b " + HandPlayerOne[0].Rank.ToString() + @" \b0 of \b " + HandPlayerOne[0].Suit.ToString() + @"\b0, the \b " + HandPlayerOne[1].Rank.ToString() +
                @" \b0 of \b " + HandPlayerOne[1].Suit.ToString() + @"\b0, and the \b " + HandPlayerOne[2].Rank.ToString() + @" \b0 of \b " + HandPlayerOne[2].Suit.ToString() + @" \b0.\line\line");

            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);

            Announce(@" \b " + player2.Name + @" \b0 got 3 cards.\line The \b " + HandPlayerTwo[0].Rank.ToString() + @" \b0 of \b " + HandPlayerTwo[0].Suit.ToString() + @"\b0, the \b " + HandPlayerTwo[1].Rank.ToString() +
                @" \b0 of \b " + HandPlayerTwo[1].Suit.ToString() + @"\b0, and the \b " + HandPlayerTwo[2].Rank.ToString() + @" \b0 of \b " + HandPlayerTwo[2].Suit.ToString() + @" \b0.\line\line");

            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
            Announce(@"* * * * * * * * * * \line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(2000);
        }

        
        /// <summary>
        /// CHECKS AND ANNOUNCES THE WINNER OF THE HAND.
        /// ---
        /// CHEQUEA Y ANUNCIA EL GANADOR DE LA MANO.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="lastToPlay"></param>
        /// <param name="isHand"></param>
        /// <param name="checkIsNeeded"></param>
        /// <param name="player1HandScore"></param>
        /// <param name="player2HandScore"></param>
        /// <param name="winnerRound"></param>
        /// <param name="trucoWanted"></param>
        private void CheckThisHandWinner(Player player1, Player player2, ref Player lastToPlay, ref Player isHand, ref bool checkIsNeeded,
            ref int player1HandScore, ref int player2HandScore, ref bool winnerRound, int trucoWanted)
        {
            Card card1;
            Card card2;

            if(PlayedPlayerOne.Count > 0 && PlayedPlayerTwo.Count > 0)
            {
                card1 = PlayedPlayerOne[PlayedPlayerOne.Count - 1];
                card2 = PlayedPlayerTwo[PlayedPlayerTwo.Count - 1];

                if(card1.RelativeRank > card2.RelativeRank)
                {
                    Announce(@" The \b " + card1.Rank + @" \b0 of \b " + card1.Suit.ToString() + @" \b0 kills the \b " + card2.Rank + @" \b0 of \b " + card2.Suit.ToString() + @". \b0\line\line");
                    isHand = player1;
                    player1HandScore++;
                    lastToPlay = player2;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    Announce(@" " + player1.Name + @" is hand now. \line\line");
                    Announce(@"* * * * * * * * * * \line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(2000);
                }
                else if (card1.RelativeRank < card2.RelativeRank)
                {
                    Announce(@" The \b " + card2.Rank + @" \b0 of \b " + card2.Suit.ToString() + @" \b0 kills the \b " + card1.Rank + @" \b0 of \b " + card1.Suit.ToString() + @". \b0\line\line");
                    isHand = player2;
                    player2HandScore++;
                    lastToPlay = player1;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    Announce(@" " + player2.Name + @" is hand now. \line\line");
                    Announce(@"* * * * * * * * * * \line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(2000);
                }
                else
                {
                    if(isHand == player1)
                    {
                        Announce(@" " + player1.Name + @" wins this one since they're Hand. \line\line");
                        player1HandScore++;
                        Announce(@"* * * * * * * * * * \line");
                        NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Announce(@" " + player2.Name + @" wins this one since they're Hand. \line\line");
                        player2HandScore++;
                        Announce(@"* * * * * * * * * * \line");
                        NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                        Thread.Sleep(2000);
                    }
                }                

                if(player1HandScore >= 2 || player2HandScore >= 2)
                {
                    if(player1HandScore >= 2)
                    {
                        Announce(@" \b " + player1.Name + @" wins this round.\b0\line\line");

                        if (trucoWanted != 0)
                        {
                            this.PlayerOneScore+=2;
                            Announce(@" \b +2pt for " + player1.Name + @"\b0\line\line");
                        }
                        else
                        {
                            this.PlayerOneScore++;
                            Announce(@" \b +1pt for " + player1.Name + @"\b0\line\line");
                        }

                        winnerRound = true;
                        NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                        Thread.Sleep(2000);
                    }
                    else if(player2HandScore >= 2)
                    {
                        Announce(@" \b " + player2.Name + @" wins this round.\b0\line\line");

                        if (trucoWanted != 0)
                        {
                            this.PlayerTwoScore+=2;
                            Announce(@" \b +2pt for " + player2.Name + @"\b0\line\line");
                        }
                        else
                        {
                            this.PlayerTwoScore++;
                            Announce(@" \b +1pt for " + player2.Name + @"\b0\line\line");
                        }

                        winnerRound = true;
                        NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                        Thread.Sleep(2000);
                    }
                }

                checkIsNeeded = false;
            }
        }

        
        /// <summary>
        /// PLAYS THE TURN OF THE CURRENT PLAYER. ANSWERS THE CALLS, MAKES CALLS, AND PLAYS A CARD.
        /// ---
        /// JUEGA EL TURNO DEL JUGADOR ACTUAL. RESPONDE LLAMADOS, HACE LLAMADOS, Y JUEGA UNA CARTA.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="lastToPlay"></param>
        /// <param name="isHand"></param>
        /// <param name="playerOrder"></param>
        /// <param name="tableStack"></param>
        /// <param name="envidoCalled"></param>
        /// <param name="envidoWanted"></param>
        /// <param name="trucoCalled"></param>
        /// <param name="trucoWanted"></param>
        /// <param name="player1EnvidoPoints"></param>
        /// <param name="player2EnvidoPoints"></param>
        /// <param name="winnerRound"></param>
        /// <param name="checkIsNeeded"></param>
        private void PlayTurnOfPlayer(Player player1, Player player2, ref Player lastToPlay, Player isHand, int playerOrder, List<Card> tableStack,
            ref bool envidoCalled, ref int envidoWanted, ref bool trucoCalled, ref int trucoWanted, ref int player1EnvidoPoints,
            ref int player2EnvidoPoints, ref bool winnerRound, ref bool checkIsNeeded)
        {            
            Player currentPlayer;
            Player oponent;
            List<Card> currentPlayerHand;

            if (playerOrder == 1)
            {
                currentPlayer = player1;
                oponent = player2;
                currentPlayerHand = HandPlayerOne;
            }
            else
            {
                currentPlayer = player2;
                oponent = player1;
                currentPlayerHand = HandPlayerTwo;
            }            

            if(envidoCalled == false || (envidoCalled == true && envidoWanted == 0))
            {
                CheckForEnvido(player1, player2, ref envidoCalled, ref envidoWanted, trucoCalled,
                ref player1EnvidoPoints, ref player2EnvidoPoints, currentPlayer, oponent, ref checkIsNeeded, isHand);
            }
            else
            {
                CheckForTruco(player1, player2, ref trucoCalled, ref trucoWanted, currentPlayer, oponent, ref winnerRound, ref checkIsNeeded);
            }            

            if(lastToPlay != currentPlayer)
            {
                if ((trucoCalled == false || (trucoCalled == true && trucoWanted != 0)) && (envidoCalled == false || (envidoCalled == true && envidoWanted != 0)))
                {                    
                    PlayCard(currentPlayer, currentPlayerHand, tableStack);
                    checkIsNeeded = true;
                    lastToPlay = currentPlayer;
                }
            }

        }

        
        /// <summary>
        /// CHECKS IF TRUCO HAS BEEN CALLED PREVIOUSLY OR NOT. BASED ON THAT, DECIDES TO CALL TRUCO OR HOW TO ANSWER TO A PREVIOUS TRUCO
        /// CALL. FINALLY MAKES THE CALL AND SETS THE BOOLEAN VARIABLES TO THEIR CORRESPONDING VALUES.
        /// ---
        /// CHEQUEA SI EL TRUCO YA HA SIDO CANTADO PREVIAMENTE O NO. BASADO EN ESO, DECIDE CANTAR TRUCO O COMO RESPONDER A UNA LLAMADA PREVIA
        /// DE TRUCO. FINALMENTE HACE EL LLAMADO Y SETEA LAS VARIABLES BOOLEANAS A SUS CORRESPONDIENTES VALORES.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="trucoCalled"></param>
        /// <param name="trucoWanted"></param>
        /// <param name="currentPlayer"></param>
        /// <param name="oponent"></param>
        /// <param name="winnerRound"></param>
        /// <param name="checkIsNeeded"></param>
        private void CheckForTruco(Player player1, Player player2, ref bool trucoCalled, ref int trucoWanted, Player currentPlayer, Player oponent,
            ref bool winnerRound, ref bool checkIsNeeded)
        {
            Random randomNum = new Random();
            int callingChance = randomNum.Next(100);

            if (trucoCalled == false && callingChance >= 50)
            {
                Call(currentPlayer, oponent, 2, ref trucoCalled, ref trucoWanted);
            }
            else if( trucoCalled == true & trucoWanted == 0)
            {
                if( callingChance >= 50)
                {
                    Call(currentPlayer, oponent, 3, ref trucoCalled, ref trucoWanted);
                }
                else
                {
                    Call(currentPlayer, oponent, 4, ref trucoCalled, ref trucoWanted);
                    if (currentPlayer == player1)
                    {
                        this.PlayerTwoScore++;
                    }
                    else
                    {
                        this.PlayerOneScore++;
                    }
                    Announce(@" \b " + oponent.Name + @" wins this round.\b0\line\line");
                    Announce(@" \b +1pt for " + oponent.Name + @"\b0\line\line");
                    winnerRound = true;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(2000);
                }
            }
        }


        /// <summary>
        /// CHECKS IF ENVIDO HAS BEEN CALLED PREVIOUSLY OR NOT. BASED ON THAT, DECIDES TO CALL ENVIDO OR HOW TO ANSWER TO A PREVIOUS ENVIDO
        /// CALL. FINALLY MAKES THE CALL AND SETS THE BOOLEAN VARIABLES TO THEIR CORRESPONDING VALUES.
        /// ---
        /// CHEQUEA SI EL ENVIDO YA HA SIDO CANTADO PREVIAMENTE O NO. BASADO EN ESO, DECIDE CANTAR ENVIDO O COMO RESPONDER A UNA LLAMADA PREVIA
        /// DE ENVIDO. FINALMENTE HACE EL LLAMADO Y SETEA LAS VARIABLES BOOLEANAS A SUS CORRESPONDIENTES VALORES.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="envidoCalled"></param>
        /// <param name="envidoWanted"></param>
        /// <param name="trucoCalled"></param>
        /// <param name="player1EnvidoPoints"></param>
        /// <param name="player2EnvidoPoints"></param>
        /// <param name="currentPlayer"></param>
        /// <param name="oponent"></param>
        /// <param name="checkIsNeeded"></param>
        /// <param name="isHand"></param>
        private void CheckForEnvido(Player player1, Player player2, ref bool envidoCalled, ref int envidoWanted, bool trucoCalled,
            ref int player1EnvidoPoints, ref int player2EnvidoPoints, Player currentPlayer, Player oponent, ref bool checkIsNeeded, Player isHand)
        {
            if (trucoCalled == false)
            {
                Random randomNum = new Random();
                int callingChance = randomNum.Next(100);

                if (envidoCalled == false)
                {
                    if (callingChance >= 50)
                    {
                        Call(currentPlayer, oponent, 1, ref envidoCalled, ref envidoWanted);
                        if (currentPlayer == player1)
                        {
                            player1EnvidoPoints = CalculateEnvidoPoints(HandPlayerOne);
                        }
                        else
                        {
                            player2EnvidoPoints = CalculateEnvidoPoints(HandPlayerTwo);
                        }
                        checkIsNeeded = false;
                    }
                }
                else if (envidoCalled == true && envidoWanted == 0)
                {
                    if (callingChance >= 50)
                    {
                        Call(currentPlayer, oponent, 3, ref envidoCalled, ref envidoWanted);
                        if (currentPlayer == player1)
                        {
                            player1EnvidoPoints = CalculateEnvidoPoints(HandPlayerOne);
                            ResolveEnvido(player1, player2, player1EnvidoPoints, player1EnvidoPoints, isHand);
                        }
                        else
                        {
                            player2EnvidoPoints = CalculateEnvidoPoints(HandPlayerTwo);
                            ResolveEnvido(player1, player2, player1EnvidoPoints, player2EnvidoPoints, isHand);
                        }                        
                    }
                    else
                    {
                        Call(currentPlayer, oponent, 4, ref envidoCalled, ref envidoWanted);
                        if (currentPlayer == player1)
                        {
                            this.PlayerTwoScore++;
                        }
                        else
                        {
                            this.PlayerOneScore++;
                        }                        
                    }
                    checkIsNeeded = false;
                }
            }
        }

        
        /// <summary>
        /// RESOLVES THE ENVIDO BASED ON WHO'S HAND AND THE ENVIDO SCORE OF EACH PLAYER, ASSIGNING POINTS IF IT'S DUE.
        /// ---
        /// RESUELVE EL ENVIDO BASANDOSE EN QUIEN ES MANO Y EN LOS PUNTOS DE ENVIDO DE CADA JUGADOR, ASIGNANDO PUNTOS SI ES DEBIDO.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="player1EnvidoPoints"></param>
        /// <param name="player2EnvidoPoints"></param>
        /// <param name="isHand"></param>
        private void ResolveEnvido(Player player1, Player player2, int player1EnvidoPoints, int player2EnvidoPoints, Player isHand)
        {
            Announce(@" \b " + player1.Name + @"\b0:  " + player1EnvidoPoints.ToString() + @"  \line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
            Announce(@" \b " + player2.Name + @"\b0:  " + player2EnvidoPoints.ToString() + @"  \line\line");
            NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
            if(player1EnvidoPoints > player2EnvidoPoints)
            {
                this.PlayerOneScore++;
                Announce(@" \b +1pt for " + player1.Name + @"\b0\line\line");
                Announce(@"* * * * *  \line");
                NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(2000);
            }
            else if(player2EnvidoPoints > player1EnvidoPoints)
            {
                this.PlayerTwoScore++;
                Announce(@" \b +1pt for " + player2.Name + @"\b0\line\line");                
                Announce(@"* * * * *  \line");
                NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(2000);
            }
            else
            {
                if( player1 == isHand)
                {
                    this.PlayerOneScore++;
                    Announce(@" \b " + player1.Name + @" wins since they're Hand.\b0\line\line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    Announce(@" \b +1pt for " + player1.Name + @"\b0\line\line");
                    Announce(@"* * * * *  \line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(2000);
                }
                else
                {
                    this.PlayerTwoScore++;
                    Announce(@" \b " + player2.Name + @" wins since they're Hand.\b0\line\line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    Announce(@" \b +1pt for " + player2.Name + @"\b0\line\line");
                    Announce(@"* * * * *  \line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(2000);
                }
            }
        }

        
        /// <summary>
        /// CALCULATES THE ENVIDO VALUE OF THE HAND AND RETURNS IT.
        /// ---
        /// CALCULA EL VALOR DE ENVIDO DE LA MANO Y LO RETORNA.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private int CalculateEnvidoPoints(List<Card> hand)
        {
            int[] envidoPointsPerSuit = new int[4] { 0, 0, 0, 0 }; // cups, swords, golds, clubs
            int cups = 0;            
            int swords = 0;            
            int golds = 0;            
            int clubs = 0;            

            int points = 0;

            foreach(Card card in hand)
            {
                switch (card.Suit)
                {
                    case Suit.Cups:
                        cups++;
                        if (card.Rank <= 7)
                        {
                            envidoPointsPerSuit[0] += card.Rank;
                        }
                        break;
                    case Suit.Swords:
                        swords++;
                        if (card.Rank <= 7)
                        {
                            envidoPointsPerSuit[1] += card.Rank;
                        }
                        break;
                    case Suit.Golds:
                        golds++;
                        if (card.Rank <= 7)
                        {
                            envidoPointsPerSuit[2] += card.Rank;
                        }
                        break;
                    case Suit.Clubs:
                        clubs++;
                        if (card.Rank <= 7)
                        {
                            envidoPointsPerSuit[3] += card.Rank;
                        }
                        break;
                }
            }

            if( cups >= 2 || swords >= 2 || golds >= 2 || clubs >= 2)
            {
                if(cups >= 2)
                {
                    envidoPointsPerSuit[0] += 20;
                }
                if(swords >= 2)
                {
                    envidoPointsPerSuit[1] += 20;
                }
                if (golds >= 2)
                {
                    envidoPointsPerSuit[2] += 20;
                }
                if (clubs >= 2)
                {
                    envidoPointsPerSuit[3] += 20;
                }                
            }

            foreach(int envidoPoints in envidoPointsPerSuit)
            {
                if(envidoPoints > points)
                {
                    points = envidoPoints;
                }
            }

            return points;
        }

        
        /// <summary>
        /// MAKES AND ANSWERS THE CALLS OF TRUCO AND ENVIDO. RETURNS THE VALUES BY REFERENCE.
        /// ---
        /// HACE Y RESPONDE LOS LLAMADOS DE TRUCO Y ENVIDO. RETORNA LOS VALORES POR REFERENCIA.
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="oponent"></param>
        /// <param name="option"></param>
        /// <param name="call"></param>
        /// <param name="wanted"></param>
        private void Call(Player currentPlayer, Player oponent, int option, ref bool call, ref int wanted)
        {
            switch (option)
            {
                case 1:
                    Announce(@" \b " + currentPlayer.Name + @"\b0: Envido!" + @"\line\line");
                    call = true;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    break;
                case 2:
                    Announce(@" \b " + currentPlayer.Name + @"\b0: Truco!" + @"\line\line");
                    call = true;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    break;
                case 3:
                    Announce(@" \b " + currentPlayer.Name + @"\b0: Quiero!" + @"\line\line");                    
                    wanted = 1;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    break;
                case 4:
                    Announce(@" \b " + currentPlayer.Name + @"\b0: No quiero!" + @"\line\line");
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    Announce(@" \b +1pt for "+ oponent.Name + @"\b0\line\line");
                    wanted = 2;
                    NotifyLogUpdate?.Invoke(this, EventArgs.Empty);
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
