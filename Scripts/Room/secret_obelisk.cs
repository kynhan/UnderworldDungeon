using UnityEngine;
using System.Collections;

public class secret_obelisk : MonoBehaviour {

	GameObject textObject;	

	void Start () {
		foreach(Transform child in transform){
			if(child.name == "textObject"){
				textObject = child.gameObject;
			}
		}
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			GameControlScript.control.achUnlocked[11] = 1;
			textObject.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			textObject.SetActive(false);
		}
	}
}
