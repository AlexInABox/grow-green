using Godot;
using System;

public partial class SoundPlayer : AudioStreamPlayer
{
	private static readonly AudioStream buttonClick = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Button.mp3");
	private static readonly AudioStream alien = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/CharacterSelect.mp3");
	private static readonly AudioStream buy = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Buy.mp3");
	private static readonly AudioStream eating = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Eating.mp3");
	private static readonly AudioStream gameOver = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/game_over.mp3");
	private static readonly AudioStream jump = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Jump.mp3");
	private static readonly AudioStream memoryWin = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/MemoryFinish.mp3");
	private static readonly AudioStream move = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/PlantMove.mp3");
	private static readonly AudioStream trash = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Trash.mp3");
	private static readonly AudioStream dry = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Die.mp3");
	private static readonly AudioStream water = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Water.mp3");
	private static readonly AudioStream overwater = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Water2.mp3");
	private static readonly AudioStream flip = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Flip.mp3");	
	private static readonly AudioStream pack = (AudioStream) GD.Load("res://Sounds/Effects/Buttons/Pack.mp3");
	private static readonly AudioStream boom = (AudioStream) GD.Load("res://Sounds/Effects/Easteregg.mp3");

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
	public void PlayBuy()
	{
		_play_sound(buy);
	}
	public void PlayEating()
	{
		_play_sound(eating);
	}
	public void PlayGameOver()
	{
		_play_sound(gameOver);
	}
	public void PlayJump()
	{
		_play_sound(jump);
	}
	public void PlayMemoryWin()
	{
		_play_sound(memoryWin);
	}
	public void PlayMove()
	{
		_play_sound(move);
	}
	public void PlayTrash()
	{
		_play_sound(trash);
	}
	public void PlayDry()
	{
		_play_sound(dry);
	}
	public void PlayWater()
	{
		_play_sound(water);
	}
	public void PlayOverwater()
	{
		_play_sound(overwater);
	}
	public void PlayFlip()
	{
		_play_sound(flip);
	}
	public void PlayPack()
	{
		_play_sound(pack);
	}
	public void PlayBoom()
	{
		_play_sound(boom);
	}
}
