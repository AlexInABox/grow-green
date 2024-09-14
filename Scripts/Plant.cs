using Godot;
using System;

public partial class Plant : Sprite2D
{
    //constants
    public string className; // Class name
    public string name; // Name
    public float decayRatePerDay; // Decay per Time (from 100)
    public float growRatePerDay = 10; // Rate of growth per day
    public int wateringImpact = 10; // Amount of water added per click
    public int cost; // Cost to purchase
    public int sellValue; // Value when fully grown
    public int yield; // Money Production per Day when fully grown
    //end-of constants

    public float growProgress; // Current Progress (0-100)
    public long growProgressTimestamp = 0; // epoch
    public float waterLevel; // Current water level (0-100)
    public long waterLevelTimestamp = 0; // epoch
    public bool withered = false; // True if the plant is dead
    public bool rotten = false; // True if the plant is dead
    
    private string[] growthTextures;


    public Plant() {
        this.className = "Agave";
        this.name = "TestName";
        this.decayRatePerDay = 100 / 0.001f;
        this.cost = 5;
        this.sellValue = 3;
        this.yield = 1;
        this.growRatePerDay = 1000000;

        this.growProgress = 1;
        this.waterLevel = 50;
    }
    public Plant(string className, string name, int waterEveryXDays, int cost, int sellValue, int yield) {
        this.className = className;
        this.name = name;
        this.decayRatePerDay = 100 / waterEveryXDays;
        this.cost = cost;
        this.sellValue = sellValue;
        this.yield = yield;

        growProgress = 1;
        waterLevel = 50;
    }

    public Plant(string className, string name, int waterEveryXDays, int cost, int sellValue, int yield, float growProgress, long growProgressTimestamp, float waterLevel, long waterLevelTimestamp, bool withered, bool rotten) {
        this.className = className;
        this.name = name;
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
    }

    public override void _Ready()
    {
        if (waterLevelTimestamp == 0) waterLevelTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (growProgressTimestamp == 0) growProgressTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        string normalizedClassName = className.ToLower().Replace(" ", "_");
        growthTextures = new string[5]{
            "res://Textures/Plants/withered.png", //Dried up
            "res://Textures/Plants/rotten.png", //Had too much to dink
            $"res://Textures/Plants/{normalizedClassName}1.png",         // Stage 1: Seed
            $"res://Textures/Plants/{normalizedClassName}2.png",       // Stage 2: Sprout
            $"res://Textures/Plants/{normalizedClassName}3.png",  // Stage 3: Young Plant
        };
        RefreshMetadata();
        
    }

    public void RefreshMetadata() {
        RecalculateWaterLevel();
        RecalculateGrowProgress();
        RefreshTexture();
    }

    public void WaterPlant() {
        waterLevel += wateringImpact;
        RefreshMetadata();
    }

    private void RecalculateWaterLevel() {
        long timeSinceLastTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - waterLevelTimestamp;
        waterLevel -= (decayRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp;
        GD.Print("WaterLevel: " + waterLevel);
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

        long timeSinceLastTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds() - growProgressTimestamp;
        growProgress += (growRatePerDay / 24 / 60 / 60) * timeSinceLastTimestamp;
        GD.Print("GrowProgress: " + growProgress);
        growProgressTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
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
        if (growProgress > 40) {
            Texture = (Texture2D)GD.Load(growthTextures[3]);
        }
        if (growProgress > 80) {
            Texture = (Texture2D)GD.Load(growthTextures[4]);
        }
    }


    public override void _Process(double delta)
    {
        RefreshMetadata();
    }
}
