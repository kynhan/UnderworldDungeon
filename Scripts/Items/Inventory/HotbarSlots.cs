using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HotbarSlots : MonoBehaviour {

	private Inventory inv;
	private int id;
	public Sprite unselectedSlot;
	public Sprite selectedSlot;
	bool isSelected;
	public int selectedNumber;
	bool resetTrigger = false;

	KeyCode inputHotbar1;
	KeyCode inputHotbar2;
	KeyCode inputHotbar3;
	KeyCode inputHotbar4;
	KeyCode inputHotbar5;


	// Use this for initialization
	void Start () {
		selectedNumber = 1;
		id = GetComponent<InventorySlot>().id;
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		inv.inventoryItems.Add(new Item());
		inv.slots.Add(gameObject);
		Invoke ("correctSlots", 0.1f);

		//Set Controls
		inputHotbar1 = GameControlScript.control.inputHotbar1;
		inputHotbar2 = GameControlScript.control.inputHotbar2;
		inputHotbar3 = GameControlScript.control.inputHotbar3;
		inputHotbar4 = GameControlScript.control.inputHotbar4;
		inputHotbar5 = GameControlScript.control.inputHotbar5;
	}

	void correctSlots() {
		inv.slots[id] = gameObject;
	}

	void Update () {

		//Change Animation
		if(isSelected){
			transform.parent.GetComponent<HotbarSelection>().selectedSlotId = this.id;
			this.GetComponent<Image>().sprite = selectedSlot;
		}else{
			this.GetComponent<Image>().sprite = unselectedSlot;
		}

		if(!GameControlScript.control.freezeHotbar && !resetTrigger){
			if(Input.GetKeyDown(inputHotbar1)){
				selectedNumber = 1;
				resetTrigger = true;
				Invoke ("resetTheTrigger", 0.1f);
			}else if(Input.GetKeyDown(inputHotbar2)){
				selectedNumber = 2;
				resetTrigger = true;
				Invoke ("resetTheTrigger", 0.1f);
			}else if(Input.GetKeyDown(inputHotbar3)){
				selectedNumber = 3;
				resetTrigger = true;
				Invoke ("resetTheTrigger", 0.1f);
			}else if(Input.GetKeyDown(inputHotbar4)){
				selectedNumber = 4;
				resetTrigger = true;
				Invoke ("resetTheTrigger", 0.1f);
			}else if(Input.GetKeyDown(inputHotbar5)){
				selectedNumber = 5;
				resetTrigger = true;
				Invoke ("resetTheTrigger", 0.1f);
			}
		}

		if(selectedNumber == 1){
			if(this.id == 28){this.isSelected = true;}else{this.isSelected = false;}
		}else if(selectedNumber == 2){
			if(this.id == 29){this.isSelected = true;}else{this.isSelected = false;}
		}else if(selectedNumber == 3){
			if(this.id == 30){this.isSelected = true;}else{this.isSelected = false;}
		}else if(selectedNumber == 4){
			if(this.id == 31){this.isSelected = true;}else{this.isSelected = false;}
		}else if(selectedNumber == 5){
			if(this.id == 32){this.isSelected = true;}else{this.isSelected = false;}
		}
	}

	void resetTheTrigger() {
		resetTrigger = false;
	}
}
