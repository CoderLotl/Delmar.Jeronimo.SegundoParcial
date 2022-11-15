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
        bool gameConcluded;
        List<Player> players;             
        Game newGame;
        Task newTask;
        DataAccess newSQLConnection;

        public Room(string name, Player player1, Player player2, GameType gameType, GameSubType gameSubType)
        {
            this.name = name + " | Game type: "+ gameType + " | Game sub-type: " + gameSubType;
            // --- SETTING THE PLAYERS
            this.players = new List<Player>();
            this.players.Add(player1); this.players.Add(player2);

            this.gameConcluded = false;

            this.newSQLConnection = new DataAccess(); // FOR SAVING LATER

            // --- SETTING THE GAME BY THE TYPE AND SUBTYPE
            this.newGame = InitializeGame(gameType, gameSubType);
            // --- STARTING A GAME
            
            this.newTask = Task.Run(() => this.newGame.Play(player1, player2));            
        }

        public string Name { get => name; }        
        public List<Player> Players { get => players; set => players = value; }        
        public Task NewTask { get => newTask; set => newTask = value; }
        public Game NewGame { get => newGame; set => newGame = value; }
        public bool GameConcluded { get => gameConcluded; set => gameConcluded = value; }

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
