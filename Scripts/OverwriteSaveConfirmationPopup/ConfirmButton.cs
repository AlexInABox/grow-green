using Godot;
using System;

public partial class ConfirmButton : Button
{	

	TitleSceneManager TitleSceneManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TitleSceneManager = GetNode<TitleSceneManager>("../../../TitleSceneManager");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		TitleSceneManager.CreateNewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
