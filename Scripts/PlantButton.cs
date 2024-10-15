using Godot;
using System;

public partial class PlantButton : Button
{
    Plant myPlant;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        //myPlant = GetNode<Plant>("../Plant"); //will error when sprite hasnt loaded yet. but works anyways for some reason
        MouseEntered += ButtonHovered;
        MouseExited += ButtonNotHoveredAnymore;
    }

    private void ButtonPressed()
    {
        myPlant = GetNode<Plant>("../Plant");
        myPlant.WaterPlant();
    }

    private void ButtonHovered() {
        //funky wunky
        Node2D statusBubble = GetNode<Node2D>("../statusBubble");
        statusBubble.Show();
    }
    private void ButtonNotHoveredAnymore() {
        //wittle weams
        Node2D statusBubble = GetNode<Node2D>("../statusBubble");
        statusBubble.Hide();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}