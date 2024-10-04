using Godot;
using System;

public partial class CharacterSelection : ScrollContainer
{
	private int characterNumber;
	AlienButton[] alienButtons = new AlienButton[5];
	TitleSceneManager titleScreenManager;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		titleScreenManager = GetNode<TitleSceneManager>("../../../TitleScreen");


		var i = 0;
		foreach (var node in GetTree().GetNodesInGroup("AlienButtons"))
		{
			if (node != null)
			{
				alienButtons[i] = (AlienButton)node;
				i++;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (var alienButton in alienButtons)
		{
			if (alienButton.Freaky())
			{
				characterNumber = alienButton.CharacterNumber();
				titleScreenManager.SetCharacterId(characterNumber);
			}
		}
	}
}
