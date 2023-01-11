using UnityEngine;
using System.Collections;

public class barrier : MonoBehaviour {

	void Start () {

	}

	void Update () {
	
	}

	void activateTrap() {
		CameraFollow.ShakeCamera(0.06f,1f);
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<Animator>().SetBool("activate", true);

	}

	void doneTrap() {
		GetComponent<Animator>().SetBool("trapDone", true);
		Invoke ("killSelf", 0.6f);
	}

	void killSelf() {
		Destroy(gameObject);
	}
}
