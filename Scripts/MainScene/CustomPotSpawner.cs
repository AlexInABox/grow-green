using Godot;
using System;
using System.Collections.Generic;

public partial class CustomPotSpawner : HBoxContainer
{
	PackedScene customPotPrefab = GD.Load<PackedScene>("res://Prefabs/custom_pot_wrapper.tscn");
	SceneManager sceneManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetTree().Root.GetChildren()[0].GetNode<SceneManager>("SceneManager");

		List<Pot> ownedPots = sceneManager.GetListOfOwnedPots();
		var counter = 0;
		foreach (Pot pot in ownedPots) {
			var customPotPrefabInstance = customPotPrefab.Instantiate();
			AddChild(customPotPrefabInstance);

			var castedInstance = (Node2D)customPotPrefabInstance;
			castedInstance.AddChild(pot);
			castedInstance.Position = new Vector2(80 + (128*counter), 40);
			castedInstance.GetNode<Button>("Button").Name = pot.potName;
			
			counter++;
		}
		CustomMinimumSize = new Vector2(128*counter, 130);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
