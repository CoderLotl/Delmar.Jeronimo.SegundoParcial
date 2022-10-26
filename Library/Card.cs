using System;
using System.Collections.Generic;

namespace Library
{

    public class Card
    {
        private int rank;
        private int relativeRank;
        private Suit suit;

        public Card(int rank, int relativeRank, Suit suit)
        {
            this.rank = rank;
            this.relativeRank = relativeRank;
            this.suit = suit;
        }

        public int Rank { get => rank; }
        public Suit Suit { get => suit; }
        public float RelativeRank { get => relativeRank; }

    }
}
