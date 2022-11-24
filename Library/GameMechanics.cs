using System;
using System.Collections.Generic;

namespace Library
{
    public static class GameMechanics
    {

        private static List<Player> players;
        private static List<Room> rooms;
        private static int id;
        private static string connectionString;

        public static List<Player> Players { get => players; set => players = value; }
        public static List<Room> Rooms { get => rooms; set => rooms = value; }
        public static int ID { get => id; set => id = value; }
        public static string ConnectionString { get => connectionString; }

        public delegate void Notify();

        public static event Notify NotifyUpdate;

        static GameMechanics()
        {
            ID = 0;
            rooms = new List<Room>();
            players = new List<Player>();
            connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
        }

        public static void InitializeLists(Action<string> warning)
        {
            DataAccess newConnection = new DataAccess(ConnectionString);
            
            if(newConnection.TestConnection() == true)
            {
                newConnection.GetPlayers(warning);
            }
            else
            {
                warning("Unable to connect with Database.\nLoading mock bots...");
                newConnection.LoadMockBots(players);
            }

            NotifyUpdate();
        }

        public static void AddTrucoRoom(Action<string> action)
        {
            Random randomNumber = new Random();
            Player player1;
            Player player2;

            ValidateInitialParamsToPlay();

            player1 = players[randomNumber.Next(players.Count)];

            player2 = ObtainPlayer2(player1);            
            
            string roomName = "Room #" + (GameMechanics.ID + 1).ToString();

            Room newRoom = new Room(roomName, player1, player2, GameType.Cards, GameSubType.Truco, action);

            rooms.Add(newRoom);

            GameMechanics.ID++;
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

        public static void RemoveTrucoRoom(Room room, Action DrawTree)
        {
            rooms.Remove(room);
            DrawTree();
        }
    }
}
