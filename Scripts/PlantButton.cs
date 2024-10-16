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

    private void ButtonHovered()
    {
       
        float buttonXPosition = GlobalPosition.X;

        Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
        Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");
        
        statusBubbleA.Hide();
        statusBubbleB.Hide();

        
        if (buttonXPosition < 960)
        {
            statusBubbleA.Show(); 
        }
        else
        {
            statusBubbleB.Show(); 
        }
    }

    private void ButtonNotHoveredAnymore()
    {
        Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
        Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");
        statusBubbleA.Hide();
        statusBubbleB.Hide();
    }

    
    public override void _Process(double delta)
    {
    }
}