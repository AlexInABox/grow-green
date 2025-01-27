using Godot;
using System;

public partial class ScamerGreenhouseButton : Button
{
	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetTree().GetCurrentScene().GetNode<SceneManager>("SceneManager");
		Pressed += BubbleActivator;
	}

	private void BubbleActivator(){
		sceneManager.SpawnGreenhouseUnlockPopup();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
