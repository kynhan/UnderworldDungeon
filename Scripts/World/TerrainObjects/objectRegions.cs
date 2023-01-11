using UnityEngine;
using System.Collections;

public class objectRegions : MonoBehaviour {

	public float offsetY = 0f;

	void Start () {

	}

	void OnTriggerStay2D(Collider2D collider) {
		GameObject obj = collider.gameObject;
		if(obj.name == "regionBox"){
			if(obj.GetComponent<regionBoxOffset>() != null){
				if(obj.transform.position.y + obj.GetComponent<regionBoxOffset>().offsetY > transform.position.y + offsetY){
					obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 3;
				}else{
					obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
				}
			}else{
				if(obj.transform.position.y > transform.position.y + offsetY){
					obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 3;
				}else{
					obj.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.name == "regionBox"){
			collider.gameObject.transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 10;
		}
	}
}
