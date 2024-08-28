using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Node
{
	List<Plant> offers;
    bool opened;

	Player player;

	//see you space cowboy!
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void SetPlayer(){
		player = GetTree().Root.GetNode<Player>("Player");
		GD.Print(player);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
