using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlantButton : Button
{
    Plant myPlant;
    Node2D plantWrapper;
    SceneManager sceneManager;

    bool wasMouseDown = false;
    bool isMouseDown = false;
    bool wasHovering = false;
    bool isHovering = false;
    bool dragActivated = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //sceneManager = GetNode<SceneManager>("/root/MainSzene/SceneManager");
        sceneManager = GetNode<SceneManager>("../../../../SceneManager");
        Pressed += ButtonWasPressed;
        //myPlant = GetNode<Plant>("../Plant"); //will error when sprite hasnt loaded yet. but works anyways for some reason
        MouseEntered += ButtonHovered;
        MouseExited += ButtonNotHoveredAnymore;
        plantWrapper = GetParent<Node2D>();
    }

    public void ButtonWasPressed()
    {
        myPlant = GetNode<Plant>("../Plant");
        if (wasDraging == false)
        {
            myPlant.WaterPlant();
        }
      
    }

    private void ButtonHovered()
    {
        isHovering = true;
       
        float buttonXPosition = GlobalPosition.X;

        Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
        Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");
        
        statusBubbleA.Hide();
        statusBubbleB.Hide();

        
        if (buttonXPosition < 960)
        {
            statusBubbleA.Show(); 
        }
        else
        {
            statusBubbleB.Show(); 
        }
    }

    private void ButtonNotHoveredAnymore()
    {
        isHovering = false;

        Node2D statusBubbleA = GetNode<Node2D>("../statusBubble");
        Node2D statusBubbleB = GetNode<Node2D>("../statusBubbleLeft");
        statusBubbleA.Hide();
        statusBubbleB.Hide();
    }

    
    bool queueDrag = false;
    long initialDragAttemptTimeStamp;
    bool wasDraging = false;
    public override void _Process(double delta)
    {
        isMouseDown = Input.IsMouseButtonPressed(MouseButton.Left);

        if (!isMouseDown){
            dragActivated = false;
            queueDrag = false;
            GetParent().GetParent().GetParent().GetNode<Node2D>("Trash").Hide();
        }

        if (isHovering && isMouseDown && !wasMouseDown){
            queueDrag = true;
            initialDragAttemptTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        long timeSinceDragQueueStart = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialDragAttemptTimeStamp;
        if (queueDrag && ((DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialDragAttemptTimeStamp) > 500)){
            dragActivated = true;
        }

        if (dragActivated){
            plantWrapper.GlobalPosition = GetGlobalMousePosition();
            GetParent().GetParent().GetParent().GetNode<Node2D>("Trash").Show();
        } 

        wasMouseDown = isMouseDown;

        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("../AnimationPlayer");
        if (queueDrag && !dragActivated) {
            animationPlayer.Play("Plants/visualDragHint");
        } else {
            animationPlayer.Stop();
        }

        if (wasDraging && !dragActivated){
            Node2D nearestSpawnPoint = GetNearestSpawnPoint();
            if (nearestSpawnPoint.Name == "Trash"){
                DeleteThisPlant();
                return;
            }
            SetNewSpawnPoint(nearestSpawnPoint);
        }

        wasDraging = dragActivated;
    }

    private Node2D GetNearestSpawnPoint(){
        //First gather all our needed stuffs >.<
        Vector2 ourPosition = GetParent<Node2D>().GlobalPosition;
        float shortestDistanceYet = float.MaxValue;
        Node2D nearestSpawnPoint = GetParent<Node2D>(); //worst case; its our own spawnpoint

        foreach (Node2D spawnPoint in GetParent<Node2D>().GetParent<Node2D>().GetParent().GetChildren().Cast<Node2D>()) {
            float distance = ourPosition.DistanceTo(spawnPoint.GlobalPosition);
            if (distance < shortestDistanceYet) {
                shortestDistanceYet = distance;
                nearestSpawnPoint = spawnPoint;
            }
        }

        return nearestSpawnPoint;
    }

    private void SetNewSpawnPoint(Node2D spawnPoint){
        //1. move nodes in tree to new spawnPoint
        //2. reset position to zero under new spawnPoint
        //3. set spawnPoint value in plant object
        //4. switch spawnPoint with already existing plant!
        Node2D plantAtNewSpawnPoint = spawnPoint.GetChildOrNull<Node2D>(0);
        if (plantAtNewSpawnPoint is not null){
            Node2D oldSpawnPoint = GetParent().GetParent<Node2D>();

            spawnPoint.RemoveChild(plantAtNewSpawnPoint);
            oldSpawnPoint.RemoveChild(GetParent());

            oldSpawnPoint.AddChild(plantAtNewSpawnPoint);
            spawnPoint.AddChild(GetParent());

            GetParent<Node2D>().Position = new Vector2(0,0);
            plantAtNewSpawnPoint.Position = new Vector2(0,0);

            Plant PlantB = plantAtNewSpawnPoint.GetNode<Plant>("Plant");
            Plant PlantA = GetNode<Plant>("../Plant");

            PlantA.spawnPoint = spawnPoint.Name.ToString().Remove(0, 10).ToInt();
            PlantB.spawnPoint = oldSpawnPoint.Name.ToString().Remove(0, 10).ToInt();
        } else {
            GetParent().GetParent().RemoveChild(GetParent());
            spawnPoint.AddChild(GetParent());

            GetParent<Node2D>().Position = new Vector2(0,0);

            Plant PlantA = GetNode<Plant>("../Plant");

            PlantA.spawnPoint = spawnPoint.Name.ToString().Remove(0, 10).ToInt();
        }
    }

    private void DeleteThisPlant(){
        List<Plant> listOfOwnedPlants = sceneManager.GetListOfOwnedPlants();
        listOfOwnedPlants.Remove(GetNode<Plant>("../Plant"));

        GetParent().QueueFree();
    }
}
