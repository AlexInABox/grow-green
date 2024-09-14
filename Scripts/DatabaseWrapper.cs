using System;
using System.Collections.Generic;
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
    public List<Plant> GetListOfAllPlants(){

		//Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToDB;
        List<Plant> fillMeUp = new List<Plant>();
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM plants";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    string className = (string)reader["className"];
                    string name = (string)reader["name"];
                    int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                    int cost = Convert.ToInt32(reader["cost"]);
                    int sellValue = Convert.ToInt32(reader["sellValue"]);
                    int yield = Convert.ToInt32(reader["yield"]);

                    Plant plantOnThisRow = new Plant(className, name, waterEveryXDays, cost, sellValue, yield);
                    fillMeUp.Add(plantOnThisRow);
                }
            }
            connection.Close();
        }
        return fillMeUp;
    }
    //TODO: Add setter method for saving a game state to another database
    //TODO: Add a load method (getter) for loading a previously saved game state
}
