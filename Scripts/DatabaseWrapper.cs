using System;
using System.IO;
using Godot;
using Microsoft.Data.Sqlite;

public partial class DatabaseWrapper
{
	private string pathToDB = @"Database/plants.db"; 

	public void InitializeDatabase(){

		//Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToDB;

        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // Read and execute SQL from .sql file
            string sqlFilePath = @"Database/plants.sql";
            if (File.Exists(sqlFilePath))
            {
                string sqlQuery = File.ReadAllText(sqlFilePath);
                using (var command = new SqliteCommand(sqlQuery, connection))
                {
                    command.ExecuteNonQuery();  // Execute the SQL commands in the file
					GD.Print("SQL file executed successfully.");

                }
            }
            else
            {
                GD.Print("SQL file not found.");
            }

            connection.Close();
        }
    }

    //TODO: Add getter method for retrieving all plants from the database as a List<Plant>
    //TODO: Add setter method for saving a game state to another database
    //TODO: Add a load method (getter) for loading a previously saved game state
}
