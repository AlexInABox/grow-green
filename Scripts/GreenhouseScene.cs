using Godot;
using System.Collections.Generic;

public partial class GreenhouseScene : Node
{
	// Called when the node enters the scene tree for the first time.
	DatabaseWrapper db = new DatabaseWrapper();
	PackedScene plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");
	SceneManager sceneManager;

	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		List<Plant> plantList = sceneManager.GetListOfOwnedPlants(); 
		PlaceAllPlants(plantList);
	}

	private void PlaceAllPlants(List<Plant> plantList) {

		foreach(Plant plant in plantList){
			int spawnPointNumber = plant.spawnPoint;
			if(spawnPointNumber > 26){

				Node spawnPointWrapper = GetNode("../SpawnPointWrapper");
				Node2D spawnPoint = spawnPointWrapper.GetNode<Node2D>($"SpawnPoint{spawnPointNumber}");
				var plantPrefabInstance = plantPrefab.Instantiate();

				spawnPoint.AddChild(plantPrefabInstance);

				var plantWrapper = spawnPoint.GetNode<Node2D>("plant_wrapper");
				plantWrapper.AddChild(plant);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}