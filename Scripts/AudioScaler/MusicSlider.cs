using Godot;
using System;

public partial class MusicSlider : HSlider
{
	AudioPlayer audioPlayer;

	public override void _Ready()
    {
		audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		this.Step = 1.0f; // Schrittweite des Sliders
		this.Value = 0.0f; // Standardwert

		// Event-Handler für die Änderung des Slider-Werts
		ValueChanged += OnSliderValueChanged;
    }
    public void OnSliderValueChanged(double value)
    {
		float myFloat = (float)value;  // explizites Casting von double zu float
		audioPlayer.SetVolume(myFloat);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
