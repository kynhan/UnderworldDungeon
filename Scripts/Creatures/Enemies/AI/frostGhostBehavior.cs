using UnityEngine;
using System.Collections;

public class frostGhostBehavior : MonoBehaviour {

	bool canCharge;
	bool charging;
	bool triggerOnce;
	generalEnemyAI generalScript;
	
	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	fireProjectile projectileObj;
	
	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalScript = GetComponent<generalEnemyAI>();
		projectileObj = GetComponent<fireProjectile>();
		canCharge = true;
	}
	
	void FixedUpdate() {
		rbody.velocity = Vector2.zero;
		
		//Charge
		targetPos = player.transform.position;
		if(charging && !generalScript.isKnockingBack){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.1f*Time.deltaTime);
		}else{
			if(movementVector == Vector2.zero){rbody.velocity = Vector2.zero;}
		}
	}
	
	void Update () {
		
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f || generalScript.inMobRoom){
			if(canCharge){
				anim.SetBool ("isWalking", true);
				prepareCharge();
				canCharge = false;
			}
		}
		
		//Check if player is close
		if(Vector3.Distance(player.transform.position, transform.position) < 0.2f){
			attack();
		}
		
		//Animations
		if(!this.triggerOnce){
			if(generalScript.direction == 2){
				anim.SetFloat("inputX", 1);anim.SetFloat("inputY", 0);
			}else if(generalScript.direction == 4){
				anim.SetFloat("inputX", -1);anim.SetFloat("inputY", 0);
			}else if(generalScript.direction == 1){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", 1);
			}else if(generalScript.direction == 3){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", -1);
			}
		}
	}
	
	void prepareCharge() {
		targetPos = player.transform.position;
		targetPos = targetPos - transform.position;
		targetPos = transform.position + targetPos.normalized * 100f;
		charging = true;
	}
	
	void attack() {
		if(!this.triggerOnce){
			this.triggerOnce = true;
			charging = false;
			anim.SetBool ("isAttacking", true);
			Invoke ("swingWeapon", 0.1f);

		}
	}

	void swingWeapon(){
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				child.GetComponent<enemyAttackBox>().chargeDir(GetComponent<generalEnemyAI>().direction);
			}
		}
		Invoke ("destroyBox", 0.2f);
		Invoke ("stopAttack", 1.6f);
	}
	
	void destroyBox() {
		BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);
	}

	void stopAttack() {
		this.triggerOnce = false;
		canCharge = true;
		anim.SetBool ("isAttacking", false);
	}

}
