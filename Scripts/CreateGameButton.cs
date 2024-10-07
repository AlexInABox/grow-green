using Godot;
using System;

public partial class CreateGameButton : Button
{	

	TitleSceneManager TitleSceneManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TitleSceneManager = GetNode<TitleSceneManager>("../TitleSceneManager");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		TitleSceneManager.CreateNewGame();

		var newScenePath = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(newScenePath);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
