using UnityEngine;
using System.Collections;

public class hideOnDeath : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
		if(Player.isDead){
			GetComponent<Canvas>().enabled = false;
		}

	}
}
