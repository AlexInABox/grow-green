using Godot;
using System;

public partial class PowerButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
		MouseEntered += Hovered;
		ButtonDown += ButtonDowned;
		ButtonUp += ButtonUped;
	}

	private void ButtonUped()
	{
		Node2D sprite = GetNode<Node2D>("../Sprite2D");
		sprite.MoveLocalY(-5);
	}
	private void ButtonDowned()
	{
		Node2D sprite = GetNode<Node2D>("../Sprite2D");
		sprite.MoveLocalY(5);
	}
	private void ButtonPressed()
	{

		GetTree().Quit();
	}

	private void Hovered(){

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
