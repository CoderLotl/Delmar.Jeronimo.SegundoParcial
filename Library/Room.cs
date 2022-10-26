using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library
{
    public class Room
    {
        string name;
        List<Player> players;
        string log;        
        Game newGame;
        Task newTask;
        bool GameIsFinished;

        public Room(string name, Player player1, Player player2,string Text, GameType gameType, GameSubType gameSubType)
        {
            this.name = name + " | Game type: "+ gameType + " | Game sub-type: " + gameSubType;
            // --- SETTING THE PLAYERS
            this.players = new List<Player>();
            this.players.Add(player1); this.players.Add(player2);
            // --- SETTING THE LOG
            this.log = "";

            this.GameIsFinished = false;
            // --- SETTING THE GAME BY THE TYPE AND SUBTYPE
            this.newGame = InitializeGame(gameType, gameSubType);
            // --- STARTING A GAME
            this.newTask = new Task(() => this.newGame.Play(this, Text));
            newTask.Start();
        }

        public string Name { get => name; }        
        public List<Player> Players { get => players; set => players = value; }
        public string Log { get => log; set => log = value; }
        public Task NewTask { get => newTask; set => newTask = value; }
        public Game NewGame { get => newGame; set => newGame = value; }
        public bool GameFinished { get => GameIsFinished; set => GameIsFinished = value; }

        private Game InitializeGame(GameType gameType, GameSubType gameSubType)
        {
            Game newGame = null;

            if (gameType is GameType.Cards)
            {
                if (gameSubType is GameSubType.Truco)
                {
                    newGame = new GameTruco();
                }
            }

            return newGame;
        }

    }
}
