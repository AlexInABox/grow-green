using Godot;
using System;

public partial class SnoopyAlien : Sprite2D
{
	private AudioStreamPlayer2D moveAudio;

	public override void _Ready()
	{
		moveAudio = GetNode<AudioStreamPlayer2D>("Move");
	}

	public override void _Process(double delta)
	{
		float AMOUNT = 5;
		bool moving = false;

		if (Input.IsKeyPressed(Key.W))
		{
			Position += new Vector2(0, -AMOUNT);
			moving = true;
		}
		if (Input.IsKeyPressed(Key.S))
		{
			Position += new Vector2(0, AMOUNT);
			moving = true;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			Position += new Vector2(-AMOUNT, 0);
			moving = true;
		}
		if (Input.IsKeyPressed(Key.D))
		{
			Position += new Vector2(AMOUNT, 0);
			moving = true;
		}

		if (moving)
		{
			if (!moveAudio.Playing)
				moveAudio.Play();
		}
		else
		{
			moveAudio.Stop();
		}
	}
}
