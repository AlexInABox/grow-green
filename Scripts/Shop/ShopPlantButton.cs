using Godot;
using System;
using System.Collections.Generic;

public partial class ShopPlantButton : Button
{
	public int cost = 0;
	public string className = "";

	SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetNode<SceneManager>("../../../../../SceneManager");
		MouseEntered += ButtonHovered;
		MouseExited += ButtonNotHovered;
		Pressed += ButtonGotPressed;
	}

	private void ButtonHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Show();
	}

	private void ButtonNotHovered() {
		Node2D statusBubble = GetNode<Node2D>("../statusBubble");
		statusBubble.Hide();
	}

	private void ButtonGotPressed() { 
		PackedScene confirmationPopup = GD.Load<PackedScene>("res://Prefabs/shopBuyConfirmation_popup.tscn");
		Node confirmationPopupInstance = confirmationPopup.Instantiate();
		ConfirmationPopup PopupScript = (ConfirmationPopup)confirmationPopupInstance;
		PopupScript.SetClassName(className);
		PopupScript.SetPrice(cost);
		PopupScript.ChangeLabel();
		GetParent().GetParent().GetParent().GetParent().GetParent().AddChild(confirmationPopupInstance);
	} 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
