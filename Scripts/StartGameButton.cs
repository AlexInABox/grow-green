using Godot;
using System;

public partial class StartGameButton : Button
{

	bool userPressed = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		GD.Print("Hello world!");
		Text = "WARUM??!?!";
		userPressed = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (userPressed) {
			Text += "!?";
		}
	}
}
