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
    

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("SceneManager");
        
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

            // Reset and Play the animation from the start.
            animationPlayer.Stop();  // Ensure the animation is stopped before playing
            animationPlayer.Play("spawn_packAnimation");

            // Connect signal if not already connected
            if (!animationPlayer.IsConnected("animation_finished", new Callable(this, nameof(OnAnimationFinished))))
            {
                animationPlayer.Connect("animation_finished", new Callable(this, nameof(OnAnimationFinished)));
            }

            // Reduce coin count
            sceneManager.SetCoinCount(sceneManager.GetCoinCount() - cost);
        }
        else
        {
            GD.Print("Not enough coins");
        }
    }



    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "spawn_packAnimation")
        {
            SpawnPacks();

            sprite = GetNode<Sprite2D>("Sprite2D");
            sprite.Visible = false;

            // Reset or clean up any other state if necessary
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
    }
}