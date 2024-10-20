using Godot;
using System;

public partial class ScamerGreenhouseButton : Button
{
	SceneManager SceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += BubbleActivator;
	}

	private void BubbleActivator(){
		SceneManager.SpawnGreenhouseUnlockPopup();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
