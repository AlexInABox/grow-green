using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;

public partial class SceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	PackedScene plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		List<Plant> listOfOwnedPlants = db.GetListOfOwnedPlants();
		foreach (var item in listOfOwnedPlants)
		{
			GD.Print(item.className);
		}

		PlaceAllPlants(listOfOwnedPlants);
	}

	private void PlaceAllPlants(List<Plant> plantList) {
		int counter = 0;
		for (int x = 1; x <= 4; x++) {
			for (int y = 1; y <= 11; y++) {
				if (plantList.Count == counter){
					return;
				}

				var plantPrefabInstance = plantPrefab.Instantiate();
				AddChild(plantPrefabInstance);

				var plantWrapper = plantPrefabInstance.GetNode<Node2D>("../plant_wrapper");
				plantWrapper.Set("position", new Vector2(150*y, 200*x));
				plantWrapper.Set("name", "freaky" + counter);

				plantWrapper.AddChild(plantList[counter]);
				counter++;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
