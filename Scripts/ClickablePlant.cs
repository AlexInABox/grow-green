using Godot;
using System;

public partial class ClickablePlant : Node2D
{
    int counter = 0;
    Plant plant = new Plant();
    Player player = new Player();
    Button plantButton;
    Sprite2D plantSprite;
    private Timer growthTimer; // Timer for plant growth

    // Tracks the current growth stage
    private int growthStage = 0;

    // Array of growth stage textures
    private string[] growthTextures = {
        "res://Textures/Plants/agave1.png",         // Stage 1: Seed
        "res://Textures/Plants/agave2.png",       // Stage 2: Sprout
        "res://Textures/Plants/agave3.png",  // Stage 3: Young Plant
    };

    public override void _Ready()
    {
        // Get the Button and Sprite2D nodes from the scene
        plantButton = GetNode<Button>("Button");
        plantSprite = GetNode<Sprite2D>("Plant");
        plantButton.Pressed += ButtonPressed; // Connect the button signal

        // Initialize plant and player properties
        plant.waterStatus = 50;
        plant.wateringImpact = 8;
        plant.decayRate = 2;
        player.coins = 0;

        // Update the button text with water status
        plantButton.Text = plant.GetWaterStatus().ToString();

        // Create and configure the growth timer
        growthTimer = new Timer();
        growthTimer.WaitTime = 1.0f; // Change every 5 seconds
        growthTimer.Autostart = true; // Automatically start the timer
        growthTimer.OneShot = false; // Repeat the timer
        growthTimer.Timeout += OnGrowthTimerTimeout; // Connect timer timeout signal
        AddChild(growthTimer); // Add the timer as a child of this node
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
                ChangePlantTexture("res://Textures/Plants/verfault.png");
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

    private void ChangePlantTexture(string texturePath)
    {
        // Load the new texture
        Texture2D newTexture = (Texture2D)GD.Load(texturePath);
        
        // Assign the new texture to the Sprite2D
        if (newTexture != null && plantSprite != null)
        {
            plantSprite.Texture = newTexture;
        }
        else
        {
            GD.Print("Failed to load texture or find Sprite2D.");
        }
    }

    private void OnGrowthTimerTimeout()
    {
        // Grow the plant by changing its texture
        if (growthStage < growthTextures.Length)
        {
            ChangePlantTexture(growthTextures[growthStage]);
            growthStage++; // Advance to the next growth stage
        }
        else
        {
            // Stop the timer if the plant is fully grown
            growthTimer.Stop();
        }
    }

    public override void _Process(double delta)
    {
    }
}
