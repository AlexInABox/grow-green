using Godot;
using System;

public partial class FlappyPlant : CharacterBody2D
{
    private const float Gravity = 400.0f;
    private const float JumpForce = -300.0f;  

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        
        velocity.Y += (float)delta * Gravity;
        velocity.X = 0;

       
        if (Input.IsActionJustPressed("ui_accept"))
        {
            velocity.Y = JumpForce;
        }

        Velocity = velocity;
        
        MoveAndSlide();
    }
}