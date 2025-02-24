using Godot;
using System;

public partial class PotSkins : Button
{
	// Called when the node enters the scene tree for the first time.
	private Panel PotSelector;
	private Button PanelRemover;
	SoundPlayer soundPlayer;
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		Pressed += Potter;
		PotSelector = GetNode<Panel>("Panel");
		PanelRemover = GetNode<Button>("Panel/PanelLeaver");
	}

	private void Potter() {
		soundPlayer.PlayButtonCick();
		PotSelector.Visible = true;
		PanelRemover.Visible = true;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
