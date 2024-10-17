using Godot;
using System;
using System.Collections.Generic;

public partial class MemoryGame : Node
{
	private PackedScene prefab;
	private MemoryCardScript turnedCard;
	private Button cardButton;
	private List<string> memoryTextures;
	private int moves = 0;
	private bool cardIsFirst = true;
	private MemoryCardScript lastCard;
	private MemoryCardScript oldCard;
	private int cardCount;
	private SceneManager sceneManager;
	
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("SceneManager");
		prefab = (PackedScene)ResourceLoader.Load("res://Prefabs/MemoryCard.tscn");
		
		InitializeMemoryTextures();
		
		SpawnPrefabsOnNodes();
	}

	private void InitializeMemoryTextures()
	{
		memoryTextures = new List<string>();
		
		for (int i = 1; i <= 8; i++)
		{
			string textureName = $"memory_{i:D2}"; 
			memoryTextures.Add(textureName);
			memoryTextures.Add(textureName);
			cardCount++;
			cardCount++;
		}
	}

	public void CardIsTurned(MemoryCardScript card)
	{
		if (cardIsFirst)
		{
			if (lastCard != null && oldCard != null)
			{
				lastCard.ForceHideCard();
				oldCard.ForceHideCard();
			}
			cardIsFirst = false;
			lastCard = card;
		}
		else
		{
			if (card.GetId() == lastCard.GetId())
			{
				PairFound(card, lastCard);
				lastCard = null;

			}
			else
			{
				card.HideCard();
				lastCard.HideCard();
				oldCard = card;
			}
			cardIsFirst = true;
		}
	}

	public void PairFound(MemoryCardScript card1, MemoryCardScript card2)
	{
		card1.RemoveCard();
		card2.RemoveCard();
		cardCount--;
		cardCount--;
		
		string spriteName = $"WinWrapper/MemoryStack{16-cardCount:D2}";  
		
		Sprite2D memoryStackSprite = GetNode<Sprite2D>(spriteName);
		
		if (memoryStackSprite != null)
		{
			memoryStackSprite.Visible = true;
		}
		if (cardCount == 0)
		{
			EndGame();
		}
	}

	private void EndGame()
	{
		GD.Print(sceneManager.GetCoinCount());
		var coins = sceneManager.GetCoinCount() + 10 ;
		sceneManager.SetCoinCount(coins);
		GD.Print(sceneManager.GetCoinCount());
		
		GetTree().ChangeSceneToFile("res://Scenes/TestMemory.tscn");
	}
	private void SpawnPrefabsOnNodes()
	{
		
		string[] nodeNames = {
			"A1", "A2", "A3", "A4",
			"B1", "B2", "B3", "B4",
			"C1", "C2", "C3", "C4",
			"D1", "D2", "D3", "D4"
		};
		
		foreach (string nodeName in nodeNames)
		{
			
			Node2D targetNode = GetNode<Node2D>(nodeName);

			if (targetNode != null && prefab != null)
			{
				
				Node2D instance = (Node2D)prefab.Instantiate();

			
				targetNode.AddChild(instance);

				
				AssignTextureToInstance(instance);
			}
			else
			{
				GD.Print($"Node {nodeName} or prefab not found.");
			}
		}
	}

	private void AssignTextureToInstance(Node2D instance)
	{
		Random r = new Random();
		int rInt = r.Next(0, memoryTextures.Count - 1);

		string textureName = memoryTextures[rInt];
		memoryTextures.RemoveAt(rInt);  
		
		Sprite2D sprite = instance.GetNode<Sprite2D>("MemoryCard/MemoryFront");
		
		Texture2D texture = GD.Load<Texture2D>($"res://Textures/Minigames/Memory/{textureName}.png");
		
		if (sprite != null && texture != null)
		{
			sprite.Texture = texture;
		}
		else
		{
			GD.Print($"Error assigning texture {textureName} to prefab.");
		}
	}
	
	
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}