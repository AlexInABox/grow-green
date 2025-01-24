using Godot;

public partial class PlantWrapper : Node2D
{
	// see you space cowboy!
	// Called when the node enters the scene tree for the first time.
	private SceneManager sceneManager;
	public string proposedPotSkin;
	public override void _Ready()
	{
		sceneManager = GetTree().Root.GetChildren()[0].GetNode<SceneManager>("SceneManager");
		Plant plant = GetNode<Plant>("Plant");
		string potName = plant.pot;
		proposedPotSkin = plant.pot;
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

	/// <summary>
	///     Changes the <see cref="Pot"/> of the <see cref="Plant"/> inside the <see cref="PlantWrapper"/>.
	/// </summary>
	/// <param name="potSkinName">The name of the new pot as a <see cref="string"/>.</param>
	public void ChangePotSkin(string potSkinName)
	{
		Node2D pot = GetNode<Node2D>("Pot");
		pot.Free();
		Sprite2D potObject = sceneManager.GetPotByName(potSkinName);
		AddChild(potObject);
		
		Plant plant = GetNode<Plant>("Plant");
		plant.pot = potSkinName;
	}
	
	/// <summary>
	///     Changes the <see cref="Pot"/> of the <see cref="Plant"/> inside the <see cref="PlantWrapper"/> to the variable <see cref="proposedPotSkin"/>.
	///		Modify that variable prior to calling this function.
	/// </summary>
	public void ChangeToProposedPotSkin()
	{
		Node2D pot = GetNode<Node2D>("Pot");
		pot.Free();
		Sprite2D potObject = sceneManager.GetPotByName(proposedPotSkin);
		AddChild(potObject);
		
		Plant plant = GetNode<Plant>("Plant");
		plant.pot = proposedPotSkin;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
