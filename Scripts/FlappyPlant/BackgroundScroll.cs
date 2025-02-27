using System;
using Godot;

public partial class BackgroundScroll : Node2D
{
    [Export] private float ScrollSpeed = 100.0f; 
    [Export] private float ResetThresholdX = -3005.0f; 

    public override void _Process(double delta)
    {
        foreach (Node child in GetChildren())
        {
            if (child is Sprite2D background)
            {
                background.Position -= new Vector2((float)(ScrollSpeed * delta), 0);
                
                if (background.Position.X <= ResetThresholdX)
                {
                    background.Position = new Vector2(0, background.Position.Y);
                }
            }
        }
    }
}