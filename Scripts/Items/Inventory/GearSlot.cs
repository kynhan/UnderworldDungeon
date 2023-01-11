using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GearSlot : MonoBehaviour, IPointerClickHandler {

	private Inventory inv;
	public int id;

	void Start () {
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		inv.inventoryItems.Add(new Item());
		Invoke ("addSlots", 0.1f);

	}

	void addSlots(){inv.slots.Add(gameObject);Invoke ("correctSlots", 0.11f);}

	void correctSlots() {
		inv.slotsCorrected = true;
		inv.slots[id] = gameObject;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if(inv.currentMouseItemObj != null){
			ItemData droppedItem = inv.currentMouseItemObj.GetComponent<ItemData>();
			if(inv.inventoryItems[droppedItem.slot].IsGear && !inv.HasDuplicate(inv.inventoryItems[droppedItem.slot].ID)){

				if((!inv.HasArmor("helmet") && inv.inventoryItems[droppedItem.slot].Armor == "helmet") || (!inv.HasArmor("chestplate") && inv.inventoryItems[droppedItem.slot].Armor == "chestplate") || (!inv.HasArmor("leggings") && inv.inventoryItems[droppedItem.slot].Armor == "leggings") || (!inv.HasArmor("shield") && inv.inventoryItems[droppedItem.slot].Armor == "shield") || (!inv.HasArmor("boots") && inv.inventoryItems[droppedItem.slot].Armor == "boots") || inv.inventoryItems[droppedItem.slot].Armor == "gear"){

					if(inv.inventoryItems[id].ID == -1){
						inv.inventoryItems[droppedItem.slot] = new Item();
						inv.inventoryItems[id] = droppedItem.item;
						droppedItem.slot = id;
						droppedItem.setSlot();
					}/*else{
						Transform item = this.transform.GetChild(0);
						item.GetComponent<ItemData>().slot = droppedItem.slot;
						item.transform.SetParent(inv.slots[droppedItem.slot].transform);
						item.transform.position = inv.slots[droppedItem.slot].transform.position;
						
						droppedItem.slot = id;
						droppedItem.transform.SetParent(transform);
						droppedItem.transform.position = this.transform.position;
						
						inv.inventoryItems[droppedItem.slot] = item.GetComponent<ItemData>().item;
						inv.inventoryItems[id] = droppedItem.item;
						droppedItem.setSlot();
					}*/
				}
			}
			GameControlScript.mouseOverItem = false;
		}
	}
}
