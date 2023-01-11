using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IPointerClickHandler {

	private Inventory inv;
	public int id;
	public bool disabledSlot;
	string finalMaterials;
	ItemDatabase database;

	void Start () {
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		database = GameObject.Find ("Inventory").GetComponent<ItemDatabase>();
		inv.inventoryItems.Add(new Item());
		Invoke ("addSlots", 0.2f);
		
	}
	
	void addSlots(){inv.slots.Add(gameObject);Invoke ("correctSlots", 0.1f);}
	
	void correctSlots() {
		inv.slotsCorrected = true;
		inv.slots[id] = gameObject;
	}
	
	void Update() {
		
	}
	
	public void OnPointerClick (PointerEventData eventData)
	{
		if(inv.currentMouseItemObj != null && !disabledSlot){
			ItemData droppedItem = inv.currentMouseItemObj.GetComponent<ItemData>();

			if(inv.inventoryItems[id].ID == -1){
				inv.inventoryItems[droppedItem.slot] = new Item();
				inv.inventoryItems[id] = droppedItem.item;
				droppedItem.slot = id;
				droppedItem.setSlot();
			}
			GameControlScript.mouseOverItem = false;
		}
	}

}
