using UnityEngine;
using System.Collections;

public class skullash_shield : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
		float theX = transform.parent.GetComponent<Animator>().GetFloat("inputX");
		float theY = transform.parent.GetComponent<Animator>().GetFloat("inputY");

		if(theX == 1f && theY == 0f){
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = false;
		}else if(theX == -1f && theY == 0f){
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = false;
		}else if(theX == 0f && theY == 1f){
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = false;
		}else if(theX == 0f && theY == -1f){
			transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
			transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = true;
		}
	}
}
