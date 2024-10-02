using Godot;
using System;

public partial class StartGameButton : Button
{
	public int characterNumber = 6;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		var newScenePath = "res://Scenes/loadPlayerObject.tscn";
		GetTree().ChangeSceneToFile(newScenePath);
		
		Player player = new Player(characterNumber);
		
		GD.Print("Player name: ", player.username);
		GD.Print("Alien: ", player.characterId);
	}
	
	public void SetCharacterNumber(int number)
	{
		characterNumber = number;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
