using System;
using System.Collections.Generic;

namespace Library
{
    public static class GameMechanics
    {

        private static List<Player> players;
        public static List<Room> rooms;

        public static List<Player> Players { get => players; set => players = value; }

        public delegate void Notify();

        public static event Notify NotifyUpdate;

        public static void InitializeLists(Action<string> warning)
        {
            DataAccess newConnection = new DataAccess();

            newConnection.GetPlayers(warning, "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True");

            rooms = new List<Room>();

            NotifyUpdate();
        }

        public static void AddTrucoRoom()
        {
            Random randomNumber = new Random();
            Player player1;
            Player player2;

            ValidateInitialParamsToPlay();

            player1 = players[randomNumber.Next(players.Count)];

            player2 = ObtainPlayer2(player1);            

            //player2 = players[1];

            string roomName = "Room #" + (rooms.Count + 1).ToString();

            Room newRoom = new Room(roomName, player1, player2, GameType.Cards, GameSubType.Truco);

            rooms.Add(newRoom);

        }

        private static Player ObtainPlayer2(Player player1)
        {
            Random randomNum = new();

            List<Player> filtered = players.FindAll((x) => x != player1);

            Player player2 = filtered[randomNum.Next(filtered.Count)];

            return player2;
        }

        private static void ValidateInitialParamsToPlay()
        {
            if (players == null)
            {
                throw new ArgumentNullException("There are no players in the list.");
            }
            if (players.Count < 2)
            {
                throw new ArgumentException("There are not enough players for this.");
            }
            if (rooms == null)
            {
                throw new ArgumentNullException("The rooms list has not been initialized correctly.");
            }
        }

        public static void AddTrucoRoom(Player player1, Player player2)
        {
            string roomName = "Room #" + (rooms.Count + 1).ToString();

            Room newRoom = new Room(roomName, player1, player2, GameType.Cards, GameSubType.Truco);

            rooms.Add(newRoom);
        }

        public static void RemoveTrucoRoom(int index, Action DrawTree)
        {

        }

        public static void RemoveTrucoRoom(Room room, Action DrawTree)
        {
            rooms.Remove(room);
            DrawTree();
        }
    }
}
