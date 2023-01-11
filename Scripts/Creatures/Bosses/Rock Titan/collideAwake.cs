using UnityEngine;
using System.Collections;

public class collideAwake : MonoBehaviour {

	bool doneAwaking = false;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.transform.parent != null && !doneAwaking){
			if(collider.transform.parent.name == "Player"){
				transform.parent.SendMessage("startAwake");
				doneAwaking = true;
			}
		}
	}
}
