using Godot;
using System;

public partial class BackToTitleMenuButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		var titleScreen = "res://Scenes/titleScreen.tscn";
		GetTree().ChangeSceneToFile(titleScreen);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
