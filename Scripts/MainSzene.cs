using Godot;
using System.Collections.Generic;

	public partial class MainSzene : Node
{
	// Called when the node enters the scene tree for the first time.
	PackedScene plantPrefab = GD.Load<PackedScene>("res://Prefabs/plant_wrapper.tscn");
	SceneManager sceneManager;
	private Button greenhouseButton;
	private Button scammerGreenhouseButton;
	private AudioStreamPlayer music;
	public override void _Ready()
	{
		music = GetNode<AudioStreamPlayer>("Music");
		sceneManager = GetNode<SceneManager>("../SceneManager");
		List<Plant> plantList = sceneManager.GetListOfOwnedPlants(); 
		PlaceAllPlants(plantList);

		greenhouseButton = GetNode<Button>("../GreenhouseButton");
		scammerGreenhouseButton = GetNode<Button>("../ScamerGreenhouseButton");
		//GD.Print(sceneManager.GetHasUnlockedGreenhouse());
		
		CheckForGreenhouse();
		
	}

	private void PlaceAllPlants(List<Plant> plantList) {

		foreach(Plant plant in plantList){
			int spawnPointNumber = plant.spawnPoint;

			Node spawnPointWrapper = GetNode("../SpawnPointWrapper");
			Node2D spawnPoint = spawnPointWrapper.GetNodeOrNull<Node2D>($"SpawnPoint{spawnPointNumber}");

			if (spawnPoint is null) {
				continue;
			}
			var plantPrefabInstance = plantPrefab.Instantiate();

			spawnPoint.AddChild(plantPrefabInstance);

			var plantWrapper = spawnPoint.GetNode<Node2D>("plant_wrapper");
			plantWrapper.AddChild(plant);
		}
	}

	public void CheckForGreenhouse()
	{
		if (sceneManager.GetHasUnlockedGreenhouse())
		{
			greenhouseButton.Disabled = false;
			scammerGreenhouseButton.QueueFree();
		} else
		{
			greenhouseButton.Disabled = true;
			scammerGreenhouseButton.Visible = true;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		 if (GetTree().CurrentScene.HasNode(GetPath())){
			if (!music.Playing)
			music.Play(); 
		}
	}
}
