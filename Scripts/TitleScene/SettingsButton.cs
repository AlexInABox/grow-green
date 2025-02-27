using Godot;
using System;

public partial class SettingsButton : Button
{
	// Called when the node enters the scene tree for the first time.
	private const string PathToSettingsScene = "res://Scenes/settings.tscn";
	public override void _Ready()
	{
		Pressed += ButtonPressedEvent;
	}
	
	private void ButtonPressedEvent()
	{
		GetTree().ChangeSceneToFile(PathToSettingsScene);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
