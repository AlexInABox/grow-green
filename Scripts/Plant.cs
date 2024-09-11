using Godot;
using System;

public partial class Plant : Node
{

	public int cost; //Cost to purchase
	public int sellValue; //Value when fully grown
	public int yield; //Money Production per Day when fully grown
	public int growDurationInDays; //Needed days for next stage (e.g. 7 days)
	public int currentGrowProgress; //Current Progress (e.g. 3/7 days)
	public int stage; //Current Stage 0=DriedOut, 1=Seed .... 3=FullyGrown, 4=Rotten
	public String name; //Name
	public String description; //Description
	public float cycle; //How often does it need to be watered
	public int waterStatus; //Current water level (0-100)
	public bool isDead; //True if the plant is dead
	public int wateringImpact; //Amount of water added per click
	public int decayRate; //Decay per Time (fromm 100)


	// Called when the node enters the scene tree for the first time.
	public Plant(int cost, int sellValue, int yield, int growDurationInDays, int currentGrowProgress, int stage, string name, string description, float cycle, int wateringImpact, int decayRate)
	{
		this.cost = cost;
		this.sellValue = sellValue;
		this.yield = yield;
		this.growDurationInDays = growDurationInDays;
		this.currentGrowProgress = currentGrowProgress;
		this.stage = stage;
		this.name = name;
		this.description = description;
		this.cycle = cycle;
		this.wateringImpact = wateringImpact;
		this.decayRate = decayRate;
	}

	public override void _Ready()
	{
		CheckForGrowth();
		if (GetWaterStatus() == 0) {
			GD.Print("KYS! im mining bittcoin now! :3");
			this.isDead = true;
		}

	}

	public double GetWaterStatus(){
		return waterStatus;
	}

	public void Decay()
	{
		waterStatus = waterStatus - decayRate;
	}
	public void SetWaterStatus(float value){}
	public void WaterPlant(){
		waterStatus += wateringImpact;
		
	}

	public void NextStage(){
		GD.PushError("KYS!!");
	}

	public void CheckForGrowth(){
		if (currentGrowProgress >= growDurationInDays){
			NextStage();
		}
	}
	
	public bool CheckForDead(){
		if (GetWaterStatus() >= 100 || GetWaterStatus() <= 0) {
			GD.Print("KYS! Dont kill the plant idio!");
			this.isDead = true;
			
		}
		return isDead;
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
