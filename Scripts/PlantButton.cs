using Godot;
using System;

public partial class PlantButton : Button
{
    Plant myPlant;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        myPlant = GetNode<Plant>("../Plant"); //will error when sprite hasnt loaded yet. but works anyways for some reason
    }

    private void ButtonPressed()
    {
        //plantSprite = GetNode<Sprite2D>("../Plant");
        myPlant.WaterPlant();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}