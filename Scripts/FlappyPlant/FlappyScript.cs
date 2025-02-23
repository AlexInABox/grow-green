using Godot;
using System;


public partial class FlappyScript : Node
{

	private int score = 0;
	private Label scoreLabel;
	private SceneManager sceneManager;
	private AudioStreamPlayer music;
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("SceneManager");
		music = GetNode<AudioStreamPlayer>("Music");
		scoreLabel = GetNode<Label>("ScoreLabel");
		scoreLabel.Text = "0";
	}

	public int increaseScore()
	{
		score++;
		scoreLabel.Text = score.ToString();
		return score;
	}

	public void GameEnd()
	{
		int result = score / 5;
		sceneManager.SetCoinCount(sceneManager.GetCoinCount() + result);
		sceneManager.UpdateSaveBlocking();
		GetTree().CurrentScene.QueueFree();
		PackedScene endgamePopup = GD.Load<PackedScene>("res://Prefabs/endFpGame.tscn");
		var unlockPopupInstance = endgamePopup.Instantiate();
		GetParent().AddChild(unlockPopupInstance);
		var scoreLabel = unlockPopupInstance.GetNode<Label>("Sprite2D/ScoreLabel");
		scoreLabel.Text = score.ToString();
	}
	public override void _Process(double delta)
	{
		if (!music.Playing)
		music.Play();
	}
}
