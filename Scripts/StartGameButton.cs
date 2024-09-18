using Godot;
using System;

public partial class StartGameButton : Button
{

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		var newScenePath = "res://Scenes/testGarden.tscn"; // Replace with your scene path
		GetTree().ChangeSceneToFile(newScenePath);
		Player player = new Player();
		GameMode gameMode = new GameMode(player);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
