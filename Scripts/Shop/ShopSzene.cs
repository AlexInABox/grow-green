using Godot;
using System;

public partial class ShopSzene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		audioPlayer.PlayShopMusic();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
