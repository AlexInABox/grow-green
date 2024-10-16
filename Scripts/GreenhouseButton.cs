using Godot;
using System;

public partial class GreenhouseButton : Button
{
	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		sceneManager.UpdateSaveBlocking();
		var newScene = "res://Scenes/GreenhouseScene.tscn";
		GetTree().ChangeSceneToFile(newScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
