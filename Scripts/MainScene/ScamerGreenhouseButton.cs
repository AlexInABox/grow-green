using Godot;
using System;

public partial class ScamerGreenhouseButton : Button
{
	SceneManager sceneManager;
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += BubbleActivator;
	}

	private void BubbleActivator(){
		soundPlayer.PlayButtonCick();
		sceneManager.SpawnGreenhouseUnlockPopup();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
