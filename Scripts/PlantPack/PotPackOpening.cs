using Godot;
using System;
using System.Collections.Generic;

public partial class PotPackOpening : Button
{
    private AnimationPlayer animationPlayer;
    private Light2D light;
    private SceneManager sceneManager;

    DatabaseWrapper db = new DatabaseWrapper();
    private List<Pot> potList;
    private Pot tempPot;
    private PotPackSpawner packSpawner;
    private bool packIsOpen = false;

    
    public override void _Ready()
    {
        Pressed += ButtonPressed;
        potList = db.GetListOfAllPots();
        
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer"); 
        light = GetNode<Light2D>("PointLight2D"); 
        light.Visible = false; 
        
        sceneManager = GetNode<SceneManager>("../../SceneManager");
        packSpawner = GetNode<PotPackSpawner>("../../../PotPackOpeningMinigame");
    }

    private void ButtonPressed()
    {
        if (packIsOpen == false)
        {
            packIsOpen = true;
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
        int randomIndex = random.Next(potList.Count);

        Pot randomPot = potList[randomIndex];
        
        PlayAnimationAndRevealPot(randomPot);
    }

    private void PlayAnimationAndRevealPot(Pot pot)
    {
        DisplayPotInPackOpening(pot);
        
        light.Visible = true;

        animationPlayer.Play("OpenPackAnimation");
        
        animationPlayer.Connect("animation_finished", new Callable(this, nameof(OnAnimationFinished)), flags: (uint)ConnectFlags.OneShot);
        
        tempPot = pot;
    }
    
    

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "OpenPackAnimation")
        {
            light.Visible = false;
        }
    }

    private void DisplayPotInPackOpening(Pot pot)
    {
        GD.Print("POTTTTTTT: " + pot.potName);
        
        string potTexturePath = $"res://Textures/Pots/{pot.potName}.png";
        Texture2D potTexture = (Texture2D)GD.Load(potTexturePath);

        Sprite2D potSprite = GetNode<Sprite2D>("PotSprite");
        potSprite.Texture = potTexture;
        
        potSprite.Visible = true;
    }
    
    private void RemovePlantSprite()
    {
        Sprite2D plantSprite = GetNode<Sprite2D>("PackSprite");
        
        if (IsInstanceValid(plantSprite))
        {
            plantSprite.QueueFree(); 
        }
    }

    public void resetScene()
    {
        packSpawner.resetScene();
    }
}