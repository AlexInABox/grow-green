using Godot;
using System;

public partial class LoadPictures : Node
{

	Control[] pictures = new Control[5];
	int character = 2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var i = 0;
		foreach (var node in GetTree().GetNodesInGroup("Pictures"))
		{
			if (node != null)
			{
				pictures[i] = (Control)node;
				i++;
			}
		} 

		for (int o = 0; o < 5; o++)
		{
			if (o == character - 1) pictures[o].Visible = true;
			else pictures[o].Visible = false;
			GD.Print("Ich bih HUH");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
