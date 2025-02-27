using Godot;
using System;

public partial class BuyGreenhouse : Button
{
	SceneManager sceneManager;
	SoundPlayer soundPlayer;

	private int greenhouseCost = 100;
	
	private MainSzene mainSzene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		sceneManager = GetNode<SceneManager>("/root/MainSzene/SceneManager");
		mainSzene = GetNode<MainSzene>("/root/MainSzene/MainSceneScript");
		Pressed += TryBuyGreenhouse;
		
	}

	public void TryBuyGreenhouse()
	{
		soundPlayer.PlayButtonCick();
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
