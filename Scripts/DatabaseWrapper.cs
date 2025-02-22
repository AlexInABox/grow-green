using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Godot;
using Microsoft.Data.Sqlite;

public class DatabaseWrapper
{
    private readonly string pathToPlantsDB = ProjectSettings.GlobalizePath("user://plants.db");
    private readonly string pathToPlantsDBInitializer = "res://Database/rebuildPlantsDB.sql";
    private readonly string pathToPotsDB = ProjectSettings.GlobalizePath("user://pots.db");
    private readonly string pathToPotsDBInitializer = "res://Database/rebuildPotsDB.sql";
    private readonly string pathToSaveDB = ProjectSettings.GlobalizePath("user://save.db");
    private readonly string pathToSaveDBInitializer = "res://Database/rebuildSaveDB.sql";

    public bool savingLock;


    public DatabaseWrapper()
    {
        CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";
        Thread.CurrentThread.CurrentCulture = customCulture;

        RebuildDatabase(pathToPlantsDB, pathToPlantsDBInitializer);
        RebuildDatabase(pathToPotsDB, pathToPotsDBInitializer);

        if (ShouldWeRebuildTheSave())
            //GD.Print("I recreated the SAVE DB!!");
            RebuildDatabase(pathToSaveDB, pathToSaveDBInitializer);
    }

    private bool ShouldWeRebuildTheSave()
    {
        //Time to evaluate if the existent database broke

        string playerStatsTableName = "playerStats";
        string listOfOwnedPlantsTableName = "listOfOwnedPlants";
        string listOfOwnedPotsTableName = "listOfOwnedPots";

        bool databaseFileExists = FileAccess.FileExists(pathToSaveDB);
        bool playerStatsTableExists = false;
        bool listOfOwnedPlantsTableExists = false;
        bool listOfOwnedPotsTableExists = false;
        bool listOfOwnedPlantsTableIsComplete = false;
        bool playerStatsTableIsComplete = false;
        if (databaseFileExists)
        {
            playerStatsTableExists = TableAlreadyExists(pathToSaveDB, playerStatsTableName);
            listOfOwnedPlantsTableExists = TableAlreadyExists(pathToSaveDB, listOfOwnedPlantsTableName);
            listOfOwnedPotsTableExists = TableAlreadyExists(pathToSaveDB, listOfOwnedPotsTableName);
            listOfOwnedPlantsTableIsComplete = TestListOfOwnedPlants();
            playerStatsTableIsComplete = TestPlayerStatsTable();
        }

        return !(databaseFileExists && playerStatsTableExists && listOfOwnedPlantsTableExists &&
                 listOfOwnedPlantsTableIsComplete && listOfOwnedPotsTableExists && playerStatsTableIsComplete);
    }

    private static bool TableAlreadyExists(string pathToDB, string tableName)
    {
        string connectionString = "Data Source=" + pathToDB;

        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();

            string sqlQuery = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";

            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                bool tableAlreadyExists = reader.HasRows;
                reader.Close();
                return tableAlreadyExists;
            }
        }
    }

    private bool TestListOfOwnedPlants()
    {
        string connectionString = "Data Source=" + pathToSaveDB;
        try
        {
            using (SqliteConnection connection = new(connectionString))
            {
                connection.Open();

                string sqlQuery =
                    "INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint) VALUES ('Agave', 0.55, 0, 0.55, 0, FALSE, FALSE, 'default', 1);";

                using (SqliteCommand command = new(sqlQuery, connection))
                {
                    SqliteDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
            }
        }
        catch
        {
            //GD.Print("OMG I ERRORED");
            return false;
        }

        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            string sqlQuery = "DELETE FROM listOfOwnedPlants WHERE ROWID = (SELECT Max(ROWID) FROM listOfOwnedPlants);";

            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        return true;
    }

    private bool TestPlayerStatsTable()
    {
        string connectionString = "Data Source=" + pathToSaveDB;
        try
        {
            using (SqliteConnection connection = new(connectionString))
            {
                connection.Open();

                string sqlQuery =
                    "INSERT INTO playerStats (username, coins, characterId, greenhouseUnlocked, flappyPlantHighscore) VALUES ('ThisUserDoesNotExist', 20, 2, FALSE, 0);";

                using (SqliteCommand command = new(sqlQuery, connection))
                {
                    SqliteDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
            }
        }
        catch
        {
            return false;
        }

        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            string sqlQuery = "DELETE FROM playerStats WHERE ROWID = (SELECT Max(ROWID) FROM playerStats);";

            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                reader.Close();
            }
        }

        return true;
    }

    public bool IsThisTheFirstRun()
    {
        string connectionString = "Data Source=" + pathToSaveDB;

        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();

            string sqlQuery = "SELECT * FROM playerStats;";

            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                bool playerStatsHasRows = reader.HasRows;
                reader.Close();
                return !playerStatsHasRows;
            }
        }
    }

    private void RebuildDatabase(string pathToDB, string pathToDBInitializer)
    {
        string connectionString = "Data Source=" + pathToDB;

        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();

            FileAccess sqlQueryFile = FileAccess.Open(pathToDBInitializer, FileAccess.ModeFlags.Read);
            string sqlQuery = sqlQueryFile.GetAsText();
            sqlQueryFile.Close();

            using (SqliteCommand command = new(sqlQuery, connection))
            {
                command.ExecuteNonQuery(); // Execute the SQL commands in the file
            }

            connection.Close();
        }
    }

    public List<Plant> GetListOfAllPlants()
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToPlantsDB;
        List<Plant> fillMeUp = new();
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM plants";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string className = (string)reader["className"];
                    string name = (string)reader["name"];
                    string difficulty = (string)reader["difficulty"];
                    int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                    int cost = Convert.ToInt32(reader["cost"]);
                    int sellValue = Convert.ToInt32(reader["sellValue"]);
                    int yield = Convert.ToInt32(reader["yield"]);

                    Plant plantOnThisRow = new(className, name, difficulty, waterEveryXDays, cost, sellValue, yield);
                    fillMeUp.Add(plantOnThisRow);
                }

                reader.Close();
            }

            connection.Close();
        }

        return fillMeUp;
    }

    public Plant GetPlantByClassName(string className)
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToPlantsDB;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM plants WHERE plants.className == '" + className + "';";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string name = (string)reader["name"];
                    string difficulty = (string)reader["difficulty"];
                    int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                    int cost = Convert.ToInt32(reader["cost"]);
                    int sellValue = Convert.ToInt32(reader["sellValue"]);
                    int yield = Convert.ToInt32(reader["yield"]);

                    Plant plantOnThisRow = new(className, name, difficulty, waterEveryXDays, cost, sellValue, yield);
                    connection.Close();
                    return plantOnThisRow;
                }

                reader.Close();
            }
        }

        return new Plant(0);
    }

    public Player GetPlayerObject()
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);

        string connectionString = "Data Source=" + pathToSaveDB;
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM playerStats";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                reader.Read();

                string username = (string)reader["username"];
                int coins = Convert.ToInt32(reader["coins"]);
                int characterId = Convert.ToInt32(reader["characterId"]);
                bool greenhouseUnlocked = Convert.ToInt32(reader["greenhouseUnlocked"]) != 0;
                int flappyPlantHighscore = Convert.ToInt32(reader["flappyPlantHighscore"]);

                reader.Close();
                connection.Close();


                List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();
                List<Pot> listOfOwnedPots = GetListOfOwnedPots();

                Player player = new(username, listOfOwnedPlants, listOfOwnedPots, coins, characterId,
                    greenhouseUnlocked, flappyPlantHighscore);

                return player;
            }
        }
    }

    private List<Plant> GetListOfOwnedPlants()
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToSaveDB;
        List<Plant> fillMeUp = new();
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM listOfOwnedPlants";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string className = (string)reader["className"];
                    double growProgress = (double)reader["growProgress"];
                    long growProgressTimestamp = Convert.ToInt32(reader["growProgressTimestamp"]);
                    double waterLevel = (double)reader["waterLevel"];
                    long waterLevelTimestamp = Convert.ToInt32(reader["waterLevelTimestamp"]);
                    bool withered = Convert.ToInt32(reader["withered"]) != 0;
                    bool rotten = Convert.ToInt32(reader["rotten"]) != 0;
                    string potName = (string)reader["potName"];
                    int spawnPoint = Convert.ToInt32(reader["spawnPoint"]);

                    Plant plantOnThisRow = ConstructPlantFromSave(className, growProgress, growProgressTimestamp,
                        waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint);
                    fillMeUp.Add(plantOnThisRow);
                }

                reader.Close();
            }

            connection.Close();
        }

        return fillMeUp;
    }

    private Plant ConstructPlantFromSave(string className, double growProgress, long growProgressTimestamp,
        double waterLevel, long waterLevelTimestamp, bool withered, bool rotten, string pot, int spawnPoint)
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToPlantsDB;
        Plant requestedPlant;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM plants WHERE plants.className == '" + className + "';";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                reader.Read();

                string name = (string)reader["name"];
                string difficulty = (string)reader["difficulty"];
                int waterEveryXDays = Convert.ToInt32(reader["waterEveryXDays"]);
                int cost = Convert.ToInt32(reader["cost"]);
                int sellValue = Convert.ToInt32(reader["sellValue"]);
                int yield = Convert.ToInt32(reader["yield"]);

                reader.Close();

                requestedPlant = new Plant(className, name, difficulty, waterEveryXDays, cost, sellValue, yield,
                    growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, pot,
                    spawnPoint);
            }

            connection.Close();
        }

        return requestedPlant;
    }

    public void UpdateSave(Player player)
    {
        savingLock = true;


        ClearTableInDB("playerStats", pathToSaveDB);

        InsertPlayerStatsIntoTable(player);

        ClearTableInDB("listOfOwnedPlants", pathToSaveDB);
        foreach (Plant plant in player.listOfOwnedPlants) InsertPlantIntoListOfOwnedPlantsTable(plant);

        ClearTableInDB("listOfOwnedPots", pathToSaveDB);
        foreach (Pot pot in player.listOfOwnedPots) InsertPotIntoListOfOwnedPotsTable(pot);

        savingLock = false;
    }

    private void ClearTableInDB(string table, string pathToDB)
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToDB;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = $"DELETE FROM {table}";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                command.ExecuteReader();
            }

            connection.Close();
        }
    }

    private void InsertPlayerStatsIntoTable(Player player)
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string username = player.username;
            int coins = player.coins;
            int characterId = player.characterId;
            bool greenhouseUnlocked = player.greenhouseUnlocked;
            int flappyPlantHighscore = player.flappyPlantHighscore;

            string sqlQuery =
                $"INSERT INTO playerStats (username, coins, characterId, greenhouseUnlocked, flappyPlantHighscore) VALUES ('{username}', {coins}, {characterId}, {greenhouseUnlocked}, {flappyPlantHighscore});";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                command.ExecuteReader();
            }

            connection.Close();
        }
    }

    private void InsertPlantIntoListOfOwnedPlantsTable(Plant plant)
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
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

            string sqlQuery =
                $"INSERT INTO listOfOwnedPlants (className, growProgress, growProgressTimestamp, waterLevel, waterLevelTimestamp, withered, rotten, potName, spawnPoint) VALUES ('{className}', {growProgress}, {growProgressTimestamp}, {waterLevel}, {waterLevelTimestamp}, {withered}, {rotten}, '{potName}', {spawnPoint})";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                command.ExecuteReader();
            }

            connection.Close();
        }
    }

    private void InsertPotIntoListOfOwnedPotsTable(Pot pot)
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToSaveDB;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string potName = pot.potName;

            string sqlQuery = $"INSERT INTO listOfOwnedPots (potName) VALUES ('{potName}');";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                command.ExecuteReader();
            }

            connection.Close();
        }
    }


    public List<Pot> GetListOfAllPots()
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToPotsDB;
        List<Pot> fillMeUp = new();
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM pots";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string potName = (string)reader["potName"];
                    int cost = Convert.ToInt32(reader["cost"]);

                    Pot potOnThisRow = new(potName, cost);
                    fillMeUp.Add(potOnThisRow);
                }

                reader.Close();
            }

            connection.Close();
        }

        return fillMeUp;
    }

    /// <summary>
    ///     Returns the requested <see cref="Pot" /> if it exists in the <see cref="DatabaseWrapper" />.
    ///     If not it will return the default <see cref="Pot" />
    /// </summary>
    /// <param name="potName">The name of the requested <see cref="Pot" /></param>
    public Pot GetPotByName(string potName)
    {
        List<Pot> listOfAllPots = GetListOfAllPots();

        foreach (Pot pot in listOfAllPots)
            if (pot.potName == potName)
                return pot;
        return new Pot();
    }

    public List<Pot> GetListOfOwnedPots()
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToSaveDB;
        List<Pot> fillMeUp = new();
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM listOfOwnedPots";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string potName = (string)reader["potName"];

                    Pot potOnThisRow = ConstructPotFromSave(potName);
                    fillMeUp.Add(potOnThisRow);
                }

                reader.Close();
            }

            connection.Close();
        }

        return fillMeUp;
    }

    private Pot ConstructPotFromSave(string potName)
    {
        //Create a local database and load the .sql file into it
        string connectionString = "Data Source=" + pathToPotsDB;
        Pot requestedPot;
        // Open the connection
        using (SqliteConnection connection = new(connectionString))
        {
            connection.Open();
            //Praise the LORD
            string sqlQuery = "SELECT * FROM pots WHERE pots.potName == '" + potName + "';";
            using (SqliteCommand command = new(sqlQuery, connection))
            {
                SqliteDataReader reader = command.ExecuteReader();
                reader.Read();

                int cost = Convert.ToInt32(reader["cost"]);

                reader.Close();
                requestedPot = new Pot(potName, cost);
            }

            connection.Close();
        }

        return requestedPot;
    }

    public void CreateNewSave()
    {
        while (IsSaveDatabaseLocked())
            // Wait for 1 second (1000 milliseconds)
            Thread.Sleep(1000);


        RebuildDatabase(pathToSaveDB, pathToSaveDBInitializer);
    }

    private bool IsSaveDatabaseLocked()
    {
        bool locked = true;
        SqliteConnection connection = new($"Data Source={pathToSaveDB}");
        connection.Open();

        try
        {
            SqliteCommand beginCommand = connection.CreateCommand();
            beginCommand.CommandText = "BEGIN EXCLUSIVE"; // tries to acquire the lock
            // CommandTimeout is set to 0 to get error immediately if DB is locked 
            // otherwise it will wait for 30 sec by default
            beginCommand.CommandTimeout = 0;
            beginCommand.ExecuteNonQuery();

            SqliteCommand commitCommand = connection.CreateCommand();
            commitCommand.CommandText = "COMMIT"; // releases the lock immediately
            commitCommand.ExecuteNonQuery();
            locked = false;
        }
        catch (SqliteException)
        {
            // database is locked error
        }
        finally
        {
            connection.Close();
        }

        return locked;
    }
}