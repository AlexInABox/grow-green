using Godot;
using System;

namespace MainScene;
public partial class MinigameButton : Button
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
		var newScene = "res://Scenes/MinigameSzene.tscn";
		GetTree().ChangeSceneToFile(newScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
