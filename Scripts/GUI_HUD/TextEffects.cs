using UnityEngine;
using System.Collections;

public class TextEffects : MonoBehaviour {
	
	public float aliveTime = 1f;
	public bool waveText = true;

	float waveZ = 0f;
	public float rotationThreshold = -2.5f;
	public float rotationMin = -2.5f;
	public float rotationMax = 2.5f;
	public float rotateSpeed = 0.1f;
	
	public bool fadeOut = true;
	public float fadeOutAfter = 0.5f;
	public float fadeChange = 0.01f;
	bool isFading = false;
	float fadeOpacity = 1f;

	void Start () {
		if(GetComponent<MeshRenderer> ()!=null){GetComponent<MeshRenderer> ().sortingOrder = 20;}
		if(fadeOut){Invoke ("fadeOutNow", fadeOutAfter);}
		Invoke ("killSelf", aliveTime);
	}

	void killSelf() {
		Destroy(gameObject);
	}

	void Update () {

		if(waveText){
			if(waveZ>rotationThreshold){
				waveZ-=rotateSpeed;
			}else if(waveZ<rotationThreshold){
				waveZ+=rotateSpeed;
			}
			if(waveZ <= rotationMin){
				rotationThreshold = rotationMax;
			}else if(waveZ >= rotationMax){
				rotationThreshold = rotationMin;
			}
			transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, waveZ);
		}

		if(isFading){
			GetComponent<TextMesh>().color = new Color(GetComponent<TextMesh>().color.r, GetComponent<TextMesh>().color.g, GetComponent<TextMesh>().color.b, fadeOpacity);
			fadeOpacity -= fadeChange;
		}
	}

	void fadeOutNow() {
		isFading = true;
	}
}
