using Godot;
using System;

public partial class SoundSlider : HSlider
{
	SoundPlayer soundPlayer;

	public override void _Ready()
    {
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		this.Step = 1.0f; // Schrittweite des Sliders
		this.Value = 0.0f; // Standardwert

		// Event-Handler für die Änderung des Slider-Werts
		ValueChanged += OnSliderValueChanged;
    }
    public void OnSliderValueChanged(double value)
    {
		float myFloat = (float)value;  // explizites Casting von double zu float
		soundPlayer.SetVolume(myFloat);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
