using System.Collections.Generic;
using System.Threading;
using Godot;

public partial class SceneManager : Node
{
    private readonly DatabaseWrapper db = new();
    private bool hasUnlockedGreenhouse;

    public bool IAMREADY;
    private Player playerObject;

    public void SetHasUnlockedGreenhouse(bool hasUnlockedGreenhouse)
    {
        playerObject.greenhouseUnlocked = hasUnlockedGreenhouse;
    }

    public bool GetHasUnlockedGreenhouse()
    {
        return playerObject.greenhouseUnlocked;
    }

    public override void _Ready()
    {
        playerObject = db.GetPlayerObject();
        IAMREADY = true;
    }

    public int GetCoinCount()
    {
        return playerObject.coins;
    }

    public void SetCoinCount(int coinCount)
    {
        playerObject.coins = coinCount;
    }

    public int GetCharacterId()
    {
        return playerObject.characterId;
    }
    //I decide not to provide a function that changes the characterId. I see no applicable use case.

    public List<Plant> GetListOfOwnedPlants()
    {
        return playerObject.listOfOwnedPlants;
    }
    //The list will be changed even after it has been given away. Remote changes to this list will also reflect in the playerObjects version. They are interlinked!

    public List<Pot> GetListOfOwnedPots()
    {
        return playerObject.listOfOwnedPots;
    }

    public bool IsSaveLocked()
    {
        return db.savingLock;
    }

    public Plant GetPlantByClassName(string className)
    {
        return db.GetPlantByClassName(className);
    }

    /// <inheritdoc cref="DatabaseWrapper.GetPotByName"/>
    public Pot GetPotByName(string potName)
    {
        return db.GetPotByName(potName);
    }

    public List<Plant> GetListOfAllPlants()
    {
        return db.GetListOfAllPlants();
    }

    public void UpdateSaveBlocking()
    {
        db.UpdateSave(playerObject);
    }

    public override void _Process(double delta)
    {
        if (Engine.GetFramesDrawn() % 1000 == 0)
        {
            GD.Print("Saving...");
            //IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
            Thread thread = new(() => db.UpdateSave(playerObject));
            thread.Start();
        }

        //this is cool! save the everything every 60 frames and even then do it in a different thread! so cool!
    }

    public void SpawnGreenhouseUnlockPopup()
    {
        //ColorRect blurLayer = GetNode<ColorRect>("../BlurLayer");
        //blurLayer.Hide();

        PackedScene unlockPopup = GD.Load<PackedScene>("res://Prefabs/greenhouseUnlock_popup.tscn");
        Node unlockPopupInstance = unlockPopup.Instantiate();
        GetParent().AddChild(unlockPopupInstance);
    }

    public void AddNewPlantToListOfOwnedPlants(Plant plantToAdd)
    {
        //automtically generate the spawnPoint
        List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();

        List<int> listOfOccupiedSpawnPoints = new();

        foreach (Plant plant in listOfOwnedPlants) listOfOccupiedSpawnPoints.Add(plant.spawnPoint);
        listOfOccupiedSpawnPoints.Sort();
        foreach (int spawnPoint in listOfOccupiedSpawnPoints) GD.Print(spawnPoint);
        for (int i = 1;; i++)
        {
            if (listOfOccupiedSpawnPoints.Count < i)
            {
                plantToAdd.spawnPoint = i;
                break;
            }

            if (listOfOccupiedSpawnPoints[i - 1] == i) continue;
            plantToAdd.spawnPoint = i;
            break;
        }

        listOfOwnedPlants.Add(plantToAdd);
    }


    /// <summary>
    ///     Changes the spawnPoint of the <see cref="Plant" /> to a spawnPoint in the Greenhouse. Returns 1 for success and 0
    ///     if it failed.
    /// </summary>
    /// <param name="plantToSwitch">The plant to change the spawnpoint of.</param>
    public int TrySwitchPlantToGreenhouseOnly(Plant plantToSwitch)
    {
        List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();

        List<int> listOfOccupiedSpawnPointsInGreenhouse = new();

        foreach (Plant plant in listOfOwnedPlants)
        {
            if (plant.spawnPoint < 27) continue;
            listOfOccupiedSpawnPointsInGreenhouse.Add(plant.spawnPoint);
        }

        if (listOfOccupiedSpawnPointsInGreenhouse.Count == 0)
        {
            plantToSwitch.spawnPoint = 27;
            return 1;
        }

        listOfOccupiedSpawnPointsInGreenhouse.Sort();

        int spawnPointCounter = 26;
        for (int i = 0; i < 26; i++)
        {
            spawnPointCounter++;

            if (listOfOccupiedSpawnPointsInGreenhouse.Count == i)
            {
                plantToSwitch.spawnPoint = spawnPointCounter;
                return 1;
            }


            if (listOfOccupiedSpawnPointsInGreenhouse[i] == spawnPointCounter) continue;
            plantToSwitch.spawnPoint = spawnPointCounter;
            return 1;
        }

        return 0; //Greenhouse is full
    }

    /// <summary>
    ///     Changes the spawnPoint of the <see cref="Plant" /> to a spawnPoint in the MainScene. Returns 1 for success and 0
    ///     if it failed.
    /// </summary>
    /// <param name="plantToSwitch">The plant to change the spawnpoint of.</param>
    public int TrySwitchPlantToMainSceneOnly(Plant plantToSwitch)
    {
        List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();

        List<int> listOfOccupiedSpawnPointsInGreenhouse = new();

        foreach (Plant plant in listOfOwnedPlants)
        {
            if (plant.spawnPoint > 26) continue;
            listOfOccupiedSpawnPointsInGreenhouse.Add(plant.spawnPoint);
        }

        if (listOfOccupiedSpawnPointsInGreenhouse.Count == 0)
        {
            plantToSwitch.spawnPoint = 1;
            return 1;
        }

        listOfOccupiedSpawnPointsInGreenhouse.Sort();

        int spawnPointCounter = 0;
        for (int i = 0; i < 26; i++)
        {
            spawnPointCounter++;

            if (listOfOccupiedSpawnPointsInGreenhouse.Count == i)
            {
                plantToSwitch.spawnPoint = spawnPointCounter;
                return 1;
            }


            if (listOfOccupiedSpawnPointsInGreenhouse[i] == spawnPointCounter) continue;
            plantToSwitch.spawnPoint = spawnPointCounter;
            return 1;
        }

        return 0; //MainScene is full
    }

    /// <summary>
    ///     Returns a list of every found <see cref="PlantWrapper"/> currently existing in the scene.
    /// </summary>
    /// <returns><see cref="List{T}"/></returns>
    public List<PlantWrapper> GetListOfAllPlantWrapperInScene()
    {
        List<PlantWrapper> plantWrappers = new List<PlantWrapper>();
        Node root = GetTree().GetCurrentScene();
        FindPlantWrappers(root);

        return plantWrappers;
        
        void FindPlantWrappers(Node node)
        {
            if (node is PlantWrapper plantWrapper)
            {
                plantWrappers.Add(plantWrapper);
            }

            foreach (Node child in node.GetChildren())
            {
                FindPlantWrappers(child);
            }
        }
    }

}