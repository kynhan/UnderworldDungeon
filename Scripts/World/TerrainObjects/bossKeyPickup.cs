using UnityEngine;
using System.Collections;

public class bossKeyPickup : MonoBehaviour {

	GameObject theCamera;
	Transform bossSkull;
	Transform theOriginalTarget;
	bool triggerOnce;
	bool donePanning;

	void Start () {
		Invoke ("findObjects", 1f);
	}

	void findObjects() {
		bossSkull = GameObject.Find("teleporter").transform;
		theCamera = GameObject.Find("Main_Camera").gameObject;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.name == "PlayerTerrainCollider" && !triggerOnce){
			activateTeleporter();
			GetComponent<SpriteRenderer>().enabled = false;
			triggerOnce = true;
		}
	}

	void activateTeleporter() {
		GameControlScript.bossKeyFound = true;
		theOriginalTarget = theCamera.GetComponent<CameraFollow>().target;
		theCamera.GetComponent<CameraFollow>().target = bossSkull;
		theCamera.GetComponent<CameraFollow>().panSpeed = 6f;
		bossSkull.GetComponent<teleporter>().theState = "active";
	}

	void Update () {
		if(GameControlScript.bossKeyFound){
			if((Vector2)theCamera.transform.position == (Vector2)bossSkull.position && !donePanning){
				Invoke ("panToPlayer", 1.5f);
				donePanning = true;
			}
		}
	}

	void panToPlayer() {
		theCamera.GetComponent<CameraFollow>().target = theOriginalTarget;
		theCamera.GetComponent<CameraFollow>().panSpeed = 6f;
		Destroy(gameObject);
	}

}
