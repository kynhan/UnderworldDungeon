using UnityEngine;
using System.Collections;

public class spike : MonoBehaviour {

	public int damage = 50;
	public float deathTimer = 0f;
	GameObject playerObj;
	bool isSteppingOn;

	void Start () {
		if(deathTimer != 0f){
			Invoke ("killSelf", deathTimer);
		}
	}


	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			isSteppingOn = true;
			playerObj = collider.gameObject.transform.parent.gameObject;
			InvokeRepeating("damagePlayer", 0f, 0.5f);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider"){
			isSteppingOn = false;
			CancelInvoke("damagePlayer");
		}
	}

	void damagePlayer(){
		if(playerObj != null && isSteppingOn){
			playerObj.GetComponent<Player>().takeDamage(damage);
			playerObj.GetComponent<Player>().knockback(1, 0);
		}
	}

	void Update () {

		if(!GetComponent<BoxCollider2D>().isActiveAndEnabled){
			isSteppingOn = false;
			CancelInvoke("damagePlayer");
		}

	}

	void killSelf() {
		Destroy(gameObject);
	}

}
