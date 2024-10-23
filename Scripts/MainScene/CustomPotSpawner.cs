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
		sceneManager = GetNode<SceneManager>("/root/MainSzene/SceneManager");


		List<Pot> ownedPots = sceneManager.GetListOfOwnedPots();
		var counter = 0;
		foreach (Pot pot in ownedPots) {
			var customPotPrefabInstance = customPotPrefab.Instantiate();
			AddChild(customPotPrefabInstance);

			customPotPrefabInstance.Name = pot.Name;
			customPotPrefabInstance.AddChild(pot);

			var buttonWrapper = customPotPrefabInstance;
			buttonWrapper.Position = new Vector2(65*counter, 40);
			
			counter++;
		}
		GD.Print(ownedPots.Count);
		GD.Print(GetParent().GetTreeStringPretty());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
