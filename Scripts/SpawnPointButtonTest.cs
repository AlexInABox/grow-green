using Godot;
using System;

public partial class SpawnPointButtonTest : Button
{
	// Called when the node enters the scene tree for the first time.
	bool hovered = false;
	public override void _Ready()
	{
		//MouseEntered += MouseHasEntered;
		//MouseExited += MouseHasLeft;
	}

	public void MouseHasEntered() {
		hovered = true;
	}

	public void MouseHasLeft() {
		hovered = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (hovered) {
			GD.Print(GetParent<Node2D>().Name + " is being hovered!");
		}
	}
}
