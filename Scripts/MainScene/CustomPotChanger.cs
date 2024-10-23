using Godot;
using System.Collections.Generic;

public partial class CustomPotChanger : Button
{
	private Panel potSelectPanel;
	private bool isSelected;
	private List<PlantButton> listOfActivePlantButtons = new List<PlantButton>();

	public override void _Ready()
	{
		potSelectPanel = GetNode<Panel>("/root/MainSzene/PotSkins/Panel");

		Pressed += OnButtonPressed;
	}

	private void OnButtonPressed(){
		foreach(Node2D spawnPoint in GetNode<Node>("/root/MainSzene/SpawnPointWrapper").GetChildren()) {
			if (spawnPoint.Name == "Trash"){
				continue;
			}

			PlantButton plantButton = spawnPoint.GetNodeOrNull<PlantButton>("plant_wrapper/Button");
			GD.Print(spawnPoint.GetTreeStringPretty());
			if (plantButton is not null) {
				listOfActivePlantButtons.Add(plantButton);
			}
		}
		GD.Print("So many activePlants!" + listOfActivePlantButtons.Count);

		potSelectPanel.Hide();
		isSelected = true;

		RegisterChangeSprite();
	}

	private void ChangeSprite(PlantButton plantButton) {
		if (isSelected) {
			isSelected = false;
			plantButton.GetNode<Plant>("../Plant").pot = GetNode<Pot>("../Pot").potName;
			plantButton.GetNode<Sprite2D>("../Pot").Texture = GetNode<Sprite2D>("../Pot").Texture;
		}
	}

	private void RegisterChangeSprite() {
		foreach(PlantButton plantButton in listOfActivePlantButtons) {
			plantButton.Pressed += () => ChangeSprite(plantButton);
		}
	}


	public override void _Process(double delta)
	{
		if (isSelected) {
			
		}
	}
}
