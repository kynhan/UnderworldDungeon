using UnityEngine;
using System.Collections;

public class eye_arms : MonoBehaviour {

	GameObject parentObj;
	float colorIntensity;
	float colorThreshold = 0.2f;
	float colorMin = 0.2f;
	float colorMax = 1f;
	float addColor = 0.01f;

	void Start () {
	
		parentObj = transform.parent.gameObject;

	}

	void Update () {
		GetComponent<SpriteRenderer>().sortingOrder = parentObj.GetComponent<SpriteRenderer>().sortingOrder;
		GetComponent<SpriteRenderer>().color = new Color(colorIntensity, colorIntensity, colorIntensity, GetComponent<SpriteRenderer>().color.a);
		if(colorIntensity>colorThreshold){
			colorIntensity-=addColor;
		}else if(colorIntensity<colorThreshold){
			colorIntensity+=addColor;
		}
		if(colorIntensity <= colorMin){
			colorThreshold = colorMax;
		}else if(colorIntensity >= colorMax){
			colorThreshold = colorMin;
		}
	}
}
