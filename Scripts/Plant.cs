using Godot;
using System;

public partial class Plant : Sprite2D
{
	//constants
	public string className; // Class name
	public string plantName; // Name
	public double decayRatePerDay; // Decay per Time (from 100)
	public double growRatePerDay = 10; // Rate of growth per day
	public int wateringImpact = 10; // Amount of water added per click
	public int cost; // Cost to purchase
	public int sellValue; // Value when fully grown
	public int yield; // Money Production per Day when fully grown
	public string difficulty;
	//end-of constants

	public double growProgress; // Current Progress (0-100) TEST
	public long growProgressTimestamp = 0; // epoch
	public double waterLevel; // Current water level (0-100)
	public long waterLevelTimestamp = 0; // epoch
	public bool withered = false; // True if the plant is dead
	public bool rotten = false; // True if the plant is dead
	public string pot;
	public int spawnPoint;    
	private string[] growthTextures;


	//status bubble stuff
	ColorRect growProgressBar;
<<<<<<< Updated upstream
	ColorRect waterLevelBar;
=======
	ColorRect growProgressBarL;
	ColorRect waterLevelBar;
	ColorRect waterLevelBarL;
>>>>>>> Stashed changes


	public Plant(int spawnPoint) {
		this.className = "Agave";
		this.plantName = "TestName";
		this.difficulty = "easy";
		this.decayRatePerDay = 100 / 0.001;
		this.cost = 5;
		this.sellValue = 3;
		this.yield = 1;
		this.growRatePerDay = 10;
		this.pot = "minecraft_chicken";
		this.spawnPoint = spawnPoint;

		this.growProgress = 0.8;
		this.waterLevel = 50;
	}
	public Plant(string className, string name, string difficulty, int waterEveryXDays, int cost, int sellValue, int yield) {
		this.className = className;
		this.plantName = name;
		this.difficulty = difficulty;
		this.decayRatePerDay = 100 / waterEveryXDays;
		this.cost = cost;
		this.sellValue = sellValue;
		this.yield = yield;

<<<<<<< Updated upstream
		growProgress = 1;
		waterLevel = 50;
		pot = "default";
=======
		growProgress = 0.01;
		waterLevel = 50;
		pot = "plain_brown";
>>>>>>> Stashed changes
		this.spawnPoint = 1;
	}
	public Plant(string className, string name, string difficulty, int waterEveryXDays, int cost, int sellValue, int yield, double growProgress, long growProgressTimestamp, double waterLevel, long waterLevelTimestamp, bool withered, bool rotten, string pot, int spawnPoint) {
		this.className = className;
		this.plantName = name;
		this.difficulty = difficulty;
		this.decayRatePerDay = 100 / waterEveryXDays;
		this.cost = cost;
		this.sellValue = sellValue;
		this.yield = yield;

		this.growProgress = growProgress;
		this.growProgressTimestamp = growProgressTimestamp;
		this.waterLevel = waterLevel;
		this.waterLevelTimestamp = waterLevelTimestamp;
		this.withered = withered;
		this.rotten = rotten;

		this.pot = pot;
		this.spawnPoint = spawnPoint;
	}

	public override void _Ready()
	{
		Name = "Plant";
		ZIndex = 2;

		if (waterLevelTimestamp == 0) waterLevelTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
		if (growProgressTimestamp == 0) growProgressTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

		string normalizedClassName = className.ToLower().Replace(" ", "");
		growthTextures = new string[5]{
			"res://Textures/Plants/withered.png", //Dried up
			"res://Textures/Plants/rotten.png", //Had too much to dink
			$"res://Textures/Plants/{normalizedClassName}1.png",         // Stage 1: Seed
			$"res://Textures/Plants/{normalizedClassName}2.png",       // Stage 2: Sprout
			$"res://Textures/Plants/{normalizedClassName}3.png",  // Stage 3: Young Plant
		};

		//GD.Print(GetParent().GetTreeStringPretty());

		growProgressBar = GetNode<ColorRect>("../statusBubble/growProgressWrapper/ColorRect");
<<<<<<< Updated upstream
		waterLevelBar = GetNode<ColorRect>("../statusBubble/waterLevelWrapper/ColorRect");
=======
		growProgressBarL = GetNode<ColorRect>("../statusBubbleLeft/growProgressWrapper/ColorRect");
		waterLevelBar = GetNode<ColorRect>("../statusBubble/waterLevelWrapper/ColorRect");
		waterLevelBarL = GetNode<ColorRect>("../statusBubbleLeft/waterLevelWrapper/ColorRect");
>>>>>>> Stashed changes

		RefreshMetadata();
	}

	public void RefreshMetadata() {
		RecalculateWaterLevel();
		RecalculateGrowProgress();
		RefreshTexture();
		RefreshStatusBubble();
	}

	public void WaterPlant() {
<<<<<<< Updated upstream
		waterLevel += wateringImpact;
		RefreshMetadata();
=======
		if (waterLevel < 100)
		{
			waterLevel += wateringImpact;
			RefreshMetadata();
		}
>>>>>>> Stashed changes
	}

	private void RecalculateWaterLevel() {
		long timeSinceLastTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - waterLevelTimestamp;
<<<<<<< Updated upstream
		waterLevel -= (decayRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp;
=======
		waterLevel -= (decayRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp * 10000;
>>>>>>> Stashed changes
		//GD.Print("WaterLevel: " + waterLevel);
		waterLevelTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

		if (waterLevel > 100) rotten = true;
		if (waterLevel <= 0) withered = true;
	}

	private void RecalculateGrowProgress() {
		if (withered) {
			growProgress = -1;
			return;
		}
		if (rotten) {
			growProgress = 0;
			return;
		}

<<<<<<< Updated upstream
		long timeSinceLastTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - growProgressTimestamp;
		growProgress += (growRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp;
		//GD.Print("GrowProgress: " + growProgress);
		growProgressTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
=======
		if (growProgress <= 1)
		{
			long timeSinceLastTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - growProgressTimestamp;
			growProgress += (growRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp * 500;
			//GD.Print("GrowProgress: " + growProgress);
			growProgressTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
		}
>>>>>>> Stashed changes
	}

	private void RefreshTexture() {
		if (growProgress == -1) {
			Texture = (Texture2D)GD.Load(growthTextures[0]);
			return;
		}
		if (growProgress == 0) {
			Texture = (Texture2D)GD.Load(growthTextures[1]);
			return;
		}
		if (growProgress > 0) {
			Texture = (Texture2D)GD.Load(growthTextures[2]);
		}
<<<<<<< Updated upstream
		if (growProgress > 40) {
			Texture = (Texture2D)GD.Load(growthTextures[3]);
		}
		if (growProgress > 80) {
=======
		if (growProgress > 0.4) {
			Texture = (Texture2D)GD.Load(growthTextures[3]);
		}
		if (growProgress >0.8) {
>>>>>>> Stashed changes
			Texture = (Texture2D)GD.Load(growthTextures[4]);
		}
	}

	private void RefreshStatusBubble(){
		growProgressBar.SetSize(new Vector2((float)(66 * growProgress), 9.45f));
<<<<<<< Updated upstream
		waterLevelBar.SetSize(new Vector2((float)(46 * (waterLevel/100)), 7.085f));
=======
		growProgressBarL.SetSize(new Vector2((float)(66 * growProgress), 9.45f));
		waterLevelBar.SetSize(new Vector2((float)(46 * (waterLevel/100)), 7.085f));
		waterLevelBarL.SetSize(new Vector2((float)(46 * (waterLevel/100)), 7.085f));
>>>>>>> Stashed changes
	}


	public override void _Process(double delta)
	{
		RefreshMetadata();
		//should we use a thread here? meh its fine ig.
	}
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
