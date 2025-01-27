using System.Collections.Generic;
using Godot;

public partial class Player : Node
{
    public int characterId;
    public int coins;
    public int flappyPlantHighscore;
    public bool greenhouseUnlocked;
    public List<Plant> listOfOwnedPlants;
    public List<Pot> listOfOwnedPots;
    public string username;

    public Player()
    {
        //Development
        username = "Söder";
        listOfOwnedPlants = new List<Plant>();
        listOfOwnedPots = new List<Pot>();
        coins = 25;
        characterId = 1;
        greenhouseUnlocked = true;
        flappyPlantHighscore = 0;
    }

    public Player(int characterId)
    {
        //New User Created
        username = "";
        listOfOwnedPlants = new List<Plant>();
        listOfOwnedPots = new List<Pot>();
        coins = 25;
        this.characterId = characterId;
        greenhouseUnlocked = false;
        flappyPlantHighscore = 0;
    }

    public Player(string username, List<Plant> listOfOwnedPlants, List<Pot> listOfOwnedPots, int coins, int characterId,
        bool greenhouseUnlocked, int flappyPlantHighscore)
    {
        //Load Player
        this.username = username;
        this.listOfOwnedPlants = listOfOwnedPlants;
        this.listOfOwnedPots = listOfOwnedPots;
        this.coins = coins;
        this.characterId = characterId;
        this.greenhouseUnlocked = greenhouseUnlocked;
        this.flappyPlantHighscore = flappyPlantHighscore;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}