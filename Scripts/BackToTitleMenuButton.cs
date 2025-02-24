using Godot;
using System;

public partial class BackToTitleMenuButton : Button
{
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		soundPlayer.PlayButtonCick();
		var titleScreen = "res://Scenes/titleScreen.tscn";
		GetTree().ChangeSceneToFile(titleScreen);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
