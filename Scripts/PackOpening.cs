using Godot;
using System;
using System.Collections.Generic;

public partial class PackOpening : Button
{
    private AnimationPlayer animationPlayer;
    private Light2D light;

    DatabaseWrapper db = new DatabaseWrapper();
    private List<Plant> plantList;
    
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        plantList = db.GetListOfAllPlants();
        
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer"); 
        light = GetNode<Light2D>("PointLight2D"); 
        light.Visible = false; 
    }

    private void ButtonPressed()
    {
        OpenPack();
    }

    private void OpenPack()
    {
        RemovePlantSprite();

        Random random = new Random();
        int randomIndex = random.Next(plantList.Count);

        Plant randomPlant = plantList[randomIndex];
        
        PlayAnimationAndRevealPlant(randomPlant);
    }

    private void PlayAnimationAndRevealPlant(Plant plant)
    {
        DisplayPlantInPackOpening(plant);
        
        light.Visible = true;

        animationPlayer.Play("OpenPackAnimation");
        
        animationPlayer.Connect("animation_finished", new Callable(this, nameof(OnAnimationFinished)), flags: (uint)ConnectFlags.OneShot);
        
        tempPlant = plant;
    }
    
    private Plant tempPlant;

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "OpenPackAnimation")
        {
            light.Visible = false;
            
            ShowPlantName(tempPlant.plantName);
        }
    }

    private void DisplayPlantInPackOpening(Plant plant)
    {
        GD.Print("PFALANZE: " + plant.plantName);
        
        string plantTexturePath = $"res://Textures/Plants/{plant.className.ToLower()}3.png";
        Texture2D plantTexture = (Texture2D)GD.Load(plantTexturePath);

        Sprite2D plantSprite = GetNode<Sprite2D>("PlantSprite");
        plantSprite.Texture = plantTexture;
        
        plantSprite.Visible = true;
        
        Label plantNameLabel = GetNode<Label>("PlantLabel");
        plantNameLabel.Visible = false; 
    }

    private void ShowPlantName(string plantName)
    {
        Label plantNameLabel = GetNode<Label>("PlantLabel");
        plantNameLabel.Text = plantName;
        plantNameLabel.Visible = true; 
    }
    
    private void RemovePlantSprite()
    {
        Sprite2D plantSprite = GetNode<Sprite2D>("PackSprite");
        
        if (IsInstanceValid(plantSprite))
        {
            plantSprite.QueueFree(); 
        }
    }
}
