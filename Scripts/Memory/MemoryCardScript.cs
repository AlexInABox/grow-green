using Godot;
using System;

public partial class MemoryCardScript : Button
{
	
	private Sprite2D spriteFront;
	private MemoryGame memoryGame;
	private Timer timer;
	private Node2D cardNode;
	SoundPlayer soundPlayer;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += ButtonPressed;
		spriteFront = (Sprite2D)GetNode("../MemoryFront");
		memoryGame = (MemoryGame)GetNode("../../../../../MemoryGame");
		timer = GetNode<Timer>("../Timer"); 
		timer.OneShot = true;
		timer.WaitTime = 3f;  
		timer.Timeout += OnTimeout;
		cardNode = (Node2D)GetNode("../../MemoryCard");
	}

	public String GetId()
	{
		return spriteFront.Texture.GetPath();
	}
	private void OnTimeout()
	{
		Disabled = false;
		spriteFront.Visible = false;
	}
	private void ButtonPressed()
	{
		soundPlayer.PlayFlip();
		RevealCard();
		
	}

	public void RevealCard()
	{
		spriteFront.Visible = true;
		memoryGame.CardIsTurned(this);
		Disabled = true;
	}

	public void HideCard()
	{
		timer.Start();
	}

	public void ForceHideCard()
	{
		
		spriteFront.Visible = false;
		Disabled = false;
	}

	public void RemoveCard()
	{
		
		cardNode.QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
