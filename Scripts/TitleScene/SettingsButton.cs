using Godot;
using System;

public partial class SettingsButton : Button
{
	// Called when the node enters the scene tree for the first time.
	PackedScene audioScalerPackedScene = GD.Load<PackedScene>("res://Prefabs/audioScaler.tscn");
	public override void _Ready()
	{
		Pressed += ButtonPressedEvent;
	}
	
	private void ButtonPressedEvent()
	{
		Node confirmationPopupInstance = audioScalerPackedScene.Instantiate();
		GetParent().AddChild(confirmationPopupInstance);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
