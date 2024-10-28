using Godot;
using System.Collections.Generic;
using System.Threading;

public partial class SceneManager : Node
{
	DatabaseWrapper db = new DatabaseWrapper();
	Player playerObject;

	public bool IAMREADY = false;
	private bool hasUnlockedGreenhouse = false;

	public void SetHasUnlockedGreenhouse(bool hasUnlockedGreenhouse)
	{
		playerObject.greenhouseUnlocked = hasUnlockedGreenhouse;
	}

	public bool GetHasUnlockedGreenhouse()
	{
		return playerObject.greenhouseUnlocked;
	}
	public override void _Ready()
	{
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
	
	public bool IsSaveLocked(){
		return db.savingLock;
	}
	public Plant GetPlantByClassName(string className){
		return db.GetPlantByClassName(className);
	}

	public Pot GetPotByName(string potName){
		return db.GetPotByName(potName);
	}

	public List<Plant> GetListOfAllPlants() {
		return db.GetListOfAllPlants();
	}

	public void UpdateSaveBlocking(){
		db.UpdateSave(playerObject);
	}

	public override void _Process(double delta)
	{
		if (Engine.GetFramesDrawn() % 1000 == 0) {
			GD.Print("Saving...");
			//IT IS MY GOD-GIVEN RIGHT TO USE A DATABASE ACCORDING TO MY WILL! IF A HUMAN BEING, LIKE ME, WANTS TO WRITE TO A DATABASE AT EVERY FRAME, THEY MUST NOT BE HINDERED BY A LESSER BEING, LIKE MY COMPUTER!!!!!!!!!
			System.Threading.Thread thread = new System.Threading.Thread(() => db.UpdateSave(playerObject));
			thread.Start();
		}

		//this is cool! save the everything every 60 frames and even then do it in a different thread! so cool!
	}

	public void SpawnGreenhouseUnlockPopup (){
		//ColorRect blurLayer = GetNode<ColorRect>("../BlurLayer");
		//blurLayer.Hide();
		
		PackedScene unlockPopup = GD.Load<PackedScene>("res://Prefabs/greenhouseUnlock_popup.tscn");
		var unlockPopupInstance = unlockPopup.Instantiate();
		GetParent().AddChild(unlockPopupInstance);
	}

	public void AddNewPlantToListOfOwnedPlants(Plant plantToAdd) { //automtically generate the spawnPoint
		List<Plant> listOfOwnedPlants = GetListOfOwnedPlants();

		List<int> listOfOccupiedSpawnPoints = new List<int>();

		foreach (Plant plant in listOfOwnedPlants){
			listOfOccupiedSpawnPoints.Add(plant.spawnPoint);
		}
		listOfOccupiedSpawnPoints.Sort();
		foreach (int spawnPoint in listOfOccupiedSpawnPoints) GD.Print(spawnPoint);
		for (int i = 1;;i++){
			if (listOfOccupiedSpawnPoints.Count < i) {
				plantToAdd.spawnPoint = i;
				break;
			}
			
			if (listOfOccupiedSpawnPoints[i-1] == i) {
				continue;
			}
			plantToAdd.spawnPoint = i;
			break;
		}
		listOfOwnedPlants.Add(plantToAdd);
	}
}
