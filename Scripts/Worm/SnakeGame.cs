using Godot;
using System;
using System.Collections.Generic;

public partial class SnakeGame : Node
{
    private const int GridSize = 14;      
    private const int CellSize = 72;     
    private const float MoveInterval = 0.2f; 
    
    private readonly Vector2 FieldOffset = new Vector2(444, 36);
    
    private Vector2 _direction = Vector2.Right;
    
    private double _moveTimer = 0;
    
    private List<Vector2> _snakePositions = new List<Vector2>();
    
    [Export] public PackedScene HeadSprite;
    [Export] public PackedScene BodySprite;
    [Export] public PackedScene TailSprite;
    [Export] public PackedScene FoodSprite;  
    
    private Node2D _snakeContainer;
    private Node2D _food;          
    private Vector2 _foodPosition; 
    
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        _snakeContainer = GetNode<Node2D>("SnakeContainer");
        rng.Randomize();
        
        Vector2 startPos = new Vector2(GridSize / 2, GridSize / 2);
        _snakePositions.Add(startPos);                
        _snakePositions.Add(startPos - Vector2.Right);  
        _snakePositions.Add(startPos - 2 * Vector2.Right); 
        
        DrawSnake();
        SpawnFood();
    }

    public override void _Process(double delta)
    {
        _moveTimer += delta;
        if (_moveTimer >= MoveInterval)
        {
            MoveSnake();
            _moveTimer = 0;
        }
    }

    private void MoveSnake()
    {
        Vector2 newHead = _snakePositions[0] + _direction;
        
        if (newHead.X < 0 || newHead.X >= GridSize ||
            newHead.Y < 0 || newHead.Y >= GridSize)
        {
            GameOver();
            return;
        }
        if (_snakePositions.Contains(newHead))
        {
            GameOver();
            return;
        }
        
        if (newHead == _foodPosition)
        {
            GD.Print("Food eaten at: " + newHead);
            _snakePositions.Insert(0, newHead);
            SpawnFood();
        }
        else
        {
            _snakePositions.Insert(0, newHead);
            _snakePositions.RemoveAt(_snakePositions.Count - 1);
        }
        DrawSnake();
    }

    private void DrawSnake()
    {
        foreach (Node child in _snakeContainer.GetChildren())
        {
            if (child.Name == "Food")
                continue;
            child.QueueFree();
        }
        
        Vector2 cellCenter = new Vector2(CellSize / 2f, CellSize / 2f);
        for (int i = 0; i < _snakePositions.Count; i++)
        {
            Node2D segment;
            float rotationAngle = 0f;
            
            if (i == 0)
            {
                segment = (Node2D)HeadSprite.Instantiate();
                rotationAngle = GetRotationFromDirection(_direction);
            }
            else if (i == _snakePositions.Count - 1) 
            {
                segment = (Node2D)TailSprite.Instantiate();
                Vector2 tailDir = _snakePositions[i - 1] - _snakePositions[i];
                rotationAngle = GetRotationFromDirection(tailDir);
            }
            else 
            {
                segment = (Node2D)BodySprite.Instantiate();
                Vector2 prevDir = _snakePositions[i - 1] - _snakePositions[i];
                rotationAngle = GetRotationFromDirection(prevDir);
            }
            
            Vector2 worldPos = FieldOffset + _snakePositions[i] * CellSize + cellCenter;
            segment.Position = worldPos;
            segment.Rotation = rotationAngle;
            
            _snakeContainer.AddChild(segment);
        }
    }
    
    private float GetRotationFromDirection(Vector2 direction)
    {
        if (direction == Vector2.Up)    return 0f;           // Up: 0 rad.
        if (direction == Vector2.Right) return Mathf.Pi / 2f;  // Right: 90°.
        if (direction == Vector2.Down)  return Mathf.Pi;       // Down: 180°.
        if (direction == Vector2.Left)  return -Mathf.Pi / 2f; // Left: -90°.
        
        GD.PrintErr("Unknown direction: " + direction);
        return 0f;
    }
    
    private void SpawnFood()
    {
        if (_food != null)
        {
            _food.QueueFree();
            _food = null;
        }
        
        Vector2 pos;
        do
        {
            pos = new Vector2(rng.RandiRange(0, GridSize - 1), rng.RandiRange(0, GridSize - 1));
        } while (_snakePositions.Contains(pos));
        
        _foodPosition = pos;
        GD.Print("Spawning food at: " + pos);
        
        _food = (Node2D)FoodSprite.Instantiate();
        _food.Name = "Food";
        _food.ZIndex = 10; 

        Vector2 cellCenter = new Vector2(CellSize / 2f, CellSize / 2f);
        _food.Position = FieldOffset + pos * CellSize + cellCenter;
        _snakeContainer.AddChild(_food);
    }

    private void GameOver()
    {
        GD.Print("Game Over!");
        GetTree().ReloadCurrentScene();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (keyEvent.Keycode == Key.Up && _direction != Vector2.Down)
                _direction = Vector2.Up;
            else if (keyEvent.Keycode == Key.Down && _direction != Vector2.Up)
                _direction = Vector2.Down;
            else if (keyEvent.Keycode == Key.Left && _direction != Vector2.Right)
                _direction = Vector2.Left;
            else if (keyEvent.Keycode == Key.Right && _direction != Vector2.Left)
                _direction = Vector2.Right;
        }
    }
}
