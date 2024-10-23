using Godot;
using System.Collections.Generic;

public partial class PotChanger : Node
{

	// Referenz zum Sprite, das die Textur wechseln soll
	private Sprite2D targetSprite;

	private Panel PotPanel;

	private Node SpawnPoint;

	private int i = 1;

	private List<Sprite2D> sprites = new List<Sprite2D>();

	private List<Sprite2D> buttonSprites = new List<Sprite2D>();

	private List<Button> buttons = new List<Button>();

	private List<Button> SpawnpointButtons = new List<Button>();

	// Speichert, ob ein Button gedrückt wurde, um Sprite zu wählen
	private bool isSelectingSprite = false;

	// Speichert den Index der Textur, die dem gedrückten Button entspricht
	private int selectedTextureIndex = -1;

	public override void _Ready()
	{
		SpawnPoint = GetNode<Node>("../SpawnPointWrapper");

		foreach(Node2D spawns in SpawnPoint.GetChildren()){
			Sprite2D potSprite = spawns.GetNodeOrNull<Sprite2D>("plant_wrapper/Pot");
			if (potSprite != null)  // Sicherstellen, dass der Node gefunden wird
			{
				sprites.Add(potSprite);
			}
			Button SpawnpointButton = spawns.GetNodeOrNull<Button>("plant_wrapper/Button");
			if (SpawnpointButton != null)
			{
				SpawnpointButtons.Add(SpawnpointButton);
			}
		}		

		PotPanel = GetNode<Panel>("../PotSkins/Panel");
		GD.Print(PotPanel);
		
		foreach (Node child in PotPanel.GetChildren()){
			if(child is Button Puttons){
				Sprite2D buttonSprite = Puttons.GetNode<Sprite2D>("Sprite2D");
				if (buttonSprite != null)
				{
					buttonSprite.Name = Puttons.Name;
				 buttonSprites.Add(buttonSprite);
				}
				buttons.Add(Puttons);
				GD.Print(Puttons);
			}
		}

		for (int n= 0; n < buttons.Count; n++){
			int index = n;
			buttons[n].Pressed += () => OnButtonPressed(index);
		}

	}

	// Wenn ein Button gedrückt wird, aktiviere den Sprite-Auswahlmodus
	private void OnButtonPressed(int i)
	{
		if (i >= 0 && i < buttonSprites.Count)
		{
			// Setze den Modus zum Auswählen eines Sprites aktiv
			isSelectingSprite = true;
			// Speichere, welche Textur ausgewählt wurde
			selectedTextureIndex = i;
			GD.Print("Button " + i + " wurde gedrückt. Wähle nun einen Sprite.");
			PotPanel.Visible = false;
			// Schalte die Auswahl für die Sprites ein
			for (int m = 0; m < sprites.Count; m++)
			{
				int spriteIndex = m;
				var button = SpawnpointButtons[m];
				button.Pressed += () => SelectSprite(sprites[spriteIndex]);
			}
		}
	}

	// Wählt den Sprite aus und ändert seine Textur
	private void SelectSprite(Sprite2D sprite)
	{
		if (isSelectingSprite)
		{
			targetSprite = sprite;
			GD.Print("Sprite " + targetSprite.Name + " wurde ausgewählt.");

			// Ändere die Textur des ausgewählten Sprites
			if (targetSprite != null && selectedTextureIndex >= 0)
			{
				targetSprite.Texture = buttonSprites[selectedTextureIndex].Texture;
				GD.Print("Textur geändert für " + targetSprite.Name);
			}

			// Deaktiviere den Sprite-Auswahlmodus nach der Auswahl
			isSelectingSprite = false;


			//
			Plant ourPlant = targetSprite.GetNode<Plant>("../Plant");
			ourPlant.pot = buttonSprites[selectedTextureIndex].Name;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
