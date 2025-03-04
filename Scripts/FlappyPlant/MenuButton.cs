using Godot;
using System;

public partial class MenuButton : Button
{

	[Export] String scene;
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
		//var mainScene = "res://Scenes/FlappyPlant/FPMainMenu.tscn";
		GetTree().ChangeSceneToFile(scene);
		GetParent().GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



