using Godot;
using System;
using System.Collections.Generic;


public partial class PackSpawner : Node
{
	PackedScene packPrefab = GD.Load<PackedScene>("res://Prefabs/pack_wrapper.tscn");
	private Button button;
	private Sprite2D sprite;
	private bool packPaid = false;
	private int cost = 6;
	private SceneManager sceneManager;
	private PackedScene packedScene;
	private Button leaveButton;
	SoundPlayer soundPlayer;
	

	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		sceneManager = GetNode<SceneManager>("SceneManager");
		leaveButton = GetNode<Button>("LeaveButton");
		AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
		audioPlayer.PlayMinigameMusic();
		
			button = GetNode<Button>("BuyPackButton");
			button.Pressed += BuyPack;
			
	}

	public void BuyPack()
	{
		soundPlayer.PlayButtonCick();
		if (sceneManager.GetCoinCount() >= cost)
		{
			button.Disabled = true;
			leaveButton.Disabled = true;
			sceneManager.SetCoinCount(sceneManager.GetCoinCount() - cost);
			SpawnPacks();
		}
		else
		{
		}
	}
	
	private void SpawnPacks()
	{
		SpawnPack("PackSpawner"); 
	}

	private void SpawnPack(string spawnerName)
	{
		var packPrefabInstance = packPrefab.Instantiate();
		AddChild(packPrefabInstance);
		
		var prePlacedNode = GetNode<Node2D>(spawnerName);
		
		var packWrapper = packPrefabInstance as Node2D;
		if (packWrapper != null && prePlacedNode != null)
		{
		   
			packWrapper.GlobalPosition = prePlacedNode.GlobalPosition; 
			packWrapper.Scale = new Vector2(8f, 8f); 
			packWrapper.Name = "SpawnedPack";

			packWrapper.Rotation = prePlacedNode.Rotation; 
		}
	}

	public void resetScene()
	{
		var packWrapper = GetNode<Node2D>("SpawnedPack");
		packWrapper.QueueFree();
		button.Disabled = false;
		leaveButton.Disabled = false;
	}
	public override void _Process(double delta)
	{
	}
}
