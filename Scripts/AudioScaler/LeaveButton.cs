using Godot;
using System;

public partial class LeaveButton : Button
{
	SoundPlayer soundPlayer;
	SettingsManager settingsManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		settingsManager = (SettingsManager)GetNode("/root/SettingsManager");
		Pressed += VerlassenAudio;
	}

	private void VerlassenAudio(){
		soundPlayer.PlayButtonCick();
		Verlassen();
	}

	private void Verlassen(){
		settingsManager.settingsDisplayed = false;
		GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
