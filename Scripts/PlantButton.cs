using Godot;
using System;

public partial class PlantButton : Button
{
	Plant myPlant;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
		//myPlant = GetNode<Plant>("../Plant"); //will error when sprite hasnt loaded yet. but works anyways for some reason
		MouseEntered += ButtonHovered;
		MouseExited += ButtonNotHoveredAnymore;
	}

	private void ButtonPressed()
	{
		myPlant = GetNode<Plant>("../Plant");
		myPlant.WaterPlant();
	}

	private void ButtonHovered()
	{
		// Get the button's Y position
		float buttonXPosition = GlobalPosition.X;
		GD.Print(buttonXPosition);

		Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
		Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");

		// Hide both bubbles initially
		statusBubbleA.Hide();
		statusBubbleB.Hide();

		// Display the appropriate bubble based on the Y position
		if (buttonXPosition < 960)
		{
			statusBubbleA.Show(); // Show Bubble A
		}
		else
		{
			statusBubbleB.Show(); // Show Bubble B
		}
	}

	private void ButtonNotHoveredAnymore()
	{
		// Hide both bubbles when mouse exits
		Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
		Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");
		statusBubbleA.Hide();
		statusBubbleB.Hide();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
