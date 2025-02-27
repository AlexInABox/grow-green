using Godot;
using System;

namespace MinigameScene;
public partial class LeaveButton : Button
{
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		soundPlayer.PlayButtonCick();
		var nextScene = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(nextScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
