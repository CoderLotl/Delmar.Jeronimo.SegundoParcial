using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    public class DataAccess
    {
        string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// THIS METHOD TRIES TO CONNECT TO THE DATABASE AND RETRIEVES A LIST OF PLAYERS, WHICH IS LATER SET
        /// IN THE STATIC LIST OF PLAYER.
        /// MAY THE CONNECTION BE SUCCESSFUL BUT ANY OTHER EXCEPTION OCCUR, THE METHOD WILL LOAD A LIST OF MOCK
        /// PLAYERS TO THE GAME AND EXECUTE THE WARNING METHOD PASSED BY THE DELEGATE.
        /// </summary>
        /// <param name="action"></param>
        public void GetPlayers(Action<string> action)
        {
            List<Player> playerList = new List<Player>();
            
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {                
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = "select Players.PlayerID, Players.Name, Score.GamesPlayed, Score.GamesWon, Score.GamesLost, Score.GamesTied" +
                    " from Players inner join Score on Players.PlayerID = Score.ID ";
                sqlCommand.Connection = connection;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int gamesPlayed = reader.GetInt32(2);
                    int gamesWon = reader.GetInt32(3);
                    int gamesLost = reader.GetInt32(4);
                    int gamesTied = reader.GetInt32(5);

                    Player newPlayer = new Player(id, name, gamesPlayed, gamesWon, gamesLost, gamesTied);
                    playerList.Add(newPlayer);
                }

                GameMechanics.Players = playerList;
                action("Database loaded successfully.");                
            }

            catch (Exception exception)
            {
                action("Unable to connect with Database.\nLoading mock bots...");
                LoadMockBots(playerList);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// THIS METHOD LOADS A LIST OF MOCK PLAYERS.
        /// </summary>
        /// <param name="playerList"></param>
        public void LoadMockBots(List<Player> playerList)
        {
            
            string[] names = { "Ana-BOT", "Rob-BOT", "Laura-BOT", "Jhon-BOT", "Danara-BOT", "Luke-BOT" };

            for (int i = 0; i < 6; i++)
            {
                Player newPlayer = new Player(i + 1, names[i], 0, 0, 0, 0);
                playerList.Add(newPlayer);
            }
            GameMechanics.Players = playerList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public void InsertPlayer(Player player, Action<string> action)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "INSERT INTO Players VALUES (@name)";

                sqlCommand.Parameters.AddWithValue("@name", player.Name);
                sqlCommand.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                sqlCommand.Parameters.Clear();

                connection.Open();

                int id = 0;
                sqlCommand.CommandText = "SELECT TOP 1 * FROM Players ORDER BY PlayerID DESC";

                SqlDataReader reader = sqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                player.Id = id;

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                sqlCommand.Parameters.Clear();

                connection.Open();

                sqlCommand.CommandText = "INSERT INTO Score VALUES (@id, @gamesPlayed, @gamesWon, @gamesLost, @gamesTied)";

                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@gamesPlayed", 0);
                sqlCommand.Parameters.AddWithValue("@gamesWon", 0);
                sqlCommand.Parameters.AddWithValue("@gamesLost", 0);
                sqlCommand.Parameters.AddWithValue("@gamesTied", 0);

                sqlCommand.ExecuteNonQuery();


                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch
            {
                action("Some unexpected exception happened.\nThe player won't be saved to the Database.");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public void UpdatePlayer(Player player)
        {            
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "UPDATE Score SET gamesPlayed = @gamesPlayed, gamesWon = @gamesWon, gamesLost = @gamesLost, gamesTied = @gamesTied WHERE ID = @id";

                sqlCommand.Parameters.AddWithValue("@gamesPlayed", player.GamesPlayed);
                sqlCommand.Parameters.AddWithValue("@gamesWon", player.GamesWon);
                sqlCommand.Parameters.AddWithValue("@gamesLost", player.GamesLost);
                sqlCommand.Parameters.AddWithValue("@gamesTied", player.GamesTied);
                sqlCommand.Parameters.AddWithValue("@id", player.Id);

                sqlCommand.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch
            {

            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public void DeletePlayer(Player player, Action<string> action)
        {
            
            SqlConnection connection = new SqlConnection(this.connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DELETE FROM Players WHERE PlayerID = @id";
                sqlCommand.Parameters.AddWithValue("@id", player.Id);

                sqlCommand.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

                sqlCommand.Parameters.Clear();

                connection.Open();
                
                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "DELETE FROM Score WHERE ID = @id";
                sqlCommand.Parameters.AddWithValue("@id", player.Id);

                sqlCommand.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            catch
            {
                action("Some unexpected exception happened.\nThe player won't be deleted from the Database.");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void WriteMatchDown(Room room, DateTime dateTime)
        {
            
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "INSERT INTO Matches VALUES (@game, @gamelog, @date)";
                
                sqlCommand.Parameters.AddWithValue("@game", room.Name + " | Players: " + room.Players[0].Name + " - " + room.Players[1].Name + " | Date: " + dateTime);
                sqlCommand.Parameters.AddWithValue("@gamelog", room.NewGame.Log);
                sqlCommand.Parameters.AddWithValue("@date", dateTime);
                sqlCommand.ExecuteNonQuery();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            catch
            {

            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void LoadMatchesHistory(List<HistoryRoom> listOfRooms)
        {
            SqlConnection connection = new SqlConnection(this.connectionString);

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = "select Matches.Game, Matches.GameLog, Matches.Date from Matches";
                sqlCommand.Connection = connection;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    string roomName = reader.GetString(0);
                    string gameLog = reader.GetString(1);
                    DateTime date = reader.GetDateTime(2);

                    HistoryRoom newHistoryRoom = new HistoryRoom(roomName, gameLog, date);
                    listOfRooms.Add(newHistoryRoom);
                }

            }
            catch
            {

            }
        }

        public bool TestConnection()
        {
            bool connectionOk;

            try
            {
                SqlConnection connection = new SqlConnection(this.connectionString);

                connectionOk = true;
                return connectionOk;
            }
            catch
            {
                connectionOk = false;
                return connectionOk;
            }            
        }
    }
}
