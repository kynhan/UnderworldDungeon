using UnityEngine;
using System.Collections;

public class allEnemiesKilled : MonoBehaviour {

	public GameObject theCamera;
	Transform bossSkull;
	Transform theOriginalTarget;
	bool activeStart;
	bool donePanning = false;

	void Start () {
		Invoke ("startUpdates", 1.5f);
	}

	void startUpdates() {
		activeStart = true;
		bossSkull = GameObject.Find("teleporter").transform;
	}

	void Update () {
	
		/*if(transform.childCount == 0 && !Player.allEnemiesKilled && activeStart){
			theOriginalTarget = theCamera.GetComponent<CameraFollow>().target;
			theCamera.GetComponent<CameraFollow>().target = bossSkull;
			bossSkull.GetComponent<teleporter>().theState = "active";
			Player.allEnemiesKilled = true;
		}

		if(Player.allEnemiesKilled && (Vector2)theCamera.transform.position == (Vector2)bossSkull.position && !donePanning){
			Invoke ("panToPlayer", 1.5f);
			donePanning = true;
		}*/

	}

	void panToPlayer() {
		theCamera.GetComponent<CameraFollow>().target = theOriginalTarget;
	}

}
