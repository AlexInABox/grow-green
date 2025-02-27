using Godot;
using System;

public partial class AgainButton : Button
{
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += ButtonPressedEventAudio;
	}
	
	private void ButtonPressedEventAudio()
	{
		soundPlayer.PlayButtonCick();
		ButtonPressedEvent();
	}
	private void ButtonPressedEvent()
	{
		var mainScene = "res://Scenes/FlappyPlant/FPGame.tscn";
		GetTree().ChangeSceneToFile(mainScene);
		GetParent().GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
