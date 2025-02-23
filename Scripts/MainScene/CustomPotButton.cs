using System.Collections.Generic;
using Godot;

namespace GrowGreen.Scripts.MainScene;

public partial class CustomPotButton : Button
{
    private Panel potSelectPanel;
    private SceneManager sceneManager;

    public override void _Ready()
    {
        if (GetTree().GetCurrentScene().HasNode("SceneManager")){
        sceneManager = GetTree().GetCurrentScene().GetNode<SceneManager>("SceneManager");
        }
        else{
        GD.Print("SceneManager node not found!");
        }

        potSelectPanel = GetTree().GetCurrentScene().GetNode<Panel>("PotSkins/Panel");

        Pressed += OnButtonPressed;
    }

    // Wenn ein Button gedrückt wird, aktiviere den Sprite-Auswahlmodus
    private void OnButtonPressed()
    {
        potSelectPanel.Visible = false;
        BlockWateringForAllPlants();
        PrepareAllPlantsForImminentPotChange();
    }

    private void OnPotSkinApplied()
    {
        UnAlertAllPlantsForImminentPotChange();
        AllowWateringForAllPlants();
    }

    private void BlockWateringForAllPlants()
    {
        List<PlantWrapper> listOfAllPlantWrapper = sceneManager.GetListOfAllPlantWrapperInScene();
        foreach (PlantWrapper plantWrapper in listOfAllPlantWrapper)
        {
            PlantButton plantButton = plantWrapper.GetNode<PlantButton>("Button");
            plantButton.Pressed -= plantButton.ButtonWasPressed; //Unsubscribe
        }
    }

    private void PrepareAllPlantsForImminentPotChange()
    {
        List<PlantWrapper> listOfAllPlantWrapper = sceneManager.GetListOfAllPlantWrapperInScene();
        foreach (PlantWrapper plantWrapper in listOfAllPlantWrapper)
        {
            PlantButton plantButton = plantWrapper.GetNode<PlantButton>("Button");
            plantWrapper.proposedPotSkin = Name;
            plantButton.Pressed += plantWrapper.ChangeToProposedPotSkin;
            plantButton.Pressed += OnPotSkinApplied;
        }
    }

    private void UnAlertAllPlantsForImminentPotChange()
    {
        List<PlantWrapper> listOfAllPlantWrapper = sceneManager.GetListOfAllPlantWrapperInScene();
        foreach (PlantWrapper plantWrapper in listOfAllPlantWrapper)
        {
            PlantButton plantButton = plantWrapper.GetNode<PlantButton>("Button");
            plantButton.Pressed -= plantWrapper.ChangeToProposedPotSkin;
            plantButton.Pressed -= OnPotSkinApplied;
        }
    }

    private void AllowWateringForAllPlants()
    {
        List<PlantWrapper> listOfAllPlantWrapper = sceneManager.GetListOfAllPlantWrapperInScene();
        foreach (PlantWrapper plantWrapper in listOfAllPlantWrapper)
        {
            PlantButton plantButton = plantWrapper.GetNode<PlantButton>("Button");
            plantButton.Pressed += plantButton.ButtonWasPressed;
        }
    }
}