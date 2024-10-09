using Godot;
using System;

public partial class Pot : Sprite2D
{
    //constants
    public string potName;
    public int cost;
    //end-of constants

    public Pot() {
        this.potName = "default";
        this.cost = 10;
    }
    public Pot(string potName, int cost) {
        this.potName = potName;
        this.cost = cost;
    }

    public override void _Ready()
    {
        Name = "Pot";
        Texture = (Texture2D)GD.Load($"res://Textures/Pots/{potName}.png");
    }

    public override void _Process(double delta)
    {
    }
}
