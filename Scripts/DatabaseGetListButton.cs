using System.Collections.Generic;
using Godot;

public partial class DatabaseGetListButton : Button
{

    DatabaseWrapper db = new DatabaseWrapper();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
		GD.Print("Heres my list:");
		List<Plant> plantList = db.GetListOfAllPlants();

		foreach (var item in plantList)
		{
			GD.Print(item.className);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
