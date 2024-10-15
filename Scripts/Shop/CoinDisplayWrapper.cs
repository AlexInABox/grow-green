using Godot;
using System;

namespace ShopScene;
public partial class CoinDisplayWrapper : Node2D
{
	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Label coinAmountLabel = GetNode<Label>("CoinAmountLabel");
        coinAmountLabel.Text = sceneManager.GetCoinCount().ToString();
	}
}