using Godot;
using System;

public partial class Collision : Area2D
{
    SoundPlayer soundPlayer;
    private FlappyScript flappyScript;
    public override void _Ready()
    {
        soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        flappyScript = (FlappyScript)GetNode("/root/FpGame");
    }
    
    private void OnBodyEntered(Node body)
    {
        if (body.Name == "Player")
        {
            soundPlayer.PlayGameOver();
            GD.Print("Dead" + body.Name);
            flappyScript.GameEnd();
        }
    }
}