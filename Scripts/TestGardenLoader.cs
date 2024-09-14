using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

public partial class TestGardenLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	DatabaseWrapper db = new DatabaseWrapper();

	public override void _Ready()
	{
		List<Plant> plantList = db.GetListOfAllPlants();
		//PlaceRandomGrid();
		PlaceSpecificPlant(plantList[1]);
		//GD.Print(plantList[0]);
	}

	private void PlaceRandomGrid() {
		var plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");
		for (int x = 1; x <= 4; x++) {
			for (int y = 1; y <= 11; y++) {
				var plantPrefabInstance = plantPrefab.Instantiate();
				AddChild(plantPrefabInstance);

				var plantWrapper = plantPrefabInstance.GetNode<Node2D>("../plant_wrapper");
				plantWrapper.Set("position", new Vector2(150*y, 200*x));
				plantWrapper.Set("name", "freaky" + y + "_" + x);
			}
		}
	}

	private void PlaceSpecificPlant(Plant plant) {
		var plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");
		var plantPrefabInstance = plantPrefab.Instantiate();
		AddChild(plantPrefabInstance);

		var plantWrapper = plantPrefabInstance.GetNode<Node2D>("../plant_wrapper");
		plantWrapper.Set("position", new Vector2(150, 200));
		plantWrapper.Set("name", "freaky");

		plantWrapper.AddChild(plant);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
