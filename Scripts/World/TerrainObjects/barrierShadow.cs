using UnityEngine;
using System.Collections;

public class barrierShadow : MonoBehaviour {

	void Start () {
	
	}

	void activateTrap() {
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<Animator>().SetBool("activate", true);
	}
}
