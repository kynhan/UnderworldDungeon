using UnityEngine;
using System.Collections;

public class blink : MonoBehaviour {

	float theOpacity;
	float threshold;

	void Start () {
		theOpacity = 0f;
		threshold = 0.8f;
	}

	void Update () {
		if(theOpacity < threshold){
			theOpacity += 0.01f;
		}
		if(theOpacity >= 0.8f){
			threshold = 0.1f;
		}
		if(theOpacity > threshold){
			theOpacity -= 0.01f;
		}
		if(theOpacity <= 0.1f){
			threshold = 0.8f;
		}

		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, theOpacity);

	}
}
