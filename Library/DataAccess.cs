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
        public void GetPlayers(Action<string> action, string connectionStringParam)
        {
            List<Player> playerList = new List<Player>();
            string connectionString = connectionStringParam;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.CommandText = "select Players.ID, Players.Name, Score.GamesPlayed, Score.GamesWon, Score.GamesLost, Score.GamesTied" +
                    " from Players inner join Score on Players.ID = Score.ID ";
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
                string[] names = {"Ana-BOT", "Rob-BOT", "Laura-BOT", "Jhon-BOT", "Danara-BOT", "Luke-BOT" };

                for (int i = 0; i < 6; i++)
                {
                    Player newPlayer = new Player(i + 1, names[i], 0, 0, 0, 0);
                    playerList.Add(newPlayer);
                }
                GameMechanics.Players = playerList;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        // --------------------------

        public void WritePlayers(Player player)
        {
            string connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand();

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "INSERT INTO Players VALUES (@name, @gamesPlayed, @gamesWon, @gamesLost, @gamesTied)";

                sqlCommand.Parameters.AddWithValue("@name", player.Name);
                sqlCommand.Parameters.AddWithValue("@gamesPlayed", player.GamesPlayed);
                sqlCommand.Parameters.AddWithValue("@gamesWon", player.GamesWon);
                sqlCommand.Parameters.AddWithValue("@gamesLost", player.GamesLost);
                sqlCommand.Parameters.AddWithValue("@gamesTied", player.GamesTied);

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

        // --------------------------

        public void UpdatePlayer(Player player)
        {
            string connectionString = "Server=ARIS-PC\\SERVIDORPARCIAL;Database=Parcial;Trusted_Connection=True;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
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

        // --------------------------

    }
}
