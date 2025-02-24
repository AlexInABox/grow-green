using Godot;
using System;

public partial class StartGameButton : Button
{
	SoundPlayer soundPlayer;
   
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
        Pressed += ButtonPressedEvent;
    }

    private void ButtonPressedEvent()
    {
        soundPlayer.PlayButtonCick();
        var creditsScene = "res://Scenes/FlappyPlant/FPGame.tscn";
        GetTree().ChangeSceneToFile(creditsScene);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}