using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum GameType
    {
        Cards,
        Dices
    }

    public enum GameSubType
    {
        Chinchon,
        Truco,
        Escoba,
        Uno,
        Generala
    }

    public abstract class Game
    {
        string log;
        int playerOneScore;
        int playerTwoScore;
        int match;
        int turn;

        //----------------------------------------

        public string Log { get => log; set => log = value; }        
        public int PlayerOneScore { get => playerOneScore; set => playerOneScore = value; }
        public int PlayerTwoScore { get => playerTwoScore; set => playerTwoScore = value; }
        public int Match { get => match; set => match = value; }
        public int Turn { get => turn; set => turn = value; }

        //----------------------------------------

        public virtual void Play(string text) { }

        public abstract void EndGame();
        
    }
}
