using Godot;
using System;

public partial class CreateGameButton : Button
{	

	TitleSceneManager TitleSceneManager;
	SoundPlayer soundPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		TitleSceneManager = GetNode<TitleSceneManager>("../TitleSceneManager");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		soundPlayer.PlayButtonCick();
		TitleSceneManager.SpawnUserConfirmationPopupRegardingSaveOverwriting();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
