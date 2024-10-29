using Godot;
using System;

public partial class PlantCrushGame : Node
{

	private CrushablePlant selectedPlant;
	private string[] nodeNames;
	private PackedScene prefab;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		nodeNames = new[] { "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6", "C1", "C2", "C3", "C4", "C5", "C6" };
		prefab = (PackedScene)ResourceLoader.Load("res://Prefabs/crushable_plant.tscn");
		SpawnPrefabsOnNodes();
	}

	public void swapPlants(CrushablePlant selectedPlant, CrushablePlant newPlant)
	{
		(selectedPlant.GlobalPosition, newPlant.GlobalPosition) = (newPlant.GlobalPosition, selectedPlant.GlobalPosition);
	}
	
	private void SpawnPrefabsOnNodes()
	{
		foreach (string nodeName in nodeNames)
		{
			
			Node2D targetNode = GetNode<Node2D>(nodeName);

			if (targetNode != null && prefab != null)
			{
				
				Node2D instance = (Node2D)prefab.Instantiate();

			
				targetNode.AddChild(instance);
			}
			else
			{
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
