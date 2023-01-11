using UnityEngine;
using System.Collections;

public class enterDungeon : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "PlayerTerrainCollider"){
			Destroy(collider.transform.parent.gameObject);
			GameObject.Find("Buttons").GetComponent<buttonsControl>().enterTheDungeon();
		}
	}

}
