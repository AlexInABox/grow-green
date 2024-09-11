using Godot;
using System;

public partial class Plant : Node
{
    public int cost; // Cost to purchase
    public int sellValue; // Value when fully grown
    public int yield; // Money Production per Day when fully grown
    public int growDurationInDays; // Needed days for next stage (e.g., 7 days)
    public int currentGrowProgress; // Current Progress (e.g., 3/7 days)
    public int stage; // Current Stage 0=DriedOut, 1=Seed, 2=Sprout, 3=YoungPlant, 4=FullyGrown, 5=Rotten
    public string name; // Name
    public string description; // Description
    public float cycle; // How often it needs to be watered
    public int waterStatus; // Current water level (0-100)
    long watertStatusTimestamp;
    public bool isDead; // True if the plant is dead
    public int wateringImpact; // Amount of water added per click
    public int decayRate; // Decay per Time (from 100)


    public Plant(){
		watertStatusTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
		GD.Print("KYS! : EPOCH: " + watertStatusTimestamp);
	}
    public override void _Ready()
    {
        CheckForGrowth();
        if (GetWaterStatus() == 0)
        {
            GD.Print("The plant has dried out and is dead!");
            isDead = true;
        }
    }

    public double GetWaterStatus()
    {
        return waterStatus;
    }

    public void Decay()
    {
        waterStatus -= decayRate;
        if (waterStatus <= 0)
        {
            waterStatus = 0;
            isDead = true;
        }
    }

    public void SetWaterStatus(float value)
    {
        waterStatus = (int)value;
    }

    public void WaterPlant()
    {
        waterStatus += wateringImpact;
        if (waterStatus > 100) waterStatus = 100; // Ensure water status does not exceed 100
        CheckForGrowth();
    }

    public void NextStage()
    {
        if (stage < 4) // Ensure stage does not exceed the maximum stage
        {
            stage++;
            currentGrowProgress = 0; // Reset grow progress for the next stage
        }
    }

    public void CheckForGrowth()
    {
        if (currentGrowProgress >= growDurationInDays)
        {
            NextStage();
        }
        else
        {
            currentGrowProgress++;
        }
    }

    public bool CheckForDead()
    {
        if (GetWaterStatus() <= 0 || GetWaterStatus() >= 100)
        {
            GD.Print("The plant is dead!");
            isDead = true;
        }
        return isDead;
    }

    public override void _Process(double delta)
    {
        if (!isDead)
        {
            Decay();
            CheckForGrowth();
        }
    }
}
