using Godot;
using System;

public partial class Plant : Node
{

	int cost;
	int sellValue;
	int yield;
	float growDurationInDays;
	float currentGrowProgress;
	int stage;
	String name;
	String description;
	float cycle;
	float waterStatus;
	bool isDead;
	float wateringImpact;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CheckForGrowth();
		if (GetWaterStatus() == 0) {
			GD.Print("KYS! im mining bittcoin now! :3");
			this.isDead = true;
		}

	}

	public float GetWaterStatus(){
		return waterStatus;
	}

	public void WaterPlant(){
		waterStatus += wateringImpact;
		if (GetWaterStatus() >= 1) {
			GD.Print("KYS! Dont kill the plant idio!");
			this.isDead = true;
		}
	}

	public void NextStage(){
		GD.PushError("KYS!!");
	}

	public void CheckForGrowth(){
		if (currentGrowProgress >= growDurationInDays){
			NextStage();
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
