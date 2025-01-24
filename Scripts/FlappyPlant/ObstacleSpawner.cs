using Godot;
using System;

public partial class ObstacleSpawner : Node2D
{
    [Export] private PackedScene TopObstacleScene;  
    [Export] private PackedScene BottomObstacleScene;  
    [Export] private float SpawnInterval = 3.0f;  
    [Export] private float ObstacleStartX = 2000.0f; 

    [Export] private float MinGap = 450.0f;
    [Export] private float MaxGap = 600.0f; 

    [Export] private float TopObstacleMinY = 50.0f;  
    [Export] private float TopObstacleMaxY = 400.0f; 

    private Timer spawnTimer;
    
    private int ObstacleCount = 0;
    public override void _Ready()
    {
        spawnTimer = new Timer();
        AddChild(spawnTimer);
        spawnTimer.WaitTime = SpawnInterval;
        spawnTimer.Timeout += OnSpawnTimerTimeout;
        spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        GD.Print("Spawning obstacle pair");

        if (TopObstacleScene != null && BottomObstacleScene != null)
        {
            float randomGap = (float)GD.RandRange(MinGap, MaxGap);
            
            float topObstacleY = (float)GD.RandRange(TopObstacleMinY, TopObstacleMaxY);
            
            float bottomObstacleY = topObstacleY + randomGap;
            
            ObstacleCount++;
            
            float speedIncrease = 20.0f * ObstacleCount;
            
            var topObstacleInstance = (Node2D)TopObstacleScene.Instantiate();
            topObstacleInstance.Position = new Vector2(ObstacleStartX, topObstacleY);
            
            if (topObstacleInstance is Obstacle topObstacle)
            {
                topObstacle.IncreaseSpeed(speedIncrease);  
            }

            AddChild(topObstacleInstance);
            
            var bottomObstacleInstance = (Node2D)BottomObstacleScene.Instantiate();
            bottomObstacleInstance.Position = new Vector2(ObstacleStartX, bottomObstacleY);
            
            if (bottomObstacleInstance is Obstacle bottomObstacle)
            {
                bottomObstacle.IncreaseSpeed(speedIncrease); 
            }

            AddChild(bottomObstacleInstance);
            
            AdjustSpawnInterval(speedIncrease);
        }
        else
        {
            GD.PrintErr("TopObstacleScene or BottomObstacleScene not assigned.");
        }
    }
    private void AdjustSpawnInterval(float speedIncrease)
    {
        float speedFactor = speedIncrease / 100.0f;  
        float newSpawnInterval = SpawnInterval - speedFactor;
        
        spawnTimer.WaitTime = Mathf.Max(newSpawnInterval, 0.5f);
    }
}
