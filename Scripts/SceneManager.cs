using Godot;
using System.Collections.Generic;
using System.Threading;

public partial class SceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	Player playerObject;
	
	public override void _Ready()
	{
		playerObject = db.GetPlayerObject();
	}
	
	public int GetCoinCount(){
		return playerObject.coins;
	}

	public void SetCoinCount(int coinCount){
		playerObject.coins = coinCount;
	}

	public int GetCharacterId(){
		return playerObject.characterId;
	}
	//I decide not to provide a function that changes the characterId. I see no applicable use case.
	
	public List<Plant> GetListOfOwnedPlants() {
		return playerObject.listOfOwnedPlants;
	}
	//The list will be changed even after it has been given away. Remote changes to this list will also reflect in the playerObjects version. They are interlinked!


	public override void _Process(double delta)
	{
		//IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
		db.UpdateSave(playerObject);
	}
}
