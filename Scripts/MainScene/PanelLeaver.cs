using Godot;
using System;

public partial class PanelLeaver : Button
{
	// Called when the node enters the scene tree for the first time.
	private Panel PotSelector;
	private Button PanelRemover;
	public override void _Ready()
	{
		Pressed += PotterRemover;
		PotSelector = GetNode<Panel>("../Panel");
		PanelRemover = GetNode<Button>("../PanelLeaver");
	}

	private void PotterRemover() {
		PotSelector.Visible = false;
		PanelRemover.Visible = false;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
