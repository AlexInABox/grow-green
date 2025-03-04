using Godot;
using System;

public partial class AgainButton : Button
{
	SoundPlayer soundPlayer;

	[Export] private String scene;
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
		GetTree().ChangeSceneToFile(scene);
		GetParent().GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
