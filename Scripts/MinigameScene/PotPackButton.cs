using Godot;
using System;

namespace MinigameScene;
public partial class PotPackButton : Button
{
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += ButtonPressedAudio;
	}

	private void ButtonPressedAudio()
	{
		soundPlayer.PlayButtonCick();
		ButtonPressed();
	}

	private void ButtonPressed()
	{
		var nextScene = "res://Scenes/PotPackOpeningMinigame.tscn";
		GetTree().ChangeSceneToFile(nextScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
