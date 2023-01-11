using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {

	Camera myCam;

	void Start () {
		myCam = GetComponent<Camera>();
		myCam.orthographicSize = 0.1f;
	}

	void Update () {
	
		if(myCam.orthographicSize <= 1.2){
			myCam.orthographicSize += 0.2f*Time.deltaTime;
		}

	}
}
