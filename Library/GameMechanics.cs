using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class GameMechanics
    {

        public static List<Player> players;
        public static List<Room> rooms;
        public static event EventHandler Notify;

        public static void InitializeLists(Action<string> warning)
        {
            DataAccess newConnection = new DataAccess();

            newConnection.GetPlayers(warning);

            rooms = new List<Room>();            
        }

        public static void AddTrucoRoom()
        {
            Random randomNumber = new Random();
            Player player1;
            Player player2;

            player1 = players[randomNumber.Next(players.Count)];

            do
            {
                player2 = players[randomNumber.Next(players.Count)];
            } while (player1 == player2);

            string roomName = "Room #" + (rooms.Count + 1).ToString();
                        
            Room newRoom = new Room(roomName, player1, player2, GameType.Cards, GameSubType.Truco);

            rooms.Add(newRoom);
        }        
    }
}
