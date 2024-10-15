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
		sceneManager = GetNode<SceneManager>("../../../../../SceneManager");
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
			GD.Print("DU ABSOLUTER VOLLIDIOT. DU BIST ARM!! KEIN GELD NIX DA!!");
		} else {
			while (sceneManager.IsSaveLocked())
			{
				GD.Print("Still locked");
				// Wait for .1 seconds (100 milliseconds)
				System.Threading.Thread.Sleep(100);
			}
			GD.Print("Natürlich kannst du " + className + " kaufen :)))");
			sceneManager.SetCoinCount(sceneManager.GetCoinCount() - cost);

			List<Plant> listOfOwnedPlants = sceneManager.GetListOfOwnedPlants();
			Plant plant = sceneManager.GetPlantByClassName(className);
			plant.spawnPoint = listOfOwnedPlants.Count + 1;
			listOfOwnedPlants.Add(plant);
		}
		GD.Print("Du hast noch " + sceneManager.GetCoinCount() + " coins!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
