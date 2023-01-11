using UnityEngine;
using System.Collections;

public class HoldItem : MonoBehaviour {

	private Inventory inv;
	private GameObject playerObj;
	public string itemType;
	public Item itemToCheck;

	GameObject itemObj; 
	bool triggerOnce;
	string currentItemName;
	string playerClass;

	void Start () {
		triggerOnce = true;
		currentItemName = null;
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		playerObj = GameObject.Find("Player");
		playerClass = GameControlScript.playerClass;

		//Load initial body part
		if(itemType != "shield" && itemType != "boot"){
			itemObj = Resources.Load("ActiveItems/" + itemType + "/" + playerClass + "_" + itemType) as GameObject;
			itemObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
			itemObj.transform.parent = gameObject.transform;
			itemObj.transform.localScale = new Vector3(1f,1f,1f);
		}
	}

	void Update() {
		if(itemType == "hand"){if(inv.GetSelectedItem()!=null)itemToCheck = inv.GetSelectedItem();}
		else if(itemType == "head"){itemToCheck = inv.GetArmorItem("helmet");}
		else if(itemType == "body"){itemToCheck = inv.GetArmorItem("chestplate");}
		else if(itemType == "leg"){itemToCheck = inv.GetArmorItem("leggings");}
		else if(itemType == "shield"){itemToCheck = inv.GetArmorItem("shield");}
		else if(itemType == "boot"){itemToCheck = inv.GetArmorItem("boots");}
		if(currentItemName != itemToCheck.Name){
			switchItem();
		}
		if(itemToCheck == null){
			Destroy(itemObj);
			switchItem();
		}

		//Sorting Order
		if(itemType == "hand"){
			if(Player.isBlocking){
				this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
			}else{
				this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
			}
		}

	}

	public void switchItem() {
		if(triggerOnce){
			if(itemObj){
				if(itemType != "hand"){
					//playerObj.GetComponent<ArmorEffects>().clearStats();
					//playerObj.GetComponent<ArmorEffects>().allEffects();
				}
				Destroy(itemObj);
			}
			Item selectedItem = itemToCheck;
			if(selectedItem.Name == null && itemType != "shield" && itemType != "boot"){
				currentItemName = null;
				itemObj = Resources.Load("ActiveItems/" + itemType + "/" + playerClass + "_" + itemType) as GameObject;
				itemObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
				itemObj.transform.parent = gameObject.transform;
				itemObj.transform.localScale = new Vector3(1f,1f,1f);
			}else{
				currentItemName = itemToCheck.Name;

				//Check if item has active sprite to hold on
				if(itemType == "hand" && !itemToCheck.HasActiveSelectable){
					itemObj = Resources.Load("ActiveItems/" + itemType + "/" + playerClass + "_" + itemType) as GameObject;
				}else{
					itemObj = Resources.Load("ActiveItems/"+itemType+"/"+currentItemName) as GameObject;

					if(itemType == "head"){ if(itemObj.GetComponent<EnchantedItem>()!=null){itemObj.GetComponent<EnchantedItem>().enchantEffect = inv.GetArmorObj("helmet").transform.GetChild(0).gameObject.GetComponent<ItemData>().enchantEffect;} }
					else if(itemType == "body"){ if(itemObj.GetComponent<EnchantedItem>()!=null){itemObj.GetComponent<EnchantedItem>().enchantEffect = inv.GetArmorObj("chestplate").transform.GetChild(0).gameObject.GetComponent<ItemData>().enchantEffect;} }
					else if(itemType == "leg"){ if(itemObj.GetComponent<EnchantedItem>()!=null){itemObj.GetComponent<EnchantedItem>().enchantEffect = inv.GetArmorObj("leggings").transform.GetChild(0).gameObject.GetComponent<ItemData>().enchantEffect;} }
					else if(itemType == "shield"){ if(itemObj!=null && inv.GetArmorObj("shield")!=null){itemObj.GetComponent<EnchantedItem>().enchantEffect = inv.GetArmorObj("shield").transform.GetChild(0).gameObject.GetComponent<ItemData>().enchantEffect;} }

					//Do armor effect
					//playerObj.GetComponent<ArmorEffects>().armorEffect(itemToCheck);
				}
				if(itemObj != null){
					itemObj = Instantiate(itemObj, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
					if(inv.GetSelectedItemObj().transform.childCount > 0){
						if(itemObj.GetComponent<EnchantedItem>()!=null){itemObj.GetComponent<EnchantedItem>().enchantEffect = inv.GetSelectedItemObj().transform.GetChild(0).gameObject.GetComponent<ItemData>().enchantEffect;}
					}
					itemObj.transform.parent = gameObject.transform;
					itemObj.transform.localScale = new Vector3(1f,1f,1f);
				}
			}
			triggerOnce = false;
			Invoke ("canTrigger", 0.1f);

		}
	}

	void canTrigger() {
		triggerOnce = true;
	}

}
