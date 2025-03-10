using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class PotPackSpawner : Node
{
	PackedScene packPrefab = GD.Load<PackedScene>("res://Prefabs/potPack_wrapper.tscn");
	private Button button;
	private Sprite2D sprite;
	private bool packPaid = false;
	private int cost = 17;
	private SceneManager sceneManager;
	private PackedScene packedScene;
	private Button leaveButton;
	DatabaseWrapper db = new DatabaseWrapper();
	private List<Pot> ownablePots;
	SoundPlayer soundPlayer;

	public override void _Ready()
	{
			soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
			sceneManager = GetNode<SceneManager>("SceneManager");
			leaveButton = GetNode<Button>("LeaveButton");
		
			button = GetNode<Button>("BuyPackButton");
			button.Pressed += BuyPack;
			ownablePots = db.GetListOfAllPots()
				.Where(pot => pot.cost > 1) // Sonst immer default
				.ToList();
			if (ownablePots.Count <= sceneManager.GetListOfOwnedPots().Count)
			{
			   button.Disabled = true;
			}
			AudioPlayer audioPlayer = (AudioPlayer)GetNode("/root/AudioPlayer");
			audioPlayer.PlayMinigameMusic();
			
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

	public void ResetScene()
	{
		var packWrapper = GetNode<Node2D>("SpawnedPack");
		packWrapper.QueueFree();
		if (ownablePots.Count > sceneManager.GetListOfOwnedPots().Count)
		{
			button.Disabled = false;
		}
		
		leaveButton.Disabled = false;
	}
	public override void _Process(double delta)
	{
	}
}
