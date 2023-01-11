using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler {

	public int id;
	private Inventory inv;

	void Start(){
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if(inv.currentMouseItemObj != null){
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
