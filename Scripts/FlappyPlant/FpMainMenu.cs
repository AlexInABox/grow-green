using Godot;
using System;

public partial class FpMainMenu : Node2D
{
	private AnimatedSprite2D _animatedSprite;
	private SceneManager sceneManager;
	private Label label;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("SceneManager");
		AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		audioPlayer.PlayMinigameMusic();
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		label = GetNode<Label>("HighscoreLabel");
		label.Text = "Highscore:" + sceneManager.GetFlappyPlantHighscore();
		if (_animatedSprite != null)
		{
			_animatedSprite.Play("jump");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
