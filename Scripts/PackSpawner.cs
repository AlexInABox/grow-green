using Godot;
using System;
using System.Collections.Generic;


public partial class PackSpawner : Node
{
    PackedScene packPrefab = GD.Load<PackedScene>("res://Prefabs/pack_wrapper.tscn");
    private Button button;
    private Sprite2D sprite;
    private bool packPaid = false;
    private AnimationPlayer animationPlayer;
    private int cost = 6;
    private SceneManager sceneManager;
    private PackedScene packedScene;
    private Button leaveButton;
    

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("SceneManager");
        leaveButton = GetNode<Button>("LeaveButton");
        
            GD.Print("AAAAAH");
            button = GetNode<Button>("BuyPackButton");
            button.Pressed += BuyPack;
            animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer"); 
            
    }

    public void BuyPack()
    {
        if (sceneManager.GetCoinCount() >= cost)
        {
            button.Disabled = true;
            leaveButton.Disabled = true;
            sceneManager.SetCoinCount(sceneManager.GetCoinCount() - cost);
            SpawnPacks();
        }
        else
        {
            GD.Print("Not enough coins");
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
}