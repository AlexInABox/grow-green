using Godot;
using System;

public partial class ShopButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		var shopScene = "res://Scenes/ShopSzene.tscn";
		GetTree().ChangeSceneToFile(shopScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
