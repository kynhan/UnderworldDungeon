using UnityEngine;
using System.Collections;

public class edgeDetector : MonoBehaviour {

	BoxCollider2D myCollider;
	bool canDetect = true;

	void Start() {
		myCollider = GetComponent<BoxCollider2D>();
	}



	void OnTriggerEnter2D(Collider2D collider){
		if(canDetect){
			if(collider.gameObject.name == "RoomEdge" || collider.gameObject.tag == "TerrainObject"){
				if(collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.name != "Player" && !collider.name.Contains("puddle")){
					transform.parent.GetComponent<generalEnemyAI>().hitEdge(collider.gameObject);
					canDetect = false;
					Invoke ("doDetect", 0.1f);
				}
			}

		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.transform.parent != null && collider.gameObject.transform.parent.name != "Player"){
			if(collider.gameObject.name == "RoomEdge" || collider.gameObject.tag == "TerrainObject"){
				transform.parent.GetComponent<generalEnemyAI>().outEdge();
			}
		}
	}

	void doDetect() {
		canDetect = true;
	}

	/*void OnCollisionStay2D(Collision2D collision) {
		if(collision.collider.gameObject.name == "RoomEdge" || collision.collider.gameObject.tag == "TerrainObject" || collision.collider.gameObject.tag == "Enemy"){
			transform.parent.GetComponent<generalEnemyAI>().hitEdge(collision);
		}
	}
	void OnCollisionExit2D(Collision2D collision) {
		if(collision.collider.gameObject.name == "RoomEdge" || collision.collider.gameObject.tag == "TerrainObject" || collision.collider.gameObject.tag == "Enemy"){
			transform.parent.GetComponent<generalEnemyAI>().outEdge();
		}
	}*/

	void FixedUpdate() {



	}

}
