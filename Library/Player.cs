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
        int gamesTied;

        public Player(int id, string name, int gamesPlayed, int gamesWon, int gamesLost, int gamesTied)
        {
            this.id = id;
            this.name = name;
            this.gamesPlayed = gamesPlayed;
            this.gamesWon = gamesWon;
            this.gamesLost = gamesLost;
            this.gamesTied = gamesTied;
        }

        public Player()
        {

        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int GamesPlayed { get => gamesPlayed; set => gamesPlayed = value; }
        public int GamesWon { get => gamesWon; set => gamesWon = value; }
        public int GamesLost { get => gamesLost; set => gamesLost = value; }
        public int GamesTied { get => gamesTied; set => gamesTied = value; }

        public override string ToString()
        {
            return "Name: "+this.Name+"\n\nGames played: "+this.GamesPlayed+"\nGames won: "+this.gamesWon+"\nGames lost: "+this.gamesLost+"\nGames tied: "+this.gamesTied;
        }
    }
}
