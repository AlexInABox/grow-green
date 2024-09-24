using System;
using System.Collections.Generic;
using Godot;
using Microsoft.Data.Sqlite;

public partial class DatabaseWrapper
{
	private readonly string pathToPlantsDB = ProjectSettings.GlobalizePath("user://plants.db"); 
    private readonly string pathToPlantsDBInitializer = "res://Database/rebuildPlantsDB.sql";
    private readonly string pathToSaveDB = ProjectSettings.GlobalizePath("user://save.db"); 
    private readonly string pathToSaveDBInitializer = "res://Database/rebuildSaveDB.sql";


    public DatabaseWrapper(){
        RebuildDatabase(pathToPlantsDB, pathToPlantsDBInitializer);

        if (!FileAccess.FileExists(pathToSaveDB)){ //Only recreate save.db if it doesnt exist
            GD.Print("I recreated the SAVE DB!!");
            RebuildDatabase(pathToSaveDB, pathToSaveDBInitializer);
        }
    }
    
    public void RebuildDatabase(string pathToDB, string pathToDBInitializer){
		string connectionString = "Data Source=" + pathToDB;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            GD.Print(pathToDBInitializer);
            var sqlQueryFile = FileAccess.Open(pathToDBInitializer, FileAccess.ModeFlags.Read);
            string sqlQuery = sqlQueryFile.GetAsText();
            sqlQueryFile.Close();
            
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                command.ExecuteNonQuery();  // Execute the SQL commands in the file
				GD.Print("Rebuilt database!");
            }
            connection.Close();
        }
    }

    public List<Plant> GetListOfAllPlants(){

		//Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToPlantsDB;
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

    public Plant ConstructPlantFromSave(string className, float growProgress, long growProgressTimestamp, float waterLevel, long waterLevelTimestamp, bool withered, bool rotten){
        //TODO: OMG DO STUFF HERE!!
    }

    public List<Plant> GetListOfOwnedPlants(){

		//Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToSaveDB;
        List<Plant> fillMeUp = new List<Plant>();
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM listOfOwnedPlants";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    string className = (string)reader["className"];
                    float growProgress = (float)reader["growProgress"];
                    long growProgressTimestamp = Convert.ToInt32(reader["growProgressTimestamp"]);
                    float waterLevel = (float)reader["waterLevel"];
                    long waterLevelTimestamp = Convert.ToInt32(reader["waterLevelTimestamp"]);
                    bool withered = (bool)reader["withered"];
                    bool rotten = (bool)reader["rotten"];

                    Plant plantOnThisRow = ConstructPlantFromSave(className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten);
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
