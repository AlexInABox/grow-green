using Godot;
using System;

public partial class ShopPlantButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MouseEntered += ButtonHovered;
		MouseExited += ButtonNotHovered;
	}

	private void ButtonHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Show();
	}

	private void ButtonNotHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
