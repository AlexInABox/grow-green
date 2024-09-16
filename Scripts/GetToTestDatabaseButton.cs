using Godot;
using System;

public partial class GetToTestDatabaseButton : Button
{

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		var newScenePath = "res://Scenes/databaseTests.tscn"; // Replace with your scene path
		GetTree().ChangeSceneToFile(newScenePath);
		Player player = new Player();
		GameMode gameMode = new GameMode(player);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
