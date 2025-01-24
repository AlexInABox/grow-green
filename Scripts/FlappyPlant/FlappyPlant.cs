using Godot;

public partial class FlappyPlant : CharacterBody2D
{
    private const float Gravity = 900.0f;
    private const float JumpForce = -350.0f;

    private AnimatedSprite2D _animatedSprite;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (_animatedSprite != null)
        {
            _animatedSprite.Play("jump");
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        
        velocity.Y += (float)delta * Gravity;
        velocity.X = 0;

        if (Input.IsActionJustPressed("ui_accept"))
        {
            velocity.Y = JumpForce;
        }

        Velocity = velocity;
        
        MoveAndSlide();
    }
}