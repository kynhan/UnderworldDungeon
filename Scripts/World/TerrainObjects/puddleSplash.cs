using UnityEngine;
using System.Collections;

public class puddleSplash : MonoBehaviour {

	GameObject splash;
	bool canSplash = true;

	void Start () {


	}

	void doSplash(float posX, float posY) {
		splash = Resources.Load("TerrainObjects/puddle/splash") as GameObject;
		Instantiate(splash, new Vector3(posX, posY, 1), Quaternion.identity);
	}

	void OnTriggerStay2D(Collider2D collider) {
		if((collider.name == "PlayerTerrainCollider" || collider.name == "terrainCollider") && canSplash ){
			doSplash(collider.transform.position.x, collider.transform.position.y - 0.19f);
			canSplash = false;
			Invoke ("enableSplash", 0.05f);
		}
	}

	void enableSplash() {
		canSplash = true;
	}

	void Update () {
	
			

	}


}
