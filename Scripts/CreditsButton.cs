using Godot;
using System;

public partial class CreditsButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressedEvent;
	}
	
	private void ButtonPressedEvent()
	{
		var creditsScene = "res://Scenes/credits.tscn";
		GetTree().ChangeSceneToFile(creditsScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
