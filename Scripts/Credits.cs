using Godot;
using System;

public partial class Credits : Node2D
{
	private AudioStreamPlayer music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer>("Music");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GetTree().CurrentScene.HasNode(GetPath())){
			if (!music.Playing)
			music.Play(); 
		}
	}
}
