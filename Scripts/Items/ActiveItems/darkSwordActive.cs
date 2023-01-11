using UnityEngine;
using System.Collections;

public class darkSwordActive : MonoBehaviour {

	public Transform pulseEffect;
	public Transform fearEffect;
	bool triggerOnce;
	bool pulseActive;

	void Start () {
	
	}

	void Update () {
	
		if(Player.isAttacking){
			if(triggerOnce){
				Vector2 playerPos = gameObject.transform.parent.parent.parent.transform.position;
				Instantiate(pulseEffect, playerPos, gameObject.transform.rotation); triggerOnce=false; 
				Instantiate(fearEffect, playerPos, gameObject.transform.rotation);
				pulseActive = true;
				Invoke ("deactivatePulse", 2f);
			}
		}else{
			triggerOnce = true;
		}

	}

	void deactivatePulse() {
		pulseActive = false;
	}

}
