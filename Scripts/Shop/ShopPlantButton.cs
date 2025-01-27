using Godot;
using System;
using System.Collections.Generic;

public partial class ShopPlantButton : Button
{
	public int cost = 0;
	public string className = "";

	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetTree().GetCurrentScene().GetNode<SceneManager>("SceneManager");
		MouseEntered += ButtonHovered;
		MouseExited += ButtonNotHovered;
		Pressed += ButtonGotPressed;
	}

	private void ButtonHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Show();
	}

	private void ButtonNotHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Hide();
	}

	private void ButtonGotPressed() {
		if (sceneManager.GetCoinCount() < cost) {
		} else {
			while (sceneManager.IsSaveLocked())
			{
				// Wait for .1 seconds (100 milliseconds)
				System.Threading.Thread.Sleep(100);
			}
			sceneManager.SetCoinCount(sceneManager.GetCoinCount() - cost);

			Plant plant = sceneManager.GetPlantByClassName(className);
			sceneManager.AddNewPlantToListOfOwnedPlants(plant);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
