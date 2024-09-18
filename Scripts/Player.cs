using Godot;
using System;

public partial class Player : Node
{
    public String name;
    public int coins;
    public int character;

    public int AddCoins(int coinsToBeAdded)
    {
	    coins += coinsToBeAdded;
	    return coins;
    }

    public Player GetCurrentPlayer()
    {
	    return this;
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AddCoins(1);
	}
}
