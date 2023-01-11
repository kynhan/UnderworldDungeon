using UnityEngine;
using System.Collections;

public class dieAfterTime : MonoBehaviour {

	public float aliveTime = 1f;
	public bool fadeOut = false;
	public float fadeOutAfter = 0.5f;
	public float fadeChange = 0.01f;

	bool isFading = false;
	float fadeOpacity = 1f;
	public bool killParent = false;

	void Start () {
		Invoke ("killSelf", aliveTime);
		if(fadeOut){Invoke ("fadeOutNow", fadeOutAfter);}
	}

	void fadeOutNow() {
		isFading = true;
	}

	void killSelf() {
		if(killParent){
			Destroy(gameObject.transform.parent.gameObject);
		}
		Destroy(gameObject);
	}

	void Update () {
		if(isFading){
			GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, fadeOpacity);
			fadeOpacity -= fadeChange;
		}
	}
}
