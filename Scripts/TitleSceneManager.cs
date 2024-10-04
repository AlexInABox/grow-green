using Godot;

public partial class TitleSceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	Player playerObject;

	BaseButton loadGameButton;
	BaseButton continueGameButton;
	
	public override void _Ready()
	{	
		loadGameButton = GetNode<BaseButton>("./Load Game");
		continueGameButton = GetNode<BaseButton>("./New Game");

		if (db.IsThisTheFirstRun()){
			GD.Print("This is their first run omg!!");
			loadGameButton.Disabled = true;
			playerObject = new Player();
		} else {
			continueGameButton.Disabled = true;
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

	public override void _Process(double delta)
	{
		//IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
		//db.UpdateSave(playerObject);
	}
}
