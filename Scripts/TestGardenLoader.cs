using Godot;
using System.Collections.Generic;

public partial class TestGardenLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	DatabaseWrapper db = new DatabaseWrapper();
	PackedScene plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");

	public override void _Ready()
	{
		List<Plant> plantList = db.GetListOfAllPlants();
		PlaceAllPlants(plantList);
	}

	private void PlaceAllPlants(List<Plant> plantList) {
		int counter = 0;
		for (int x = 1; x <= 6; x++) {
			for (int y = 1; y <= 12; y++) {
				if (plantList.Count == counter){
					return;
				}

				var plantPrefabInstance = plantPrefab.Instantiate();
				AddChild(plantPrefabInstance);

				var plantWrapper = plantPrefabInstance.GetNode<Node2D>("../plant_wrapper");
				plantWrapper.Set("position", new Vector2(145*y, 175*x));
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
