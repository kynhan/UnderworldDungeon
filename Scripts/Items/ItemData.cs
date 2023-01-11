using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

	public Item item;
	public GameObject player;
	private Inventory inv;
	private Vector2 offset;
	public int slot;
	public int itemDurability;
	public string enchantEffect = "Mundane";
	public string enchantType = "none";
	public int metalTier = 0;
	bool shouldDrag;
	bool hasDescrip;
	GameObject itemDescripObject;
	GameObject enchantDescripObject;
	GameObject metalTierDescripObject;

	KeyCode inputQuicksort;
	KeyCode inputCraftsort;
	KeyCode inputDropitem;

	void Start(){
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		itemDescripObject = GameObject.Find("ItemDescripText");
		enchantDescripObject = GameObject.Find("EnchantText");
		metalTierDescripObject = GameObject.Find("MetalTierText");
		player = GameObject.Find("Player");

		//Set Controls
		inputQuicksort = GameControlScript.control.inputQuicksort;
		inputCraftsort = GameControlScript.control.inputCraftsort;
		inputDropitem = GameControlScript.control.inputDropitem;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (item != null && !hasDescrip){
			GameControlScript.mouseOverItem = true;
			itemDescripObject.GetComponent<itemDescription>().pointerOnItem(item);
			enchantDescripObject.GetComponent<enchantDescrip>().pointerOnItem(gameObject);
			metalTierDescripObject.GetComponent<metalTierDescrip>().pointerOnItem(gameObject);
			hasDescrip = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (item != null && hasDescrip){
			GameControlScript.mouseOverItem = false;
			itemDescripObject.GetComponent<itemDescription>().pointerOffItem();
			enchantDescripObject.GetComponent<enchantDescrip>().pointerOffItem();
			metalTierDescripObject.GetComponent<metalTierDescrip>().pointerOffItem();
			hasDescrip = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (item != null && GameControlScript.hudOpen)
		{
			if(!shouldDrag){
				if(slot == 37){
					inv.finishCraft = true;
					inv.destroyCraftItems();
				}
				//-------------
				if(Input.GetKey(inputQuicksort)){
					if(slot >= 28){
						for (int i=0;i<28;i++){
							if(inv.inventoryItems[i].ID == -1){
								setToIDSlot(slot, i);
								break;
							}
						}
					}
					else{
						if((item.IsGear && !inv.HasDuplicate(item.ID)) && ((!inv.HasArmor("helmet") && item.Armor == "helmet") || (!inv.HasArmor("chestplate") && item.Armor == "chestplate") || (!inv.HasArmor("leggings") && item.Armor == "leggings") || (!inv.HasArmor("shield") && item.Armor == "shield") || (!inv.HasArmor("boots") && item.Armor == "boots") || (item.Armor == "gear"))){
							for (int i=38;i<=47;i++){
								if(inv.inventoryItems[i].ID == -1){
									setToIDSlot(slot, i);
									break;
								}
							}
						}else{
							if(inv.inventoryItems[28].ID == -1){setToIDSlot(slot, 28);}
							else if(inv.inventoryItems[29].ID == -1){setToIDSlot(slot, 29);}
							else if(inv.inventoryItems[30].ID == -1){setToIDSlot(slot, 30);}
							else if(inv.inventoryItems[31].ID == -1){setToIDSlot(slot, 31);}
							else if(inv.inventoryItems[32].ID == -1){setToIDSlot(slot, 32);}
						}
					}
				}else if(Input.GetKey(inputCraftsort)){
					if(inv.inventoryItems[33].ID == -1){setToIDSlot(slot, 33);}
					else if(inv.inventoryItems[34].ID == -1){setToIDSlot(slot, 34);}
					else if(inv.inventoryItems[35].ID == -1){setToIDSlot(slot, 35);}
					else if(inv.inventoryItems[36].ID == -1){setToIDSlot(slot, 36);}
				}else if(Input.GetKey(inputDropitem)){
					inv.currentMouseItemObj = this.gameObject;
					inv.currentMouseItem = item;
					player.GetComponent<Player>().dropTheItem();
					itemDescripObject.GetComponent<itemDescription>().pointerOffItem();
					enchantDescripObject.GetComponent<enchantDescrip>().pointerOffItem();
					metalTierDescripObject.GetComponent<metalTierDescrip>().pointerOffItem();
				}else{
					if(inv.currentMouseItemObj == null){
						offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
						this.transform.SetParent(this.transform.parent.parent);
						this.transform.position = eventData.position - offset;
						GetComponent<CanvasGroup>().blocksRaycasts = false;
						inv.currentMouseItemObj = this.gameObject;
						inv.currentMouseItem = item;
						shouldDrag = true;
					}
				}
			}
		}
	}

	void setToIDSlot(int currentID, int newID) {
		inv.inventoryItems[currentID] = new Item();
		inv.inventoryItems[newID] = item;
		slot = newID;
		this.setSlot();
	}

	void Update() {
		if(shouldDrag){
			if(item != null){
				if(GameControlScript.hudOpen){GameControlScript.mouseOverItem = true;}
				this.transform.position = (Vector2)Input.mousePosition - offset;
			}
		}
		if(Input.GetMouseButtonDown(0) && shouldDrag){
			Invoke ("setSlot", 0.1f);
			shouldDrag = false;
		}
	}

	public void setSlot() {
		inv.currentMouseItemObj = null;
		inv.currentMouseItem = null;
		this.transform.SetParent(inv.slots[slot].transform);
		this.transform.position = inv.slots[slot].transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

}
