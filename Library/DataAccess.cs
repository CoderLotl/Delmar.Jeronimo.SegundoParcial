using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    public static class DataAccess
    {
        public static void GetPlayers(Action<string> action)
        {
            List<Player> playerList = new List<Player>();
            string connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = "SELECT * FROM Players";
                sqlCommand.Connection = connection;

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int gamesPlayed = reader.GetInt32(2);
                    int gamesWon = reader.GetInt32(3);
                    int gamesLost = reader.GetInt32(4);

                    Player newPlayer = new Player(id, name, gamesPlayed, gamesWon, gamesLost);
                    playerList.Add(newPlayer);
                }

                GameMechanics.players = playerList;
                action("Database loaded successfully.");
            }

            catch
            {
                string[] names = {"Ana-BOT", "Rob-BOT", "Laura-BOT", "Jhon-BOT", "Danara-BOT", "Luke-BOT" };

                for (int i = 0; i < 6; i++)
                {
                    Player newPlayer = new Player(i + 1, names[i], 0, 0, 0);
                    playerList.Add(newPlayer);
                }
                GameMechanics.players = playerList;
                action("Unable to connect with Database.\nLoading mock bots...");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public static void WritePlayers(Player player)
        {
            string connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "INSERT INTO Players VALUES (@name, @gamesPlayed, @gamesWon, @gamesLost)";

                sqlCommand.Parameters.AddWithValue("@name", player.Name);
                sqlCommand.Parameters.AddWithValue("@gamesPlayed", player.GamesPlayed);
                sqlCommand.Parameters.AddWithValue("@gamesWon", player.GamesWon);
                sqlCommand.Parameters.AddWithValue("@gamesLost", player.GamesLost);

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

        public static void UpdatePlayer(Player player)
        {
            string connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "UPDATE Players SET gamesPlayed = @gamesPlayed, gamesWon = @gamesWon, gamesLost = @gamesLost WHERE ID = @id";

                sqlCommand.Parameters.AddWithValue("@gamesPlayed", player.GamesPlayed);
                sqlCommand.Parameters.AddWithValue("@gamesWon", player.GamesWon);
                sqlCommand.Parameters.AddWithValue("@gamesLost", player.GamesLost);
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

    }
}
