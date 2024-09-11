using Godot;
using System;

public partial class ClickablePlant : Button
{

	int counter = 0;
	Plant plant = new Plant();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
		plant.waterStatus = 50;
		plant.wateringImpact = 8;
		plant.decayRate = 2;
		Text = plant.GetWaterStatus().ToString();

	}

	private void ButtonPressed()
	{
		if (plant.CheckForDead() == false)
		{
			plant.WaterPlant();
			Text = plant.GetWaterStatus().ToString();
			bool killPlant = plant.CheckForDead();
			if (killPlant == true)
			{
				Text = "x";
			}

			
		}
		else
		{
			QueueFree();
			return;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}
}
