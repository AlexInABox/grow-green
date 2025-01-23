using Godot;
using System.Collections.Generic;

	public partial class EasyPlantsContainercs : HBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	PackedScene shopPrefab = GD.Load<PackedScene>("res://Prefabs/shop_wrapper.tscn");
	SceneManager sceneManager;

	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../../../SceneManager");
		List<Plant> plantList = sceneManager.GetListOfAllPlants(); 
		PlaceAllPlants(plantList);
	}

	private void PlaceAllPlants(List<Plant> plantList) {
		int counter = 0;
		foreach (Plant plant in plantList) {
			if (plant.difficulty == "easy"){
			var PlantName = plant.className.ToLower().Replace(" ", "");
			string PlantTexture = $"res://Textures/Plants/{PlantName}3.png";
			string SeedTexture = $"res://Textures/Plants/SeedIcon.png";

			Sprite2D myNewSprite = new Sprite2D();

			var shopPrefabInstance = shopPrefab.Instantiate();
			AddChild(shopPrefabInstance);
			var easyShopWrapper = shopPrefabInstance.GetNode<Node2D>("../ShopWrapper");
			easyShopWrapper.Set("position", new Vector2(240*counter, 110));
			easyShopWrapper.Set("name", "freaky" + counter);

			Button shopButton = easyShopWrapper.GetNode<Button>("PlantButton");
			Sprite2D Bubble = easyShopWrapper.GetNode<Sprite2D>("statusBubble");
			Label NameLabel = easyShopWrapper.GetNode<Label>("statusBubble/NameLabel");
			Label DifficultyLabel = easyShopWrapper.GetNode<Label>("statusBubble/DifficultyLabel");
			Label PreisLabel = easyShopWrapper.GetNode<Label>("statusBubble/Price");
			Sprite2D BubblePlant = easyShopWrapper.GetNode<Sprite2D>("statusBubble/Plant");
			
			var styleBoxTextureNormal = new StyleBoxTexture();
			styleBoxTextureNormal.Texture = (Texture2D)GD.Load(PlantTexture);
			var styleBoxTextureSeed = new StyleBoxTexture();
			/* styleBoxTextureSeed.Texture = (Texture2D)GD.Load(SeedTexture); */
			var styleBoxTextureSeedPressed = new StyleBoxTexture();
			styleBoxTextureSeedPressed.Texture = (Texture2D)GD.Load(SeedTexture);
			styleBoxTextureSeedPressed.ModulateColor = new Color(0f, 0f, 0f);
			/* var styleBoxTextureNormalHovered = new StyleBoxTexture();
			styleBoxTextureNormalHovered.Texture = (Texture2D)GD.Load(PlantTexture);
			styleBoxTextureNormalHovered.RegionRect = new Rect2(Vector2.Zero, new Vector2(64, 64)); 
 */
			var theme = Theme ?? new Theme();

			theme.SetStylebox("normal", "Button", styleBoxTextureNormal);
			theme.SetStylebox("hover", "Button", styleBoxTextureSeed);
			theme.SetStylebox("pressed", "Button", styleBoxTextureSeedPressed);

			shopButton.Theme = theme;
			shopButton.CustomMinimumSize = new Vector2(240, 240);

			Bubble.Visible = false;
			Bubble.Position = new Vector2(120, 80);
			Bubble.Scale = new Vector2(8, 8);

			NameLabel.Text = plant.className;
			BubblePlant.Texture = (Texture2D)GD.Load(PlantTexture);
			DifficultyLabel.Text = "leicht";
			DifficultyLabel.AddThemeColorOverride("font_color", new Color(0.3058823529411765f, 0.6509803921568628f, 0.5294117647058824f));
			string costAsString = plant.cost.ToString();
			PreisLabel.Text = costAsString;

			ShopPlantButton shopButtonScript = easyShopWrapper.GetNode<ShopPlantButton>("PlantButton");
			shopButtonScript.className = plant.className;
			shopButtonScript.cost = plant.cost;

			counter++;
			easyShopWrapper.AddChild(myNewSprite);
			}
		}
		CustomMinimumSize = new Vector2(110+(240*counter), 240);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
