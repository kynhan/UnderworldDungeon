using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {

	public Transform invObject;
	public int itemId;
	public int itemDurability;
	public int soulCost = 0;
	public int healthCost = 0;
	public string enchantEffect = "Mundane";
	public string enchantType = "none";
	public int metalTier = 0;
	public bool specialHotbar = false;
	public bool isDropped = false;
	bool canPickup;
	bool hasEnchantEffect;
	Inventory inv;
	GameObject particleInstance;

	void Awake () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
		particleInstance = Resources.Load("Particles/EnchantingEffect") as GameObject;
	}

	void Start() {
		Invoke ("pickupTime", 0.5f);
	}

	void pickupTime() {
		canPickup = true;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider" && (canPickup || (!canPickup && !isDropped)) && !inv.inventoryFull()){
			//inv.AddItem(itemId, itemDurability);
			GameControlScript.control.unlockedItems[itemId] = 1;
			itemAchs(itemId);
			if(specialHotbar && (inv.inventoryItems[28].ID == -1 || inv.inventoryItems[29].ID == -1 || inv.inventoryItems[30].ID == -1 || inv.inventoryItems[31].ID == -1 || inv.inventoryItems[32].ID == -1)){
				inv.AddHotbarItem(itemId, enchantEffect, enchantType, metalTier);
			}else{
				inv.AddItem(itemId, enchantEffect, enchantType, metalTier);
			}
			if(GetComponent<shopItem>() != null){
				Player.souls -= GetComponent<shopItem>().itemSoulCost;
			}
			if(GetComponent<godItem>() != null){
				collider.transform.parent.GetComponent<Player>().takeDamage(GetComponent<godItem>().itemHealthCost);
				Player.maxHealth -= GetComponent<godItem>().itemHealthCost;
			}
			Destroy(gameObject);
		}
	}

	void Update() {

		if(enchantEffect != "Mundane" && !hasEnchantEffect){
			createEnchantParticles();
			hasEnchantEffect = true;
		}

	}

	void createEnchantParticles() {
		GameObject theInstance;
		theInstance = Instantiate(particleInstance, new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z), gameObject.transform.rotation) as GameObject;
		theInstance.transform.parent = transform;
	}

	void itemAchs(int id){
		if(id == 70){
			GameControlScript.control.achUnlocked[12] = 1;
		}
	}

}
