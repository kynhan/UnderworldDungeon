using UnityEngine;
using System.Collections;

public class pulsingLight : MonoBehaviour {

	public Light lightComponent;
	float lightIntensity;
	float lightThreshold = 4f;
	public float lightMin = 4f;
	public float lightMax = 8f;
	public float addLight = 0.1f;

	void Start () {

		lightIntensity = 0f;
		lightComponent = GetComponent<Light>();

	}

	void Update () {
		if(lightComponent.enabled){
			lightComponent.intensity = lightIntensity;
			if(lightIntensity>lightThreshold){
				lightIntensity-=addLight;
			}else if(lightIntensity<lightThreshold){
				lightIntensity+=addLight;
			}
			if(lightIntensity <= lightMin){
				lightThreshold = lightMax;
			}else if(lightIntensity >= lightMax){
				lightThreshold = lightMin;
			}
		}
	}
}
