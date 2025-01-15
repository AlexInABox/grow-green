using Godot;
using System;

public partial class MenuButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressedEvent;
	}
	
	private void ButtonPressedEvent()
	{
		var mainScene = "res://Scenes/FlappyPlant/FPMainMenu.tscn";
		GetTree().ChangeSceneToFile(mainScene);
		GetParent().GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



