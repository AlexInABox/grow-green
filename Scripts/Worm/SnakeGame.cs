using Godot;
using System;
using System.Collections.Generic;

public partial class SnakeGame : Node
{
    
    private SceneManager sceneManager;
    private int _score = 0; 
    private Label _scoreLabel; 
    
    private const int GridSize = 14;      
    private const int CellSize = 72;     
    private const float MoveInterval = 0.2f; 
    
    private readonly Vector2 FieldOffset = new Vector2(444, 36);
    
    private Vector2 _direction = Vector2.Right;
    private Vector2 _nextDirection = Vector2.Right;
    private bool _inputBuffered = false;
    
    private double _moveTimer = 0;
    
    private List<Vector2> _snakePositions = new List<Vector2>();
    private List<Vector2> _prevSnakePositions = new List<Vector2>();
    
    [Export] public PackedScene HeadSprite;
    [Export] public PackedScene BodySprite;
    [Export] public PackedScene TailSprite;
    [Export] public PackedScene FoodSprite;  
    
    private Node2D _snakeContainer;
    private Node2D _foodContainer;
    private Vector2 _foodPosition;  
    
    private RandomNumberGenerator rng = new RandomNumberGenerator();
    SoundPlayer soundPlayer;


    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("SceneManager");
        soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
        _scoreLabel = GetNode<Label>("ScoreLabel"); 
        UpdateScoreLabel(); 
        
        _snakeContainer = GetNode<Node2D>("SnakeContainer");
        _foodContainer = GetNode<Node2D>("FoodContainer");
        rng.Randomize();
        
        // Initialize snake in center of grid.
        Vector2 startPos = new Vector2(GridSize / 2, GridSize / 2);
        _snakePositions.Add(startPos);                
        _snakePositions.Add(startPos - Vector2.Right);   
        _snakePositions.Add(startPos - 2 * Vector2.Right); 
        
        _prevSnakePositions = new List<Vector2>(_snakePositions);
        
        _nextDirection = _direction;
        SpawnFood();
    }

    public override void _Process(double delta)
    {
        _moveTimer += delta;
        if (_moveTimer >= MoveInterval)
        {
            _direction = _nextDirection;
            MoveSnake();
            _moveTimer = 0;
            _inputBuffered = false;
        }
        float t = (float)(_moveTimer / MoveInterval);
        DrawSnakeSmooth(t);
    }

    private void MoveSnake()
    {
        _prevSnakePositions = new List<Vector2>(_snakePositions);
        
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
            soundPlayer.PlayEating();
            GD.Print("Food eaten at: " + newHead);
            _snakePositions.Insert(0, newHead);
            _score++; 
            UpdateScoreLabel(); 
            
            foreach (Node child in _foodContainer.GetChildren())
                child.QueueFree();
            SpawnFood();
        }
        else
        {
            _snakePositions.Insert(0, newHead);
            _snakePositions.RemoveAt(_snakePositions.Count - 1);
        }
    }
    
    private void UpdateScoreLabel()
    {
        if (_scoreLabel != null)
        {
            _scoreLabel.Text = _score.ToString();
        }
    }

    private void DrawSnakeSmooth(float t)
    {
        foreach (Node child in _snakeContainer.GetChildren())
        {
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
        
            Vector2 prevWorldPos = FieldOffset + _snakePositions[i] * CellSize + cellCenter; 
            if (i < _prevSnakePositions.Count)
            {
                prevWorldPos = FieldOffset + _prevSnakePositions[i] * CellSize + cellCenter;
            }

            Vector2 currWorldPos = FieldOffset + _snakePositions[i] * CellSize + cellCenter;
            Vector2 interpPos = prevWorldPos.Lerp(currWorldPos, t);
        
            segment.Position = interpPos;
            segment.Rotation = rotationAngle;
        
            _snakeContainer.AddChild(segment);
        }
    }

    private float GetRotationFromDirection(Vector2 direction)
    {
        if (direction == Vector2.Up)    return 0f;          
        if (direction == Vector2.Right) return Mathf.Pi / 2f;  
        if (direction == Vector2.Down)  return Mathf.Pi;       
        if (direction == Vector2.Left)  return -Mathf.Pi / 2f;
        
        GD.PrintErr("Unknown direction: " + direction);
        return 0f;
    }

    private void SpawnFood()
    {
        foreach (Node child in _foodContainer.GetChildren())
            child.QueueFree();
        
        Vector2 pos;
        int attempts = 0;
        do
        {
            pos = new Vector2(rng.RandiRange(0, GridSize - 1), rng.RandiRange(0, GridSize - 1));
            attempts++;
            if (attempts > 100) break;
        } while (_snakePositions.Contains(pos));
        
        _foodPosition = pos;
        GD.Print("Spawning food at: " + pos);
        
        Node2D food = (Node2D)FoodSprite.Instantiate();
        food.Name = "Food";
        food.ZIndex = 100; 
        
        Vector2 cellCenter = new Vector2(CellSize / 2f, CellSize / 2f);
        food.Position = FieldOffset + pos * CellSize + cellCenter;
        _foodContainer.AddChild(food);
    }

    private void GameOver()
    {
        int result = _score / 4;
        sceneManager.SetCoinCount(sceneManager.GetCoinCount() + result);
        sceneManager.UpdateSaveBlocking();
        GetTree().CurrentScene.QueueFree();
        PackedScene endgamePopup = GD.Load<PackedScene>("res://Prefabs/endSnakeGame.tscn");
        var unlockPopupInstance = endgamePopup.Instantiate();
        GetParent().AddChild(unlockPopupInstance);
        var scoreLabel = unlockPopupInstance.GetNode<Label>("Sprite2D/ScoreLabel");
        scoreLabel.Text = _score.ToString();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            if (!_inputBuffered)
            {
                Vector2 newDir = _direction;
                if (keyEvent.Keycode == Key.Up && _direction != Vector2.Down)
                    newDir = Vector2.Up;
                else if (keyEvent.Keycode == Key.Down && _direction != Vector2.Up)
                    newDir = Vector2.Down;
                else if (keyEvent.Keycode == Key.Left && _direction != Vector2.Right)
                    newDir = Vector2.Left;
                else if (keyEvent.Keycode == Key.Right && _direction != Vector2.Left)
                    newDir = Vector2.Right;
                if (newDir != _direction)
                {
                    _nextDirection = newDir;
                    _inputBuffered = true;
                }
            }
        }
    }
}
