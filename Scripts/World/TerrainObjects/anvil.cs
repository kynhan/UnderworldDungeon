using UnityEngine;
using System.Collections;

public class anvil : MonoBehaviour {

	GameObject textObject;
	GameObject theMetalObj;
	string hasMetal = "null";
	bool triggerOnce;

	void Start () {
		foreach(Transform child in transform){
			if(child.name == "textObject"){
				textObject = child.gameObject;
			}
		}
		textObject.transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 20;	
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		
		if(collider.name == "PlayerTerrainCollider"){
			textObject.SetActive(true);
		}else if(collider.GetComponent<PickupItem>() != null){

			//Metal
			if(collider.name.Contains("ironBar")){hasMetal = "iron";theMetalObj=collider.gameObject;}
			else if(collider.name.Contains("goldBar")){hasMetal = "gold";theMetalObj=collider.gameObject;}
			else if(collider.name.Contains("darkMatter")){hasMetal = "dark";theMetalObj=collider.gameObject;}


		}
		
	}

	void OnTriggerStay2D(Collider2D collider) {

		if(collider.GetComponent<PickupItem>() != null){
			//Sword
			if(collider.GetComponent<PickupItem>().enchantType == "sword" && !triggerOnce && theMetalObj!=null){
				collider.GetComponent<PickupItem>().metalTier ++;
				Destroy(theMetalObj);
				triggerOnce = true;
			}
		}

	}

	void OnTriggerExit2D(Collider2D collider) {
		
		if(collider.name == "PlayerTerrainCollider"){
			textObject.SetActive(false);
		}
		if(collider.GetComponent<PickupItem>() != null){
			if(collider.GetComponent<PickupItem>().enchantType == "sword" || collider.GetComponent<PickupItem>().enchantType == "armor"){
				triggerOnce = false;
			}
		}
		
	}

}
