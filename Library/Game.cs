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
        Cards        
    }

    public enum GameSubType
    {        
        Truco
    }

    public abstract class Game
    {
        Player player1;
        Player player2;
        string log;
        string cleanLog;
        int playerOneScore;
        int playerTwoScore;
        int match;
        int turn;
        Action<string> action;

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
        public Action<string> Action { get => action; set => action = value; }
        public Player Player1 { get => player1; set => player1 = value; }
        public Player Player2 { get => player2; set => player2 = value; }

        //----------------------------------------

        public abstract void Play();
        public abstract void EndRound();
        public abstract void EndGame();
        
    }
}
