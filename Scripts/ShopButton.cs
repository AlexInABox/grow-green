using Godot;
using System;

public partial class ShopButton : Button
{
	SceneManager sceneManager;
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		sceneManager.UpdateSaveBlocking();
		var shopScene = "res://Scenes/ShopSzene.tscn";
		GetTree().ChangeSceneToFile(shopScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
