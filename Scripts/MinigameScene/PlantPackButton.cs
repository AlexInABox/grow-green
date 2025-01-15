using Godot;
using System;

namespace MinigameScene;
public partial class PlantPackButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		//var nextScene = "res://Scenes/PackOpeningMinigame.tscn";
		var nextScene = "res://Scenes/FlappyPlant/FPMainMenu.tscn";
		GetTree().ChangeSceneToFile(nextScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
