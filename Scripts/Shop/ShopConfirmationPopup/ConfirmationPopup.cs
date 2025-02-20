using Godot;
using System;

public partial class ConfirmationPopup : Node2D
{
	SceneManager sceneManager;
	public string PlantName { get; private set; }
	
	public void SetClassName(string className)
	{
		PlantName = className;
		GD.Print(className);
	}
 	public int Price { get; private set; }
	
	public void SetPrice(int price)
	{
		Price = price;
	}

	public void ChangeLabel()
	{
		Label warning = GetNode<Label>("Sprite2D/Warning");
		warning.Text = $"Möchtest du {PlantName} wirklich Kaufen?";
	}

	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		Button buyButton = GetNode<Button>("Sprite2D/Buy");
		if (sceneManager.GetCoinCount() < Price) {
			Label warning = GetNode<Label>("Sprite2D/Warning");
			warning.Text = $"Dafür reicht ihr Geld leider nicht aus";
			buyButton.Disabled = true;
		} 
		buyButton.Pressed += Buy;
	}

	private void Buy(){
			while (sceneManager.IsSaveLocked())
			{
				// Wait for .1 seconds (100 milliseconds)
				System.Threading.Thread.Sleep(100);
			}
			sceneManager.SetCoinCount(sceneManager.GetCoinCount() - Price);

			Plant plant = sceneManager.GetPlantByClassName(PlantName);
			sceneManager.AddNewPlantToListOfOwnedPlants(plant);
			QueueFree(); 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
