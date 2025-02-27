using Godot;
using System;

public partial class Passed : Area2D
{
    
    private FlappyScript flappyGame;
    public override void _Ready()
    {
        flappyGame = GetNode<FlappyScript>("/root/FpGame");
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        
    }
    
    private void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            GD.Print("Passed" + body.Name);
            flappyGame.increaseScore();
        }
    }
}