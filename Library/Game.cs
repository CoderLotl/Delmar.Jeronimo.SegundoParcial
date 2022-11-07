using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        string cleanLog;
        int playerOneScore;
        int playerTwoScore;
        int match;
        int turn;       

        private CancellationTokenSource cancelToken;


        public abstract event EventHandler NotifyLogUpdate;
        public abstract event EventHandler NotifyEndGame;

        //----------------------------------------

        public string Log { get => log; set => log = value; }        
        public int PlayerOneScore { get => playerOneScore; set => playerOneScore = value; }
        public int PlayerTwoScore { get => playerTwoScore; set => playerTwoScore = value; }
        public int Match { get => match; set => match = value; }
        public int Turn { get => turn; set => turn = value; }
        public string CleanLog { get => cleanLog; set => cleanLog = value; }
        public CancellationTokenSource CancelToken { get => cancelToken; set => cancelToken = value; }

        //----------------------------------------

        //public virtual void OldPlay(string text) { }

        public abstract void Play(Player player1, Player player2);
        public abstract void EndRound();
        public abstract void EndGame();
        
    }
}
