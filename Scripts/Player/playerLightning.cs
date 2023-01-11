﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerLightning : MonoBehaviour {

	public int lightningDamage = 100;

	bool shadow_isFading = false;
	float shadow_fadeOpacity = 0f;
	bool lightning_isFading = false;

	bool hitRecharge = true;
	float lightning_fadeOpacity = 1f;
	bool triggerOnce;
	GameObject shadow;
	GameObject objRegion;
	GameObject playerObj;

	List<GameObject> hitEnemies = new List<GameObject>();

	void Start () {
	
		foreach(Transform child in transform){
			if(child.name == "shadow"){
				shadow = child.gameObject;
			}
			if(child.name == "objectRegion"){
				objRegion = child.gameObject;
			}
		}

		Invoke ("startLightning", 0f);
	}


	void Update () {
	
		//Fade Shadow
		if(shadow_isFading && shadow_fadeOpacity<1f){
			shadow.GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, shadow_fadeOpacity);
			shadow_fadeOpacity += 1f*Time.deltaTime;
		}

		//Initiate Lightning Strike
		if(shadow_fadeOpacity >= 0.8f && !triggerOnce){
			strikeLightning();
			triggerOnce = true;
		}

		//Fade Lightning
		if(lightning_isFading && lightning_fadeOpacity>0f){
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, lightning_fadeOpacity);
			lightning_fadeOpacity -= 1f*Time.deltaTime;
		}

		//Destroy Lightning
		if(lightning_fadeOpacity <= 0f){
			Destroy(gameObject);
		}
	}

	void startLightning() {
		shadow_isFading = true;
	}

	void strikeLightning() {
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<BoxCollider2D>().enabled = true;
		CameraFollow.ShakeCamera(0.1f,0.2f);
		Invoke ("removeLightning", 0.3f);
	}

	void removeLightning(){
		lightning_isFading = true;
		GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Enemy" || collider.tag == "Pet" || collider.tag == "Chest"){
			if(collider.transform.parent.GetComponent<generalEnemyAI>() != null){
				
				int damageRand = Random.Range(15, 25);
				int currentDamage = damageRand + Player.damage;
				bool alreadyHit = false;
				for (int i = 0; i < this.hitEnemies.Count; i++) {
					if(this.hitEnemies[i] == collider.gameObject){
						alreadyHit = true;
					}
				}
				if(!alreadyHit){
					this.hitEnemies.Add(collider.gameObject);
					collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(currentDamage);
				}
				
			}else{
				collider.SendMessageUpwards("gotHit", SendMessageOptions.DontRequireReceiver);
			}

		}

	}

	void rechargeHit() {
		hitRecharge = true;
	}
}
