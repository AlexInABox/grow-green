using Godot;
using System;

public partial class ShopButton : Button
{
	SceneManager sceneManager;
	SoundPlayer soundPlayer;
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		soundPlayer.PlayButtonCick();
		sceneManager.UpdateSaveBlocking();
		var shopScene = "res://Scenes/ShopSzene.tscn";
		GetTree().ChangeSceneToFile(shopScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
