using Godot;

public partial class TitleSceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	Player playerObject;

	BaseButton loadGameButton;
	BaseButton createGameButton;
	private AudioStreamPlayer music;
	
	public override void _Ready()
	{	
		loadGameButton = GetNode<BaseButton>("../Load Game");
		createGameButton = GetNode<BaseButton>("../New Game");
		music = GetNode<AudioStreamPlayer>("Music");

		if (db.IsThisTheFirstRun()){
			loadGameButton.Disabled = true;
			playerObject = new Player();
		} else {
			playerObject = db.GetPlayerObject();
		}
	}

	public int GetCharacterId(){
		return playerObject.characterId;
	}
	
	public void SetCharacterId(int characterId){
		playerObject.characterId = characterId;
	}

	public void SaveMyPlayerObjectAndCreateTheGame(){ //Call this to actually save the character incase this is the first time the player starts the game.
		db.UpdateSave(playerObject);
	}

	public void CreateNewGame(){
		db.CreateNewSave();

		int characterId = playerObject.characterId;
		playerObject = new Player(characterId);

		SaveMyPlayerObjectAndCreateTheGame();

		var newScenePath = "res://Scenes/MainSzene.tscn";
		GetTree().ChangeSceneToFile(newScenePath);
	}

	public void SpawnUserConfirmationPopupRegardingSaveOverwriting(){
		//ColorRect blurLayer = GetNode<ColorRect>("../BlurLayer");
		//blurLayer.Hide();
		
		PackedScene confirmationPopup = GD.Load<PackedScene>("res://Prefabs/createGame_confirmation_popup.tscn");
		var confirmationPopupInstance = confirmationPopup.Instantiate();
		GetParent().AddChild(confirmationPopupInstance);
	}

	public void RemoveSaveOverwriteConfirmationPopup(){
		Node2D confirmationPopup = GetNode<Node2D>("../CreateGameConfirmationPopup");
		confirmationPopup.QueueFree(); //byeeee :waves:

		//ColorRect blurLayer = GetNode<ColorRect>("../BlurLayer");
		//blurLayer.Show();
	}

	public override void _Process(double delta)
	{
		//IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
		//db.UpdateSave(playerObject);
		
		if (GetTree().CurrentScene.HasNode(GetPath())){
			if (!music.Playing)
			music.Play(); 
		}
	}
}
