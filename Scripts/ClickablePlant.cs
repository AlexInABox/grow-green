using Godot;
using System;

public partial class ClickablePlant : Node2D // Change from Button to Node2D
{
    int counter = 0;
    Plant plant = new Plant();
    Player player = new Player();
    Button plantButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Get the Button node from the scene
        plantButton = GetNode<Button>("Button"); // Adjust the path if necessary
        plantButton.Pressed += ButtonPressed; // Connect the button signal

        // Initialize plant and player properties
        plant.waterStatus = 50;
        plant.wateringImpact = 8;
        plant.decayRate = 2;
        player.coins = 0;

        // Update the button text with water status
        plantButton.Text = plant.GetWaterStatus().ToString();
    }

    private void ButtonPressed()
    {
        if (!plant.CheckForDead())
        {
            plant.WaterPlant();
            plantButton.Text = plant.GetWaterStatus().ToString();
            
            // Check again if the plant is dead after watering
            if (plant.CheckForDead())
            {
                plantButton.Text = "x"; // Show plant is dead
            }
        }
        else
        {
            // Plant is dead, reward the player and remove the node
            player.coins += 100; // Add 100 coins
            GD.Print("Plant is dead. Player awarded 100 coins.");
            QueueFree(); // Remove the entire plant scene
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}