using UnityEngine;
using System.Collections;

public class VoidDetection : MonoBehaviour {

	bool inRegion = false;
	
	
	void Start () {
		InvokeRepeating ("checkVoid", 1f, 0.1f);
	}
	
	void OnTriggerStay2D(Collider2D colliderObject) {
		if(colliderObject.name == "RoomRegion"){
			inRegion = true;
		}
	}

	void OnTriggerExit2D(Collider2D colliderObject) {
		if(colliderObject.name == "RoomRegion"){
			inRegion = false;
		}
	}

	void checkVoid() {
		if(!inRegion){
			Invoke ("shouldFall", 0.5f);
		}
	}


	void shouldFall() {
		if(!inRegion){
			gameObject.transform.parent.SendMessage("fallDeath");
		}
	}
}
