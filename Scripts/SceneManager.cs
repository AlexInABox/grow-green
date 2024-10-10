using Godot;
using System.Collections.Generic;
using System.Threading;

public partial class SceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	Player playerObject;

	public bool IAMREADY = false;
	
	public override void _Ready()
	{
		GD.Print("IM ALIVE!!");
		playerObject = db.GetPlayerObject();
		IAMREADY = true;
	}
	
	public int GetCoinCount(){
		return playerObject.coins;
	}

	public void SetCoinCount(int coinCount){
		playerObject.coins = coinCount;
	}

	public int GetCharacterId(){
		GD.Print("I HELP!!");
		return playerObject.characterId;
	}
	//I decide not to provide a function that changes the characterId. I see no applicable use case.
	
	public List<Plant> GetListOfOwnedPlants() {
		return playerObject.listOfOwnedPlants;
	}
	//The list will be changed even after it has been given away. Remote changes to this list will also reflect in the playerObjects version. They are interlinked!

	public List<Pot> GetListOfOwnedPots() {
		return playerObject.listOfOwnedPots;
	}
	
	public Pot GetPotByName(string potName){
		return db.GetPotByName(potName);
	}

	public List<Plant> GetListOfAllPlants() {
		return db.GetListOfAllPlants();
	}

	public override void _Process(double delta)
	{
		if (Engine.GetFramesDrawn() % 60 == 0) {
			//IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
			System.Threading.Thread thread = new System.Threading.Thread(() => db.UpdateSave(playerObject));
			thread.Start();
		}

		//this is cool! save the everything every 60 frames and even then do it in a different thread! so cool!
	}
}
