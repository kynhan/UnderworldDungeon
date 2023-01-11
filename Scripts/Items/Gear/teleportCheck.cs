using UnityEngine;
using System.Collections;

public class teleportCheck : MonoBehaviour {

	Vector2 mousePos;
	Vector2 movement_vector;
	GameObject theParent;
	bool triggerOnce;

	void Start () {
		theParent = transform.parent.gameObject;
		transform.position = theParent.transform.position;
		Player.canTeleport = true;
	}

	void FixedUpdate() {

		GetComponent<Rigidbody2D>().MovePosition (GetComponent<Rigidbody2D>().position + movement_vector * Time.deltaTime * 50f);
	
	}

	void Update () {
	
		Vector2 pos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(pos);
		movement_vector = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

		/*if(Vector2.Distance(theParent.transform.position, transform.position) > 2f){
			transform.position = theParent.transform.position;
		}*/

		if(Player.resetTeleport){
			transform.position = theParent.transform.position;
			Player.resetTeleport = false;
		}
		if(Vector2.Distance(theParent.transform.position, transform.position) > 3f){
			transform.position = theParent.transform.position;
		}

	}
}
