using Godot;
using System;

public partial class AlienButton : Button
{
	private Sprite2D myAlien;
	private int characterNumber;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myAlien = GetNode<Sprite2D>("../Alien");
		string textureName = System.IO.Path.GetFileName(myAlien.Texture.ResourcePath);
		GD.Print("Texture:", textureName);
		if (textureName != null) characterNumber = textureName.Split('_')[0].ToInt();
		GD.Print("Character number:", characterNumber);
		
		FocusEntered += ButtonFocused;
		FocusExited += ButtonUnfocused;
	}

	private void ButtonFocused()
	{	
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

	public int GetCharacterNumber()
	{
		return characterNumber;
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
