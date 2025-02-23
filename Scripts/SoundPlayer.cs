using Godot;
using System;

public partial class SoundPlayer : AudioStreamPlayer
{
	private static readonly AudioStream buttonClick = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Button.mp3");
	private static readonly AudioStream alien = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/CharacterSelect.mp3");

	private void _play_sound(AudioStream music, float volume = 0.0f)
	{
		Stream = music;
		VolumeDb = volume;
		Play();
	}
	public void PlayButtonCick()
	{
		_play_sound(buttonClick);
	}
	
	public void PlayAlien()
	{
		_play_sound(alien);
	}
}
