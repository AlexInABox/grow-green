using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

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

        Pot randomPot = GetWeightedRandomPot();

        sceneManager.GetListOfOwnedPots().Add(randomPot);
        
        PlayAnimationAndRevealPot(randomPot);
    }

    private Pot GetWeightedRandomPot()
    {
        Random random = new Random();
        
       
        List<(Pot pot, double weight)> weightedPots = potList
            .Where(pot => pot.cost > 1) // Sonst immer default
            .Select(pot => (pot, weight: 1.0 / pot.cost))
            .ToList();

       
        double totalWeight = weightedPots.Sum(p => p.weight);

       
        double randomWeight = random.NextDouble() * totalWeight;

        
        double cumulativeWeight = 0.0;
        foreach (var (pot, weight) in weightedPots)
        {
            cumulativeWeight += weight;
            if (randomWeight <= cumulativeWeight)
            {
                return pot;
            }
        }
        //Notfalllösung
        GD.Print("If this is ever printed bye bye");
        return potList[0];
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
