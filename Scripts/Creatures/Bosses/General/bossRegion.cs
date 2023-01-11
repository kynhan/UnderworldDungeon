using UnityEngine;
using System.Collections;

public class bossRegion : MonoBehaviour {

	public float yOffset = 0f;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D collider) {
		GameObject obj = collider.gameObject;
		if(obj.name == "playerHitBox" || obj.tag == "Enemy"){
			if(obj.transform.position.y + yOffset > transform.position.y){
				obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 3;
			}else{
				obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.name == "playerHitBox"){
			collider.gameObject.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
		}
	}
}
