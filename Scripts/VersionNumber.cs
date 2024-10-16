using Godot;
using System;

public partial class VersionNumber : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string Version = ProjectSettings.GetSetting("application/config/version").ToString();
		Text = Version;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
