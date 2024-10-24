using Godot;
using System;

public partial class BuyGreenhouse : Button
{
	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../../../SceneManager");
		Pressed += Gekauft;
	}

	private void Gekauft(){
		var shopScene = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(shopScene);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
