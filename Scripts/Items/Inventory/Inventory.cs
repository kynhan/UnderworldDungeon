using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public GameObject emptyItemEntity;
	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public bool slotsCorrected;
	public GameObject currentMouseItemObj;
	public Item currentMouseItem;

	int slotAmount;
	string finalMaterials;
	public List<Item> inventoryItems = new List<Item>();
	public List<GameObject> slots = new List<GameObject>(100);
	public List<Item> gearItems = new List<Item>(100);
	List<int> craftingMaterialsID = new List<int>();


	void Start()
	{
		slotsCorrected = false;
		database = GetComponent<ItemDatabase>();
		slotAmount = 28;
		inventoryPanel = GameObject.Find("InventoryPanel");
		slotPanel = inventoryPanel.transform.Find("SlotPanel").gameObject;
		for(int i=0;i<slotAmount;i++){
			inventoryItems.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[i].GetComponent<InventorySlot>().id = i;
			slots[i].transform.SetParent(slotPanel.transform);
			slots[i].transform.localScale = new Vector3(1,1,1);
		}
		previousList = inventoryItems[33].ID + " " + inventoryItems[34].ID + " " + inventoryItems[35].ID + " " + inventoryItems[36].ID;

		//AddItem(0);
	}

	public void AddItem(int id, string enchantEffect, string enchantType, int metalTier)
	{
		Item itemToAdd = database.FetchItemByID(id);
		for (int i=0;i<inventoryItems.Count;i++){
			if(inventoryItems[i].ID == -1){
				inventoryItems[i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData>().item = itemToAdd;
				itemObj.GetComponent<ItemData>().slot = i;
				//itemObj.GetComponent<ItemData>().itemDurability = currentDurability;
				itemObj.GetComponent<ItemData>().enchantEffect = enchantEffect;
				itemObj.GetComponent<ItemData>().enchantType = enchantType;
				itemObj.GetComponent<ItemData>().metalTier = metalTier;
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
				itemObj.transform.localScale = new Vector3(1,1,1);
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Name;
				break;
			}
		}
	}

	public void AddHotbarItem(int id, string enchantEffect, string enchantType, int metalTier)
	{
		Item itemToAdd = database.FetchItemByID(id);
		for (int i=28;i<33;i++){
			if(inventoryItems[i].ID == -1){
				inventoryItems[i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData>().item = itemToAdd;
				itemObj.GetComponent<ItemData>().slot = i;
				//itemObj.GetComponent<ItemData>().itemDurability = currentDurability;
				itemObj.GetComponent<ItemData>().enchantEffect = enchantEffect;
				itemObj.GetComponent<ItemData>().enchantType = enchantType;
				itemObj.GetComponent<ItemData>().metalTier = metalTier;
				itemObj.transform.SetParent(slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
				itemObj.transform.localScale = new Vector3(1,1,1);
				itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Name;
				break;
			}
		}
	}

	public Item GetSelectedItem(){
		GameObject hotbar = GameObject.Find ("HotbarPanel");
		return inventoryItems[hotbar.GetComponent<HotbarSelection>().selectedSlotId];
	}

	public GameObject GetSelectedItemObj(){
		GameObject hotbar = GameObject.Find ("HotbarPanel");
		return slots[hotbar.GetComponent<HotbarSelection>().selectedSlotId].gameObject;
	}

	public void RemoveSelectedItem(){
		GameObject hotbar = GameObject.Find ("HotbarPanel");
		inventoryItems[hotbar.GetComponent<HotbarSelection>().selectedSlotId] = new Item();
	}

	public bool HasArmor(string armorType){
		bool containArmor = false;
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].Armor == armorType){
				containArmor = true;
			}
		}
		return containArmor;
	}

	public bool HasDuplicate(int id) {
		bool containDup = false;
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].ID == id){
				containDup = true;
			}
		}
		return containDup;
	}

	public Item GetArmorItem(string armorType){
		int armorSlotId = -1;
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].Armor == armorType){
				armorSlotId = i;
			}
		}
		if(armorSlotId == -1){
			return new Item();
		}else{
			return inventoryItems[armorSlotId];
		}
	}

	public GameObject GetArmorObj(string armorType){
		int armorSlotId = -1;
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].Armor == armorType){
				armorSlotId = i;
			}
		}
		if(armorSlotId == -1){
			return new GameObject();
		}else{
			return slots[armorSlotId];
		}
	}

	public List<Item> GetGearItems(){
		gearItems.Clear();
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].Armor == "gear" && !gearItems.Contains(inventoryItems[i])){
				gearItems.Add(inventoryItems[i]);
			}
		}
		return gearItems;
	}

	public bool inventoryFull() {
		for(int i = 0;i<=27;i++){
			if(inventoryItems[i].Name == null){
				return false;
			}
		}
		return true;
	}

	public bool gearFull() {
		for(int i = 38;i<=47;i++){
			if(inventoryItems[i].ID == -1){
				return false;
				break;
			}
		}
		return true;
	}


	//Crafting-----------------------------------

	string currentList;
	string previousList;
	public bool finishCraft;

	void Update() {
		//Check for craft update
		if(currentList != previousList){displayCraftProduct();previousList=currentList;}
		currentList = inventoryItems[33].ID + " " + inventoryItems[34].ID + " " + inventoryItems[35].ID + " " + inventoryItems[36].ID;

		//Decked Out Achievement
		if(gearFull() && GameControlScript.control.achUnlocked[6] == 0){
			GameControlScript.control.achUnlocked[6] = 1;
		}
	}

	public void displayCraftProduct() {
		if(inventoryItems[37].ID != -1){destroyPreviousDisplay();}
		craftingMaterialsID.Clear();

		if(inventoryItems[33].ID != -1){craftingMaterialsID.Add(inventoryItems[33].ID);}
		if(inventoryItems[34].ID != -1){craftingMaterialsID.Add(inventoryItems[34].ID);}
		if(inventoryItems[35].ID != -1){craftingMaterialsID.Add(inventoryItems[35].ID);}
		if(inventoryItems[36].ID != -1){craftingMaterialsID.Add(inventoryItems[36].ID);}

		craftingMaterialsID.Sort();
		
		Item itemToCreate = new Item();
		bool canCreateItem = false;

		if(craftingMaterialsID.Count == 1){
			finalMaterials = "" + craftingMaterialsID[0];
		}else if(craftingMaterialsID.Count == 2){
			finalMaterials = "" + craftingMaterialsID[0] + " " + craftingMaterialsID[1];
		}else if(craftingMaterialsID.Count == 3){
			finalMaterials = "" + craftingMaterialsID[0] + " " + craftingMaterialsID[1] + " " + craftingMaterialsID[2];
		}else if(craftingMaterialsID.Count == 4){
			finalMaterials = "" + craftingMaterialsID[0] + " " + craftingMaterialsID[1] + " " + craftingMaterialsID[2] + " " + craftingMaterialsID[3];
		}

		//===Numbers from low to high===

		//Iron Sword
		if(finalMaterials == "0 17 17"){itemToCreate = database.FetchItemByID(1); canCreateItem = true;}
		
		//Iron Battleaxe
		if(finalMaterials == "0 17 17 17"){itemToCreate = database.FetchItemByID(24); canCreateItem = true;}
		
		//Iron Spear
		if(finalMaterials == "0 0 17 17"){itemToCreate = database.FetchItemByID(35); canCreateItem = true;}
		
		//Iron Helmet
		if(finalMaterials == "17 17 27"){itemToCreate = database.FetchItemByID(3); canCreateItem = true;}
		
		//Iron Chestplate
		if(finalMaterials == "17 17 28"){itemToCreate = database.FetchItemByID(4); canCreateItem = true;}
		
		//Iron Leggings
		if(finalMaterials == "17 17 29"){itemToCreate = database.FetchItemByID(5); canCreateItem = true;}

		//Iron Shield
		if(finalMaterials == "6 17 17"){itemToCreate = database.FetchItemByID(50); canCreateItem = true;}

		
		//Gold Sword
		if(finalMaterials == "1 18 18"){itemToCreate = database.FetchItemByID(39); canCreateItem = true;}
		
		//Gold Battleaxe
		if(finalMaterials == "18 18 24"){itemToCreate = database.FetchItemByID(45); canCreateItem = true;}
		
		//Gold Spear
		if(finalMaterials == "18 18 35"){itemToCreate = database.FetchItemByID(47); canCreateItem = true;}
		
		//Gold Helmet
		if(finalMaterials == "3 18 18"){itemToCreate = database.FetchItemByID(13); canCreateItem = true;}
		
		//Gold Chestplate
		if(finalMaterials == "4 18 18"){itemToCreate = database.FetchItemByID(14); canCreateItem = true;}
		
		//Gold Leggings
		if(finalMaterials == "5 18 18"){itemToCreate = database.FetchItemByID(15); canCreateItem = true;}
		
		//Gold Shield
		if(finalMaterials == "18 18 50"){itemToCreate = database.FetchItemByID(51); canCreateItem = true;}


		//Dark Sword
		if(finalMaterials == "19 19 39"){itemToCreate = database.FetchItemByID(11); canCreateItem = true;}
		
		//Dark Battleaxe
		if(finalMaterials == "19 19 45"){itemToCreate = database.FetchItemByID(46); canCreateItem = true;}
		
		//Dark Spear
		if(finalMaterials == "19 19 47"){itemToCreate = database.FetchItemByID(48); canCreateItem = true;}
		
		//Dark Helmet
		if(finalMaterials == "13 19 19"){itemToCreate = database.FetchItemByID(20); canCreateItem = true;}
		
		//Dark Chestplate
		if(finalMaterials == "14 19 19"){itemToCreate = database.FetchItemByID(21); canCreateItem = true;}
		
		//Dark Leggings
		if(finalMaterials == "15 19 19"){itemToCreate = database.FetchItemByID(22); canCreateItem = true;}

		//Dark Shield
		if(finalMaterials == "19 19 51"){itemToCreate = database.FetchItemByID(52); canCreateItem = true;}


		if(GameControlScript.control.achUnlocked[0] == 1){
			//Bone Helmet
			if(finalMaterials == "26 26 27"){itemToCreate = database.FetchItemByID(8); canCreateItem = true;}
			
			//Bone Chestplate
			if(finalMaterials == "26 26 28"){itemToCreate = database.FetchItemByID(9); canCreateItem = true;}
			
			//Bone Leggings
			if(finalMaterials == "26 26 29"){itemToCreate = database.FetchItemByID(10); canCreateItem = true;}
		}
		
		//-------------------------
		
		
		//Golden Charm
		if(finalMaterials == "18 31"){itemToCreate = database.FetchItemByID(32); canCreateItem = true;}
		
		//Nature Charm
		if(finalMaterials == "0 31 37"){itemToCreate = database.FetchItemByID(38); canCreateItem = true;}
		
		//Bone Charm
		if(finalMaterials == "26 31"){itemToCreate = database.FetchItemByID(57); canCreateItem = true;}

		//Golden Skull
		if(finalMaterials == "8 18"){itemToCreate = database.FetchItemByID(40); canCreateItem = true;}
		
		//Boots of Hephaestus
		if(finalMaterials == "12 16"){itemToCreate = database.FetchItemByID(34); canCreateItem = true;}

		//Boreas Boots
		if(finalMaterials == "12 43"){itemToCreate = database.FetchItemByID(44); canCreateItem = true;}

		//Winter Shield
		if(finalMaterials == "6 43 43"){itemToCreate = database.FetchItemByID(33); canCreateItem = true;}
		
		//Mystic Crown
		if(finalMaterials == "17 18 19 41"){itemToCreate = database.FetchItemByID(42); canCreateItem = true;}

		//Dark Ares Sword
		if(finalMaterials == "19 19 19 66"){itemToCreate = database.FetchItemByID(67); canCreateItem = true;}
		
		if(canCreateItem && !finishCraft){
			inventoryItems[37] = itemToCreate;
			GameObject itemObj = Instantiate(inventoryItem);
			GameControlScript.control.unlockedItems[itemToCreate.ID] = 1;
			itemObj.GetComponent<ItemData>().item = itemToCreate;
			itemObj.GetComponent<ItemData>().slot = 37;

			//Enchanting Effects
			int n = itemToCreate.ID;
			if(n == 1 || n == 11 || n == 39 || n == 66 || n == 67 || n == 24 || n == 45 || n == 46 || n == 35 || n == 47 || n == 48){
				itemObj.GetComponent<ItemData>().enchantType = "sword";
			}else if(n == 3 || n == 4 || n == 5 || n == 8 || n == 9 || n == 10 || n == 13 || n == 14 || n == 15 || n == 20 || n == 21 || n == 22 || n == 27 || n == 28 || n == 29){
				itemObj.GetComponent<ItemData>().enchantType = "armor";
			}

			itemObj.transform.SetParent(slots[37].transform);
			itemObj.transform.position = Vector2.zero;
			itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
			itemObj.transform.localScale = new Vector3(1,1,1);
			itemObj.GetComponent<Image>().sprite = itemToCreate.Sprite;
			itemObj.name = itemToCreate.Name;
		}
	}

	public void destroyCraftItems(){
		if(inventoryItems[33].ID != -1){inventoryItems[33] = new Item();Destroy(slots[33].transform.GetChild(0).gameObject);}
		if(inventoryItems[34].ID != -1){inventoryItems[34] = new Item();Destroy(slots[34].transform.GetChild(0).gameObject);}
		if(inventoryItems[35].ID != -1){inventoryItems[35] = new Item();Destroy(slots[35].transform.GetChild(0).gameObject);}
		if(inventoryItems[36].ID != -1){inventoryItems[36] = new Item();Destroy(slots[36].transform.GetChild(0).gameObject);}
		Invoke ("resetCrafting", 0.1f);
	}

	void resetCrafting() {
		finishCraft = false;
	}

	void destroyPreviousDisplay() {
		if(slots[37].transform.childCount != 0){
			inventoryItems[37] = new Item();Destroy(slots[37].transform.GetChild(0).gameObject);
			finishCraft = false;
		}
	}
}
