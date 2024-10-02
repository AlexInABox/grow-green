using Godot;
using System;

public partial class CharacterSelection : ScrollContainer
{
	private int characterNumber;
	AlienButton[] alienButtons = new AlienButton[5];
	StartGameButton startGameButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var i = 0;
		foreach (var node in GetTree().GetNodesInGroup("AlienButtons"))
		{
			if (node != null)
			{
				alienButtons[i] = (AlienButton)node;
				i++;
			}
		}

		foreach (var startbutton in GetTree().GetNodesInGroup("newGameButton"))
		{
			if (startbutton != null)
			{
				startGameButton = (StartGameButton)startbutton;
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
				GD.Print("Name: ", alienButton.Name);
				GD.Print("Freaky: ", alienButton.Freaky());
				
				characterNumber = alienButton.CharacterNumber();
				startGameButton.SetCharacterNumber(characterNumber);
				GD.Print(characterNumber);
			}
		}
	}
}
