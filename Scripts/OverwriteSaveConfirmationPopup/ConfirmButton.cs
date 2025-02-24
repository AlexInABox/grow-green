using Godot;
using System;

public partial class ConfirmButton : Button
{	

	TitleSceneManager TitleSceneManager;
	SoundPlayer soundPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		TitleSceneManager = GetNode<TitleSceneManager>("../../../TitleSceneManager");
		Pressed += ButtonPressedEventAudio;
	}
	private void ButtonPressedEventAudio()
	{
		soundPlayer.PlayButtonCick();
		ButtonPressedEvent();
	}
	private void ButtonPressedEvent()
	{
		TitleSceneManager.CreateNewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
