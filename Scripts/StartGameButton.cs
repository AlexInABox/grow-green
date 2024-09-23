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
		var newScenePath = "res://Scenes/testGarden.tscn";
		GetTree().ChangeSceneToFile(newScenePath);
		// TODO Get selected node for character selection number
		
		Player player = new Player("Alexxx", 100, 1);
		GameMode gameMode = new GameMode(player);
		
		GD.Print("Player name: ", player.name);
		GD.Print("Alien: ", player.character);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
