using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    /// <summary>
    /// HERE ARE ALL THE MECHANICS COMMON TO ALL CARD GAMES.
    /// </summary>
    public interface ICardGame
    {

        public List<Card> Deck { get; set; }
        public List<Card> HandPlayerOne { get; set; }
        public List<Card> HandPlayerTwo { get; set; }
        public List<Card> PlayedPlayerOne { get; set; }
        public List<Card> PlayedPlayerTwo { get; set; }


        List<Card> GenerateDeck();
        List<Card> ShuffleDeck(List<Card> deck);
        void GiveCards(List<Card> deck, List<Card> playerCards);
        void DrawCard(List<Card> playerCards, List<Card> deck);
        void PlayCard(Player player, List<Card> hand, List<Card> tableStack);
    }
}
