using Godot;

public partial class TutorialButton : Button
{	
	TitleSceneManager titleSceneManager;
	SoundPlayer soundPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		soundPlayer = (SoundPlayer)GetNode("/root/SoundPlayer");
		titleSceneManager = GetNode<TitleSceneManager>("../TitleSceneManager");
		Pressed += ButtonPressedEvent;
	}

	private void ButtonPressedEvent()
	{
		soundPlayer.PlayButtonCick();
		titleSceneManager.OpenTutorial();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
