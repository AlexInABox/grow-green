using Godot;

public partial class PlantWrapper : Node2D
{
	// see you space cowboy!
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(GetTreeStringPretty());

        SceneManager sceneManager = GetNode<SceneManager>("../../../SceneManager");
		Plant plant = GetNode<Plant>("Plant");
		string potName = plant.pot;
		Sprite2D potObject = sceneManager.GetPotByName(potName);
		AddChild(potObject);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
