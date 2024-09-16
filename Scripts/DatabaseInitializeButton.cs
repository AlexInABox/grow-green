using Godot;

public partial class DatabaseInitializeButton : Button
{

    DatabaseWrapper db = new DatabaseWrapper();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += ButtonPressed;
	}

	private void ButtonPressed()
	{
        db.InitializeDatabase();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
