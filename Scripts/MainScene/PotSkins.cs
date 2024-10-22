using Godot;
using System;

public partial class PotSkins : Button
{
	// Called when the node enters the scene tree for the first time.
	private Panel PotSelector;
	private Button PanelRemover;
	public override void _Ready()
	{
		Pressed += Potter;
		PotSelector = GetNode<Panel>("Panel");
		PanelRemover = GetNode<Button>("PanelLeaver");
	}

	private void Potter() {
		PotSelector.Visible = true;
		PanelRemover.Visible = true;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
