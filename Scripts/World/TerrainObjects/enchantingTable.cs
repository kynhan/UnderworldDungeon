using UnityEngine;
using System.Collections;

public class enchantingTable : MonoBehaviour {

	GameObject textObject;	
	GameObject costCanvas;
	string enchantType;
	bool isEnchanting;

	public Light theLight;
	public int soulCost;


	void Start () {
		foreach(Transform child in transform){
			if(child.name == "textObject"){
				textObject = child.gameObject;
			}
		}
		foreach(Transform child in transform){
			if(child.name == "CostCanvas"){
				costCanvas = child.gameObject;
			}
		}
		foreach(Transform child in transform){
			if(child.name == "light"){
				theLight = child.gameObject.GetComponent<Light>();
			}
		}
		textObject.transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 20;	
		soulCost = Random.Range(100, 200);
	}

	void Update () {

		if(isEnchanting){
			GetComponent<Animator>().SetBool("enchanting", true);
			theLight.enabled = true;
		}else{
			GetComponent<Animator>().SetBool("enchanting", false);
			theLight.enabled = false;
		}

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.name == "PlayerTerrainCollider"){
			textObject.SetActive(true);
			costCanvas.SetActive(true);
		}else if(collider.GetComponent<PickupItem>() != null){
			if(collider.GetComponent<PickupItem>().enchantEffect == "Mundane"){
				string theEnchType = collider.GetComponent<PickupItem>().enchantType;
				if(Player.souls >= soulCost && (theEnchType == "sword" || theEnchType == "armor")){
					isEnchanting = true;
					Invoke("stopEnchantingEffects", 3f);
					enchantType = collider.GetComponent<PickupItem>().enchantType;
					int rand;
					if(enchantType == "sword"){
						rand = Random.Range(1,5);
						if(rand == 1){collider.GetComponent<PickupItem>().enchantEffect = "Sharp";}
						else if(rand == 2){collider.GetComponent<PickupItem>().enchantEffect = "Thorn";}
						if(rand == 3){collider.GetComponent<PickupItem>().enchantEffect = "Razor";}
						if(rand == 4){collider.GetComponent<PickupItem>().enchantEffect = "Ignited";}
						if(rand == 5){collider.GetComponent<PickupItem>().enchantEffect = "Magma";}
					}else if(enchantType == "armor"){
						rand = Random.Range(1,4);
						if(rand == 1){collider.GetComponent<PickupItem>().enchantEffect = "Barrier";}
						else if(rand == 2){collider.GetComponent<PickupItem>().enchantEffect = "Safeguard";}
						if(rand == 3){collider.GetComponent<PickupItem>().enchantEffect = "Warding";}
					}

					if(enchantType!="none"){Player.souls -= soulCost;}
				}
			}
		}
		
	}

	void OnTriggerExit2D(Collider2D collider) {

		if(collider.name == "PlayerTerrainCollider"){
			textObject.SetActive(false);
			costCanvas.SetActive(false);
		}

	}

	void stopEnchantingEffects() {
		isEnchanting = false;
	}

}
