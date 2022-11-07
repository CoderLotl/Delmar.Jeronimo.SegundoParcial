using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library
{

    public class Card
    {
        private int rank;
        private int relativeRank;
        private Suit suit;
        
        public Card(int Rank, int RelativeRank, Suit Suit)
        {
            this.rank = Rank;
            this.relativeRank = RelativeRank;
            this.suit = Suit;
        }

        public int Rank { get => rank; set => rank = value;  }
        public Suit Suit { get => suit; set => suit = value; }
        public int RelativeRank { get => relativeRank; set => relativeRank = value; }        
    }
}
