using Godot;
using System;

public partial class ContinueGameButton : Button
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
		titleScreenManager.SaveMyPlayerObjectAndCreateTheGame();
		var newScenePath = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(newScenePath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
