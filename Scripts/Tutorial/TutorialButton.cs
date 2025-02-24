using Godot;
using System;

public partial class TutorialButton : Button
{	
	TitleSceneManager titleScreenManager;
	SoundPlayer soundPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		titleScreenManager = GetNode<TitleSceneManager>("../TitleSceneManager");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		soundPlayer.PlayButtonCick();
		titleScreenManager.SpawnTutorialPopup();
		AnimatedSprite2D buyGif = GetNode<AnimatedSprite2D>("../TutorialPopup/Popup/GIF");
		buyGif.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
