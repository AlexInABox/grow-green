using Godot;
using System;

public partial class BuyGreenhouse : Button
{
	SceneManager sceneManager;

	private int greenhouseCost = 1;
	
	private MainSzene mainSzene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetTree().GetCurrentScene().GetNode<SceneManager>("SceneManager");
		mainSzene = GetTree().GetCurrentScene().GetNode<MainSzene>("MainSceneScript");
		Pressed += TryBuyGreenhouse;
	}

	public void TryBuyGreenhouse()
	{
		if (sceneManager.GetCoinCount() >= greenhouseCost)
		{
			sceneManager.SetCoinCount(sceneManager.GetCoinCount() - greenhouseCost);
			BoughtGreenhouse();
		}
	}

	public void BoughtGreenhouse(){
		sceneManager.SetHasUnlockedGreenhouse(true);
		mainSzene.CheckForGreenhouse();
		GetParent().GetParent().QueueFree();
		
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
