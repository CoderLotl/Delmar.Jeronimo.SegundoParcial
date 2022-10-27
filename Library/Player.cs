using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Player
    {
        int id;
        string name;        
        int gamesPlayed;
        int gamesWon;
        int gamesLost;

        public Player(int id, string name, int gamesPlayed, int gamesWon, int gamesLost)
        {
            this.id = id;
            this.name = name;
            this.gamesPlayed = gamesPlayed;
            this.gamesWon = gamesWon;
            this.gamesLost = gamesLost;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int GamesPlayed { get => gamesPlayed; set => gamesPlayed = value; }
        public int GamesWon { get => gamesWon; set => gamesWon = value; }
        public int GamesLost { get => gamesLost; set => gamesLost = value; }        

        public override string ToString()
        {
            return "Name: "+this.Name+"\nGames played: "+this.GamesPlayed+"\nGames won: "+this.gamesWon+"\nGames lost: "+this.gamesLost;
        }
    }
}
