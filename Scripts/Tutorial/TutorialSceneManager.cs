using Godot;
using System;

public partial class TutorialSceneManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void LeaveTutorial(){
		string titleScene = "res://Scenes/titleScreen.tscn";
		GetTree().ChangeSceneToFile(titleScene);
	}
}
