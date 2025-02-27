using Godot;
using System;

public partial class LeaveTutorialButton : Button
{
	TutorialSceneManager tutorialSceneManager;
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		tutorialSceneManager = GetNode<TutorialSceneManager>("../TutorialSceneManager");
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		soundPlayer.PlayButtonCick();
		tutorialSceneManager.LeaveTutorial();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
