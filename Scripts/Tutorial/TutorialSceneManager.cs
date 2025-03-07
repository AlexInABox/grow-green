using Godot;
using System;
using System.Collections.Generic;

public partial class TutorialSceneManager : Node
{
	int currentTutorial = 0;
	Sprite2D[] tutorials = new Sprite2D[7];
	AnimatedSprite2D[] tutorialAnimations = new AnimatedSprite2D[7];
	Button[] navigationButtons = new Button[2];
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		audioPlayer.PlayTitleMusic();
		
		var i = 0;
		foreach (var node in GetTree().GetNodesInGroup("Tutorial"))
		{
			if (node != null)
			{
				tutorials[i] = (Sprite2D)node;
				i++;
			}
		}
		var j = 0;
		foreach (var node in GetTree().GetNodesInGroup("NavigationButtons"))
		{
			if (node != null)
			{
				navigationButtons[j] = (Button)node;
				j++;
			}
		}
		var k = 0;
		foreach (var node in GetTree().GetNodesInGroup("TutorialAnimations"))
		{
			if (node != null)
			{
				tutorialAnimations[k] = (AnimatedSprite2D)node;
				k++;
			}
		}
		
		SetTutorial(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		navigationButtons[0].Visible = currentTutorial > 0;
		navigationButtons[1].Visible = currentTutorial < tutorials.Length - 1;
	}
	
	public void LeaveTutorial(){
		string titleScene = "res://Scenes/titleScreen.tscn";
		GetTree().ChangeSceneToFile(titleScene);
	}
	
	public void PreviousTutorial(){
		currentTutorial--;
		SetTutorial(currentTutorial);
	}

	public void NextTutorial(){
		currentTutorial++;
		SetTutorial(currentTutorial);
	}
	
	private void SetTutorial(int tutorial){
		for (int i = 0; i < tutorials.Length; i++){
			if (i == tutorial){
				tutorials[i].Visible = true;
				tutorialAnimations[i].Play();
			}
			else{
				tutorials[i].Visible = false;
				tutorialAnimations[i].Stop();
			}
		}
	}
}
