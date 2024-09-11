using Godot;
using System;

public partial class Pot : Button
{
    PackedScene plantScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        // Load the plant scene
        plantScene = (PackedScene)GD.Load("res://Scenes/Plant.tscn"); // Replace with the actual path to your Plant scene
    }

    private void ButtonPressed()
    {
        if (plantScene != null)
        {
            // Instantiate the plant scene
            Node2D plantInstance = (Node2D)plantScene.Instantiate();

            // Set the position relative to the pot
            plantInstance.Position = this.Position; // Or adjust as needed
            plantInstance.Transform = this.GetTransform();

            // Add the plant instance as a child of the pot or its parent
            GetParent().AddChild(plantInstance);
        }
        else
        {
            GD.Print("Plant scene could not be loaded.");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}