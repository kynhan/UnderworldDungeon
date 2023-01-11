using UnityEngine;
using System.Collections;

public class skullash_shieldDetection : MonoBehaviour {

	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			transform.parent.parent.GetComponent<generalEnemyAI>().isInvincible = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			transform.parent.parent.GetComponent<generalEnemyAI>().isInvincible = false;
		}
	}

	void Update () {
	
	}
}
