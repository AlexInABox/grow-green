using Godot;
using System;

public partial class BackgroundScroll : Node2D
{
    [Export] private float ScrollSpeed = 100.0f;
    [Export] private float ResetPositionX = -512.0f;
    [Export] private float ResetOffsetX = 1024.0f; 

    public override void _Process(double delta)
    {
        foreach (Node child in GetChildren())
        {
            if (child is Sprite2D background)
            {
                background.Position -= new Vector2((float)(ScrollSpeed * delta), 0);
                
                if (background.Position.X < ResetPositionX)
                {
                    background.Position += new Vector2(ResetOffsetX, 0);
                }
            }
        }
    }
}