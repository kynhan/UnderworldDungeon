using UnityEngine;
using System.Collections;

public class laserbeamAttack : MonoBehaviour {

	public float aliveTime = 5f;
	public int laserDamage = 80;
	bool isFading = false;
	float fadeOpacity = 1f;
	bool canHit = true;

	void Start () {
		Invoke ("fade", aliveTime-1f);
		Invoke("killSelf", aliveTime);
	}

	void Update () {
	
		//Fade
		if(isFading){
			GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, fadeOpacity);
			fadeOpacity -= 0.02f;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.name == "playerHitBox" && !Player.isBlocking){
			if(gameObject && canHit){
				int randomDir = Random.Range(1, 4);

				collider.transform.parent.GetComponent<Player>().knockback(randomDir, 1f);
				if(transform.parent.gameObject != null){collider.SendMessageUpwards("enemyHit", transform.parent.gameObject);}
				canHit = false;
				Invoke ("rechargeHit", 0.5f);
			}
		}
	}

	void killSelf() {
		Destroy(gameObject);
	}

	void rechargeHit() {
		canHit = true;
	}

	void fade() {
		isFading = true;
	}
}
