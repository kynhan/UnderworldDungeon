using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour {

	private Inventory inv;
	private HotbarSlots hotbarslots;
	public bool isHealing;
	public float destroyTime = 0f;

	void Start () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		hotbarslots = GameObject.Find("HotbarSlot1").GetComponent<HotbarSlots>();
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !GameControlScript.mouseOverItem){
			//ITEM USES
			GameControlScript.control.freezeHotbar = true;

			//Dynamite
			if(inv.GetSelectedItem()!=null && inv.GetSelectedItem().Name == "dynamite"){
				spawnDynamite();
				inv.inventoryItems[27 + hotbarslots.selectedNumber] = new Item();
				Destroy(inv.slots[27 + hotbarslots.selectedNumber].transform.GetChild(0).gameObject);
			}

			//Herb
			if(inv.GetSelectedItem()!=null && inv.GetSelectedItem().Name == "herb" && !isHealing){
				useHerb();
				inv.inventoryItems[27 + hotbarslots.selectedNumber] = new Item();
				Destroy(inv.slots[27 + hotbarslots.selectedNumber].transform.GetChild(0).gameObject);
				isHealing = true;
			}

			//Cornucopia
			if(inv.GetSelectedItem()!=null && inv.GetSelectedItem().Name == "cornucopia" && !isHealing){
				useCornucopia();
				isHealing = true;
				Invoke ("removeCornucopia", 1f);
			}

			//Nectar Bottle
			if(inv.GetSelectedItem()!=null && inv.GetSelectedItem().Name == "nectarBottle"){
				Player.maxHealth += 100;
				Player.health += 100;
				inv.inventoryItems[27 + hotbarslots.selectedNumber] = new Item();
				Destroy(inv.slots[27 + hotbarslots.selectedNumber].transform.GetChild(0).gameObject);
			}

			//Gorgon's Blood
			if(inv.GetSelectedItem()!=null && inv.GetSelectedItem().Name == "gorgonBlood"){
				float randChance = Random.value;
				if(randChance>=0.5f){
					//Good
					Player.baseSpeed += 0.2f;
					Player.damage += 2;
					Player.defense += 2;
					Player.staminaRegen += 1;
				}else{
					//Bad
					Player.health -= 100;
					Player.maxHealth -= 100;
				}
				Player.staminaRegen += 1;
				inv.inventoryItems[27 + hotbarslots.selectedNumber] = new Item();
				Destroy(inv.slots[27 + hotbarslots.selectedNumber].transform.GetChild(0).gameObject);
			}
			GameControlScript.control.freezeHotbar = false;

		}

		//Herb Particles
		if(healInstance != null){healInstance.transform.position = gameObject.transform.position;}

	}

	//Dynamite Action
	void spawnDynamite(){
		GameObject instance = Resources.Load("ActiveItems/Dynamite") as GameObject;
		Instantiate(instance, new Vector3(transform.position.x, transform.position.y - 0.2f, 0), Quaternion.identity);
	}
		

	//Herb Action
	GameObject healInstance;
	void useHerb(){
		GameObject healEffect = Resources.Load("Particles/healEffect") as GameObject;
		this.healInstance = Instantiate(healEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		InvokeRepeating("healPlayer", 0f, 0.02f);
		Invoke ("destroyHealEffect", 2f);
	}
	void healPlayer(){Player.health+=1;}
	void destroyHealEffect(){CancelInvoke("healPlayer"); Destroy(this.healInstance); isHealing = false;}


	//Cornucopia Action
	void useCornucopia() {
		GameControlScript.control.freezeHotbar = true;
		Player.wallHacks = true;
		GameObject healEffect = Resources.Load("Particles/healEffect") as GameObject;
		this.healInstance = Instantiate(healEffect, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		InvokeRepeating("healPlayer", 0f, 0.02f);
		Invoke ("destroyHealEffect", 4f);
	}
	void removeCornucopia() {
		inv.inventoryItems[27 + hotbarslots.selectedNumber] = new Item();
		Destroy(inv.slots[27 + hotbarslots.selectedNumber].transform.GetChild(0).gameObject);
		GameControlScript.control.freezeHotbar = false;
	}
}
