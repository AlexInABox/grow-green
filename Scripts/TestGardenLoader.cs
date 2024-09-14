using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class TestGardenLoader : Node
{
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		PlaceRandomGrid();
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
