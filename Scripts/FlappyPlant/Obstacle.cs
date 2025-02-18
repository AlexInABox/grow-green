using System;
using Godot;
public partial class Obstacle : Node2D
{
    [Export] private float Speed = 300.0f; 
   
    public float IncreaseSpeed(float SpeedIncreaseRate)
    {
        Speed += SpeedIncreaseRate;
        return Speed;
    }

    public override void _Process(double delta)
    {
        Position -= new Vector2((float)(Speed * delta), 0);
        
        if (Position.X < -200)
        {
            QueueFree();
        }
    }
}