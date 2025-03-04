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

	public float musicVolume = 0.0f;

	// Methode zum Abspielen von Musik mit einem optionalen Lautstärkewert
	private void _play_music(AudioStreamMP3 music)
	{
		if (Stream == music)
		{
			return;
		}
		Stream = music;
		VolumeDb = musicVolume;

		music.Loop = true;
		
		Play();
	}

	public void SetVolume(float volume)
    {
        musicVolume = volume;
        // Optional: Hier kann auch das aktuell abgespielte Musikstück aktualisiert werden, falls gewünscht
        if (Playing)
        {
            VolumeDb = musicVolume;
        }
    }
	
	public void PlayTitleMusic()
	{
		_play_music(titleMusic);
	}

	public void PlayWormMusic()
	{
		_play_music(wormMusic);
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
