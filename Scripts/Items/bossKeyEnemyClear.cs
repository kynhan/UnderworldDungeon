using UnityEngine;
using System.Collections;

public class bossKeyEnemyClear : MonoBehaviour {

	public bool innerClearing;
	bool canDelete = true;

	void Start() {
		Invoke ("finishDelete", 1f);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Enemy" && collider.name == "damageBox" && canDelete){
			if(collider.transform.parent != null){
				if(collider.transform.parent.GetComponent<DropItem>() != null){
					collider.transform.parent.GetComponent<DropItem>().shouldDropItem = false;
				}
				Destroy (collider.transform.parent.gameObject);
			}
		}
		if(collider.name == "ActivateRegion"){
			Destroy (collider.transform.parent.gameObject);
		}
		if(innerClearing && canDelete){
			if(collider.name != "teleporter" && collider.name != "playerHitBox" && collider.name != "tradeRegion" && collider.name != "statueBase"){ 
				if(collider.gameObject.transform.childCount > 0){
					if(collider.gameObject.transform.GetChild(0).GetComponent<DropItem>() != null){collider.gameObject.transform.GetChild(0).GetComponent<DropItem>().dropOnDeath = false;}
				}
				if(collider.gameObject.GetComponent<DropItem>() != null){collider.gameObject.GetComponent<DropItem>().dropOnDeath = false;}
				Destroy (collider.gameObject);
			}
		}
	}

	void finishDelete() {
		canDelete = false;
	}

}
