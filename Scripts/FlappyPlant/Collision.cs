using Godot;
using System;

public partial class Collision : Area2D
{
    
    private FlappyScript flappyScript;
    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        flappyScript = (FlappyScript)GetNode("/root/FpGame");
    }
    
    private void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            GD.Print("Dead" + body.Name);
            flappyScript.GameEnd();
        }
    }
}