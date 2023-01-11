using UnityEngine;
using System.Collections;

public class spawnBarrier : MonoBehaviour {

	bool triggerOnce;

	void Start () {
	
	}

	void Update () {
	
		if(!GameControlScript.control.toggleTutorial && !triggerOnce){
			doneTrap();
			triggerOnce = false;
		}

	}

	void doneTrap() {
		GetComponent<Animator>().SetBool("trapDone", true);
		Invoke ("killSelf", 0.6f);
	}
	
	void killSelf() {
		Destroy(gameObject);
	}
}
