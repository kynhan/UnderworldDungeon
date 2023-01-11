using UnityEngine;
using System.Collections;

public class activateEnemies : MonoBehaviour {
	
	Camera myCam;
	Vector2 cameraPos;

	void Start () {
		myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void Update () {
	
		foreach(Transform child in transform){
			//Active on screen
			cameraPos = myCam.WorldToViewportPoint(child.gameObject.transform.position);
			if(this.cameraPos.x >= 0f && this.cameraPos.x <= 1.1f && this.cameraPos.y >= 0f && this.cameraPos.y <= 1.1f){
				MonoBehaviour[] scripts = child.gameObject.GetComponents<MonoBehaviour>();
				foreach(MonoBehaviour script in scripts)
				{
					script.enabled = true;
				}
			}else{
				MonoBehaviour[] scripts = child.gameObject.GetComponents<MonoBehaviour>();
				foreach(MonoBehaviour script in scripts)
				{
					script.enabled = false;
				}
			}
		}

	}
}
