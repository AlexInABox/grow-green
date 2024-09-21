using Godot;
using System;

public partial class AlienButton : Button
{
	CanvasItem myAlien;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myAlien = GetNode<CanvasItem>("../Alien");
		FocusEntered += ButtonFocused;
		FocusExited += ButtonUnfocused;
	}

	private void ButtonFocused()
	{
		GD.Print("Test");
		Shader shiny = GD.Load<Shader>("res://Shaders/shiny.gdshader");
		if (shiny == null)
		{
			GD.Print("Looser");
		}
		ShaderMaterial shinyMaterial = new ShaderMaterial();
		shinyMaterial.Shader = shiny;
		if (myAlien == null)
		{
			GD.Print("Überlooser");
		}
		myAlien.Material = shinyMaterial;
	}

	private void ButtonUnfocused()
	{
		myAlien.Material = null;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
