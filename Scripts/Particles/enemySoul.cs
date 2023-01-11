using UnityEngine;
using System.Collections;

public class enemySoul : MonoBehaviour {

	GameObject player;
	SpriteRenderer myRenderer; 
	bool shouldMove;
	float theJourney = 0f;

	void Start () {
		player = GameObject.Find("Player");
		myRenderer = GetComponent<SpriteRenderer>();
		Invoke ("goToPlayer", 1f);
	}

	void FixedUpdate() {
		if(shouldMove){
			theJourney+=0.1f*Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, player.transform.position, theJourney);
		}
	}

	void Update () {
	
	}

	void goToPlayer() {
		shouldMove = true;
	}

}
