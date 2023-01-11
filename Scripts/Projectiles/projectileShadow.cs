using UnityEngine;
using System.Collections;

public class projectileShadow : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
		if(transform.childCount > 0){
			transform.position = transform.GetChild(0).position;
		}else{
			Destroy(gameObject);
		}

	}
}
