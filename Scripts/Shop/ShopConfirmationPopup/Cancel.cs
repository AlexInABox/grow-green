using Godot;
using System;

public partial class Cancel : Button
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += Verlassen;
	}

	private void Verlassen(){
		GetParent().GetParent().QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
