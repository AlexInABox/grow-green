using Godot;
using GodotPlugins.Game;
using System;

public partial class LoadPictures : Node
{

	Control[] pictures = new Control[5];
	SceneManager sceneManager;
	int characterId;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../SceneManager");
		characterId = sceneManager.GetCharacterId();

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
			if (o == characterId - 1) pictures[o].Visible = true;
			else pictures[o].Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
