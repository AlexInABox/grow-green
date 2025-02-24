using Godot;
using System;

public partial class SnoopyAlien : Sprite2D
{
	private AudioStreamPlayer2D moveAudio;
	private SoundPlayer soundPlayer;
	private Sprite2D rabbit;

	public override void _Ready()
	{
		rabbit = GetNode<Sprite2D>("../../Vladimir/Alien");
		moveAudio = GetNode<AudioStreamPlayer2D>("Move");
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		var area1 = GetNode<Area2D>("Area2D");
		
		 area1.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
		// Überprüfen, ob das berührte Objekt das `rabbit` (Sprite2D) ist
		if (body.Name == "rabbit")
		{
			soundPlayer.PlayBoom();
		}
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
