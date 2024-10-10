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
    private readonly string pathToPotsDB = ProjectSettings.GlobalizePath("user://pots.db"); 
    private readonly string pathToPotsDBInitializer = "res://Database/rebuildPotsDB.sql";



    public DatabaseWrapper(){
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        RebuildDatabase(pathToPlantsDB, pathToPlantsDBInitializer);
        RebuildDatabase(pathToPotsDB, pathToPotsDBInitializer);

        if (ShouldWeRebuildTheSave()){
            GD.Print("I recreated the SAVE DB!!");
            RebuildDatabase(pathToSaveDB, pathToSaveDBInitializer);
        }
    }

    private bool ShouldWeRebuildTheSave(){
        //Time to evaluate if the existent database broke

        string playerStatsTableName = "playerStats";
        string listOfOwnedPlantsTableName = "listOfOwnedPlants";
        string listOfOwnedPotsTableName = "listOfOwnedPots";

        bool databaseFileExists = FileAccess.FileExists(pathToSaveDB);
        bool playerStatsTableExists = false;
        bool listOfOwnedPlantsTableExists = false;
        bool listOfOwnedPotsTableExists = false;
        bool listOfOwnedPlantsTableIsComplete = false;
        if (databaseFileExists) {
            playerStatsTableExists = TableAlreadyExists(pathToSaveDB, playerStatsTableName);
            listOfOwnedPlantsTableExists = TableAlreadyExists(pathToSaveDB, listOfOwnedPlantsTableName);
            listOfOwnedPotsTableExists = TableAlreadyExists(pathToSaveDB, listOfOwnedPotsTableName);
            listOfOwnedPlantsTableIsComplete = TestListOfOwnedPlants();
        }

        return !(databaseFileExists && playerStatsTableExists && listOfOwnedPlantsTableExists && listOfOwnedPlantsTableIsComplete && listOfOwnedPotsTableExists);
    }

    private static bool TableAlreadyExists(string pathToDB, string tableName)
    {
        string connectionString = "Data Source=" + pathToDB;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';"; 

            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();
                bool tableAlreadyExists = reader.HasRows;
                reader.Close();
                return tableAlreadyExists;
            }
        }
    }

    private bool TestListOfOwnedPlants(){
        string connectionString = "Data Source=" + pathToSaveDB;
        try{
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var sqlQuery = "INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint) VALUES ('Agave', 0.55, 0, 0.55, 0, FALSE, FALSE, 'default', 1);";

                using (var command = new SqliteCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                }
            }
        } catch{
            GD.Print("OMG I ERRORED");
            return false;
        }

        GD.Print("The testing worked! Lets undo this...");
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var sqlQuery = "DELETE FROM listOfOwnedPlants WHERE ROWID = (SELECT Max(ROWID) FROM listOfOwnedPlants);";

            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();
            }
        }
        return true;
    }

    public bool IsThisTheFirstRun()
    {
        string connectionString = "Data Source=" + pathToSaveDB;

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var sqlQuery = $"SELECT * FROM playerStats;"; 

            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();
                bool playerStatsHasRows = reader.HasRows;
                reader.Close();
                return !playerStatsHasRows;
            }
        }
    }
    
    private void RebuildDatabase(string pathToDB, string pathToDBInitializer){
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
                    string difficulty = (string)reader["difficulty"];
                    int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                    int cost = Convert.ToInt32(reader["cost"]);
                    int sellValue = Convert.ToInt32(reader["sellValue"]);
                    int yield = Convert.ToInt32(reader["yield"]);

                    Plant plantOnThisRow = new Plant(className, name, difficulty, waterEveryXDays, cost, sellValue, yield);
                    fillMeUp.Add(plantOnThisRow);
                }
            }
            connection.Close();
        }
        return fillMeUp;
    }

    public Player GetPlayerObject(){
        string connectionString = "Data Source=" + pathToSaveDB;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM playerStats";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();

                reader.Read();

                string username = (string)reader["username"];
                int coins = Convert.ToInt32(reader["coins"]);
                int characterId = Convert.ToInt32(reader["characterId"]);

                connection.Close();

                List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();
                List<Pot> listOfOwnedPots = GetListOfOwnedPots();

                Player player = new Player(username, listOfOwnedPlants, listOfOwnedPots, coins, characterId);

                return player;
            }
            
        }
        
    }

    private List<Plant> GetListOfOwnedPlants(){

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
                    double growProgress = (double)reader["growProgress"];
                    long growProgressTimestamp = Convert.ToInt32(reader["growProgressTimestamp"]);
                    double waterLevel = (double)reader["waterLevel"];
                    long waterLevelTimestamp = Convert.ToInt32(reader["waterLevelTimestamp"]);
                    bool withered = Convert.ToInt32(reader["withered"]) != 0;
                    bool rotten = Convert.ToInt32(reader["rotten"]) != 0;
                    string potName = (string)reader["potName"];
                    int spawnPoint = Convert.ToInt32(reader["spawnPoint"]);

                    Plant plantOnThisRow = ConstructPlantFromSave(className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint);
                    fillMeUp.Add(plantOnThisRow);
                }
            }
            connection.Close();
        }
        return fillMeUp;
    }

    private Plant ConstructPlantFromSave(string className, double growProgress, long growProgressTimestamp, double waterLevel, long waterLevelTimestamp, bool withered, bool rotten, string pot, int spawnPoint){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToPlantsDB;
        Plant requestedPlant;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM plants WHERE plants.className == '" + className + "';";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();
                reader.Read();

                string name = (string)reader["name"];
                string difficulty = (string)reader["difficulty"];
                int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                int cost = Convert.ToInt32(reader["cost"]);
                int sellValue = Convert.ToInt32(reader["sellValue"]);
                int yield = Convert.ToInt32(reader["yield"]);

                requestedPlant = new Plant(className, name, difficulty, waterEveryXDays, cost, sellValue, yield, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, pot, spawnPoint);
            }
            connection.Close();
        }
        return requestedPlant;
    }

    public void UpdateSave(Player player) {
        ClearTableInDB("playerStats", pathToSaveDB);
        InsertPlayerStatsIntoTable(player);

        ClearTableInDB("listOfOwnedPlants", pathToSaveDB);
        foreach (Plant plant in player.listOfOwnedPlants) {
            InsertPlantIntoListOfOwnedPlantsTable(plant);
        }

        ClearTableInDB("listOfOwnedPots", pathToSaveDB);
        foreach (Pot pot in player.listOfOwnedPots) {
            InsertPotIntoListOfOwnedPotsTable(pot);
        }
    }

    private void ClearTableInDB(string table, string pathToDB) {
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToDB;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = $"DELETE FROM {table}";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                command.ExecuteReader();
            }
            connection.Close();
        }
    }

    private void InsertPlayerStatsIntoTable(Player player){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string username = player.username;
            int coins = player.coins;
            int characterId = player.characterId;

            string sqlQuery = $"INSERT INTO playerStats (username, coins, characterId) VALUES ('{username}', {coins}, {characterId});";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                command.ExecuteReader();
            }
            connection.Close();
        }
    }

    private void InsertPlantIntoListOfOwnedPlantsTable(Plant plant){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string className = plant.className;
            double growProgress = plant.growProgress;
            long growProgressTimestamp = plant.growProgressTimestamp;
            double waterLevel = plant.waterLevel;
            long waterLevelTimestamp = plant.waterLevelTimestamp;
            bool withered = plant.withered;
            bool rotten = plant.rotten;
            string potName = plant.pot;
            int spawnPoint = plant.spawnPoint;

            string sqlQuery = $"INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint) VALUES ('{className}', {growProgress}, {growProgressTimestamp}, {waterLevel}, {waterLevelTimestamp}, {withered}, {rotten}, '{potName}', {spawnPoint})";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                command.ExecuteReader();
            }
            connection.Close();
        }
    }

    private void InsertPotIntoListOfOwnedPotsTable(Pot pot){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string potName = pot.potName;

            string sqlQuery = $"INSERT INTO listOfOwnedPots (potName) VALUES ('{potName}');";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                command.ExecuteReader();
            }
            connection.Close();
        }
    }


    public List<Pot> GetListOfAllPots(){

		//Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToPotsDB;
        List<Pot> fillMeUp = new List<Pot>();
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM pots";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    string potName = (string)reader["potName"];
                    int cost = Convert.ToInt32(reader["cost"]);

                    Pot potOnThisRow = new Pot(potName, cost);
                    fillMeUp.Add(potOnThisRow);
                }
            }
            connection.Close();
        }
        return fillMeUp;
    }

    public Pot GetPotByName(string potName){
        List<Pot> listOfAllPots = GetListOfAllPots();

        foreach (Pot pot in listOfAllPots) {
            if (pot.potName == potName) return pot;
        }
        return new Pot();
    }

    public List<Pot> GetListOfOwnedPots(){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToSaveDB;
        List<Pot> fillMeUp = new List<Pot>();
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM listOfOwnedPots";
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();

                while(reader.Read()) {

                    string potName = (string)reader["potName"];

                    Pot potOnThisRow = ConstructPotFromSave(potName);
                    fillMeUp.Add(potOnThisRow);
                }
            }
            connection.Close();
        }
        return fillMeUp;
    }

    private Pot ConstructPotFromSave(string potName){
        //Create a local database and load the .sql file into it
		string connectionString = "Data Source=" + pathToPotsDB;
        Pot requestedPot;
        // Open the connection
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM pots WHERE pots.potName == '" + potName + "';";
            GD.Print(sqlQuery);
            using (var command = new SqliteCommand(sqlQuery, connection))
            {
                var reader = command.ExecuteReader();
                reader.Read();

                int cost = Convert.ToInt32(reader["cost"]);

                requestedPot = new Pot(potName, cost);
            }
            connection.Close();
        }
        return requestedPot;
    }

    public void CreateNewSave(){
        RebuildDatabase(pathToSaveDB, pathToSaveDBInitializer);
    }
}
