using Godot;
using System;

public partial class AlienButton : Button
{
	private Sprite2D myAlien;
	private int characterNumber;
	private bool freaky;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		freaky = false;
		
		myAlien = GetNode<Sprite2D>("../Alien");
		string textureName = System.IO.Path.GetFileName(myAlien.Texture.ResourcePath);
		if (textureName != null) characterNumber = textureName.Split('_')[0].ToInt();
		
		FocusEntered += ButtonFocused;
		FocusExited += ButtonUnfocused;
	}

	private void ButtonFocused()
	{	
		freaky = true;
		Shader shiny = GD.Load<Shader>("res://Shaders/shiny.gdshader");
		if (shiny == null)
		{
			GD.PushError("Looser");
		}
		ShaderMaterial shinyMaterial = new ShaderMaterial();
		shinyMaterial.Shader = shiny;
		if (myAlien == null)
		{
			GD.PushError("Überlooser");
		}
		myAlien.Material = shinyMaterial;
	}

	private void ButtonUnfocused()
	{
		freaky = false;
		myAlien.Material = null;
	}

	public int CharacterNumber()
	{
		return characterNumber;
	}
	
	public bool Freaky()
	{
		return freaky;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
