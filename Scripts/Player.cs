using Godot;
using System;
using System.Collections.Generic;

public partial class Player : Node
{
	public string username;
	public List<Plant> listOfOwnedPlants;
	public List<Pot> listOfOwnedPots;
	public int coins;
	public int characterId;
    
    public Player(){ //Development
        this.username = "Söder";
        this.listOfOwnedPlants = new List<Plant>();
        this.listOfOwnedPots = new List<Pot>();
        this.coins = 1000;
        this.characterId = 1;
    }

    public Player(int characterId){ //New User Created
        this.username = "";
        this.listOfOwnedPlants = new List<Plant>();
        this.listOfOwnedPots = new List<Pot>();
        this.coins = 1000;
        this.characterId = characterId;
    }

	public Player(string username, List<Plant> listOfOwnedPlants, List<Pot> listOfOwnedPots, int coins, int characterId){ //Load Player
		this.username = username;
		this.listOfOwnedPlants = listOfOwnedPlants;
		this.listOfOwnedPots = listOfOwnedPots;
		this.coins = coins;
		this.characterId = characterId;
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
