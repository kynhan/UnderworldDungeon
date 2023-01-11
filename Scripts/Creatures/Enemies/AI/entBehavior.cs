using UnityEngine;
using System.Collections;

public class entBehavior : MonoBehaviour {

	bool canCharge;
	bool charging;
	bool triggerOnce;
	generalEnemyAI generalScript;
	int attackTimes = 0;  
	int attackDirection;
	
	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	
	//Attributes
	
	
	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		generalScript = GetComponent<generalEnemyAI>();
		canCharge = true;
	}

	void FixedUpdate() {

		rbody.velocity = Vector2.zero;
		//Charge
		targetPos = player.transform.position;
		if(charging && !generalAI.isKnockingBack){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f*Time.deltaTime);
		}else{
			if(movementVector == Vector2.zero){rbody.velocity = Vector2.zero;}
		}
	}
	
	void Update () {
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f){
			if(canCharge){
				anim.SetBool("doneResting", true);
				anim.SetBool ("isWalking", true);
				prepareCharge();
				canCharge = false;
			}
		}

		//Check if player is close
		if(Vector3.Distance(player.transform.position, transform.position) < 0.3f){
			if(!this.triggerOnce){attackDirection = generalScript.direction;
				if(attackDirection == 2){
					anim.SetFloat("inputX", 1);anim.SetFloat("inputY", 0);
				}else if(attackDirection == 4){
					anim.SetFloat("inputX", -1);anim.SetFloat("inputY", 0);
				}else if(attackDirection == 1){
					anim.SetFloat("inputX", 0);anim.SetFloat("inputY", 1);
				}else if(attackDirection == 3){
					anim.SetFloat("inputX", 0);anim.SetFloat("inputY", -1);
				}

				attack ();
			}

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
	
	void attack() {
		if(!this.triggerOnce){
			this.triggerOnce = true;
			targetPos = transform.position;
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", true);
			charging = false;
			canCharge = false;
			createAttackBox();
		}
	}

	void createAttackBox(){
		if(attackTimes < 3){
			attackTimes ++;
			Invoke ("enableCollider", 0.3f);
			Invoke ("disableCollider", 0.6f);
			Invoke ("createAttackBox", 0.65f);
		}else{
			this.triggerOnce = false;
			attackTimes = 0;
			anim.SetBool ("isAttacking", false);
			canCharge = true;
		}
	}

	void enableCollider(){
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				child.GetComponent<enemyAttackBox>().chargeDir(attackDirection);
			}
		}
	}

	void disableCollider(){BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);}
	
	void prepareCharge() {
		targetPos = player.transform.position;
		targetPos = targetPos - transform.position;
		targetPos = transform.position + targetPos.normalized * 100f;
		chargePlayer();
	}
	
	void chargePlayer() {
		charging = true;
	}

	
}
