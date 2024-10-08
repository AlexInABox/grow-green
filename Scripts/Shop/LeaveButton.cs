using Godot;
using System;

public partial class LeaveButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		var mainScene = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(mainScene);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}