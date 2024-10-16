using Godot;
using System;
using System.Collections.Generic;

public partial class PackOpening : Button
{
    private AnimationPlayer animationPlayer;
    private Light2D light;
    private SceneManager sceneManager;

    DatabaseWrapper db = new DatabaseWrapper();
    private List<Plant> plantList;
    private Plant tempPlant;
    private PackSpawner packSpawner;
    private bool packIsOpen = false;

    
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        plantList = db.GetListOfAllPlants();
        
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer"); 
        light = GetNode<Light2D>("PointLight2D"); 
        light.Visible = false; 
        
        sceneManager = GetNode<SceneManager>("../../SceneManager");
        packSpawner = GetNode<PackSpawner>("../../../PackOpeningMinigame");
    }

    private void ButtonPressed()
    {
        if (packIsOpen == false)
        {
            packIsOpen = true;
            this.Disabled = true;
            OpenPack();
        }
        else
        {
            resetScene();
        }
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
    
    

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "OpenPackAnimation")
        {
            light.Visible = false;
            
            ShowPlantName(tempPlant.className);
        }
    }

    private void DisplayPlantInPackOpening(Plant plant)
    {
        GD.Print("PFALANZE: " + plant.plantName);
        
        string plantTexturePath = $"res://Textures/Plants/{plant.className.ToLower().Replace(" ", "")}3.png";
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
        placePlant();
        this.Disabled = false;
    }
    
    private void RemovePlantSprite()
    {
        Sprite2D plantSprite = GetNode<Sprite2D>("PackSprite");
        
        if (IsInstanceValid(plantSprite))
        {
            plantSprite.QueueFree(); 
        }
    }

    public void placePlant()
    {
        List<Plant> ownedPlants =  sceneManager.GetListOfOwnedPlants();
        tempPlant.spawnPoint = ownedPlants.Count + 1;
        ownedPlants.Add(tempPlant);
       
    }

    public void resetScene()
    {
        packSpawner.resetScene();
    }
}
