using Godot;
using System;

public partial class PackSpawner : Node
{
    PackedScene packPrefab = GD.Load<PackedScene>("res://Prefabs/pack_wrapper.tscn");

    public override void _Ready()
    {
        SpawnPacks(); 
    }

    private void SpawnPacks()
    {
       
        for (int i = 1; i <= 3; i++)
        {
            string spawnerName = $"PackSpawner{i}";
            SpawnPack(spawnerName); 
        }
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
            packWrapper.Scale = new Vector2(4, 4); 

            packWrapper.Rotation = prePlacedNode.Rotation; 
        }
    }
}