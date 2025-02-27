using Godot;
using System;

public partial class LeaveButton : Button
{
	SoundPlayer soundPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += VerlassenAudio;
	}

	private void VerlassenAudio(){
		soundPlayer.PlayButtonCick();
		Verlassen();
	}

	private void Verlassen(){
		GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
