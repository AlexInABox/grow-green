using Godot;
using System;

public partial class GameMode : Node
{
	private Player player;
	// Called when the node enters the scene tree for the first time.
	public GameMode(Player player)
	{
		this.player = player;
	}

	public override void _Ready()
	{
		
	}

	public Player GetCurrentPlayer()
	{
		return player;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
