using Godot;
using System;

public partial class AudioPlayer : AudioStreamPlayer
{
	// Konstanten müssen einen Datentyp haben, daher wird der Typ `AudioStream` verwendet
	private static readonly AudioStreamMP3 titleMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/MainMenu.mp3");
	private static readonly AudioStreamMP3 mainMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/MainSzene.mp3");
	private static readonly AudioStreamMP3 greenMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/Greenhouse.mp3");
	private static readonly AudioStreamMP3 minigameMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/Minigames.mp3");
	private static readonly AudioStreamMP3 memoryMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/mini games/Memory.mp3");
	private static readonly AudioStreamMP3 shopMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/Shop.mp3");
	private static readonly AudioStreamMP3 flappyMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/mini games/FlappyPlant.mp3");
	private static readonly AudioStreamMP3 wormMusic = (AudioStreamMP3) GD.Load("res://Sounds/Background Music/mini games/Worm.mp3");

	// Methode zum Abspielen von Musik mit einem optionalen Lautstärkewert
	private void _play_music(AudioStream music, float volume = 0.0f)
	{
		if (Stream == music)
		{
			return;
		}
		Stream = music;
		VolumeDb = volume;
		if (music is AudioStreamMP3 mp3Stream)
		{
			mp3Stream.Loop = true;
		}
		Play();
	}
	
	public void PlayTitleMusic()
	{
		_play_music(titleMusic);
	}
	
	public void PlayMainMusic()
	{
		_play_music(mainMusic);
	}
	
	public void PlayGreenMusic()
	{
		_play_music(greenMusic);
	}
	
	public void PlayMinigameMusic()
	{
		_play_music(minigameMusic);
	}
	
	public void PlayShopMusic()
	{
		_play_music(shopMusic);
	}
	
	public void PlayMemoryMusic()
	{
		_play_music(memoryMusic);
	}
	
	public void PlayFlappyMusic()
	{
		_play_music(flappyMusic);
	}
}
