using Godot;
using System;

public partial class QuitGreenhousePopup : Button
{
	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../../../SceneManager");
		Pressed += Verlassen;
	}

	private void Verlassen(){
		var shopScene = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(shopScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
