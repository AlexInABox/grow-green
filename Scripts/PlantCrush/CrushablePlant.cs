using System;
using System.Collections.Generic;
using Godot;

public partial class CrushablePlant : Button
{
    private PlantCrushGame game;
    private int id;
    private Sprite2D sprite;
    private List<string> memoryTextures;
    public override void _Ready()
    {
        game = (PlantCrushGame)GetNode("/root/PlantCrush");
        Pressed += ButtonPressed;
        sprite = (Sprite2D)GetNode("../Sprite2D");
        memoryTextures = new List<string>();
		
        for (int i = 1; i <= 12; i++)
        {
            string textureNames = $"memory_{i:D2}"; 
            memoryTextures.Add(textureNames);
        }
        Random r = new Random();
        
        int rInt = r.Next(0, memoryTextures.Count - 1);

        string textureName = memoryTextures[rInt];
        sprite.Texture = GD.Load<Texture2D>($"res://Textures/Minigames/Memory/{textureName}.png");
    }

    public Sprite2D GetSprite()
    {
        return sprite;
    }
    public void ButtonPressed()
    {
      //  game.SelectPlant(this);
    }

    public override void _Process(double delta)
    {
    }
}