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
		
		Label plantName = GetNode<Label>("statusBubble/plantName");
		Label className = GetNode<Label>("statusBubble/className");
		plantName.Text = plant.className;
		className.Text = plant.plantName;
		Label plantNameL = GetNode<Label>("statusBubbleLeft/plantName");
		Label classNameL = GetNode<Label>("statusBubbleLeft/className");
		plantNameL.Text = plant.className;
		classNameL.Text = plant.plantName;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
