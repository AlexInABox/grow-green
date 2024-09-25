using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;

public partial class SceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		List<Plant> listOfOwnedPlants = db.GetListOfOwnedPlants();
		foreach (var item in listOfOwnedPlants)
		{
			GD.Print(item.className);
		}
		GD.Print(listOfOwnedPlants);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
