using Godot;
using System;

public partial class SettingsManager: Node
{
    public bool settingsDisplayed = false;
    PackedScene audioScalerPackedScene = GD.Load<PackedScene>("res://Prefabs/audioScaler.tscn");
    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Escape))
        {
            if (!settingsDisplayed)
            {
                settingsDisplayed = true;
                Node confirmationPopupInstance = audioScalerPackedScene.Instantiate();
                GetParent().AddChild(confirmationPopupInstance);
            }
        }
    }
}
