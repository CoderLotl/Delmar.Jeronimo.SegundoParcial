using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class GameMechanics
    {

        public static List<Player> players;
        public static List<Room> rooms;

        public static void InitializeLists(Action<string> action)
        {
            DataAccess.GetPlayers(action);    
            rooms = new List<Room>();
        }

        public static void AddTrucoRoom(string Text)
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
                        
            Room newRoom = new Room(roomName, player1, player2,Text, GameType.Cards, GameSubType.Truco);

            rooms.Add(newRoom);
        }        
    }
}
