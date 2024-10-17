using Godot;
using System;

public partial class MemoryCardScript : Button
{
	
	private Sprite2D spriteFront;
	private MemoryGame memoryGame;
	private Timer timer;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
		spriteFront = (Sprite2D)GetNode("../MemoryFront");
		memoryGame = (MemoryGame)GetNode("../../../../../MemoryGame");
		timer = GetNode<Timer>("../Timer"); 
		timer.OneShot = true;
		timer.WaitTime = 3f;  
		timer.Timeout += OnTimeout;
	}

	public String GetId()
	{
		return spriteFront.Texture.GetPath();
	}
	private void OnTimeout()
	{
		spriteFront.Visible = false;
	}
	private void ButtonPressed()
	{
		RevealCard();
		
	}

	public void RevealCard()
	{
		spriteFront.Visible = true;
		memoryGame.CardIsTurned(this);
	}

	public void HideCard()
	{
		timer.Start();
	}

	public void ForceHideCard()
	{
		spriteFront.Visible = false;
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
