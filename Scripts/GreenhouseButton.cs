using Godot;
using System;

public partial class GreenhouseButton : Button
{
	SceneManager sceneManager;

	private Sprite2D schloss;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += ButtonPressed;
		schloss = GetNode<Sprite2D>("Sprite2D");
		schloss.Visible = false;

		if(Disabled){
			schloss.Visible = true;
		}
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
