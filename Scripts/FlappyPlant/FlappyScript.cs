using Godot;
using System;


public partial class FlappyScript : Node
{

	private int score = 0;
	private Label scoreLabel;
	private SceneManager sceneManager;
	public override void _Ready()
	{
		AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		audioPlayer.PlayFlappyMusic();
		sceneManager = GetNode<SceneManager>("SceneManager");
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
		if (sceneManager.GetFlappyPlantHighscore() < score)
		{
			sceneManager.SetFlappyPlantHighscore(score);
		}
		sceneManager.UpdateSaveBlocking();
		PackedScene endgamePopup = GD.Load<PackedScene>("res://Prefabs/endFpGame.tscn");
		var unlockPopupInstance = endgamePopup.Instantiate();
		GetParent().AddChild(unlockPopupInstance);
		var scoreLabel = unlockPopupInstance.GetNode<Label>("Sprite2D/ScoreLabel");
		scoreLabel.Text = score.ToString();
		GetTree().ChangeSceneToFile("res://Scenes/DeathScene.tscn");
	}
	public override void _Process(double delta)
	{
	}
}
