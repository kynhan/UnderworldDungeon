using UnityEngine;
using System.Collections;

public class crusaderBehavior : MonoBehaviour {

	public float restTime = 1.3f;
	public float speed = 1f;

	bool canCharge;
	bool charging;
	bool canPrepare;
	bool usedDodge;
	bool triggerOnce;
	bool canSwing = true;

	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	int attackDirection;

	void Start () {
	
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		canCharge = true;

	}
	void FixedUpdate() {
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f){
			if(canCharge){
				//Debug.Log ("can charge");
				anim.SetBool ("isWalking", true);
				prepareCharge();
				canCharge = false;
			}
		}
		//Attack
		if(Vector3.Distance(player.transform.position, transform.position) < 0.3f){

			if(!this.triggerOnce){
				attackDirection = generalAI.direction;
				stopCharging();
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
				canSwing = false;
			}
		}

		//Charge
		targetPos = player.transform.position;
		if(charging && !generalAI.isKnockingBack){
			transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
		}else{
			if(movementVector == Vector2.zero){rbody.velocity = Vector2.zero;}
		}
		if(movementVector != Vector2.zero){
			rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * 0.5f);
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
			Invoke ("activateBox", 0.35f);
			Invoke ("breakBox", 0.45f);
			Invoke ("stopAttack", 0.6f);
		}
	}

	void Update () {
		
		//Animations
		if(!anim.GetBool("isAttacking")){
			if(generalAI.direction == 2){
				anim.SetFloat("inputX", 1);anim.SetFloat("inputY", 0);
			}else if(generalAI.direction == 4){
				anim.SetFloat("inputX", -1);anim.SetFloat("inputY", 0);
			}else if(generalAI.direction == 1){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", 1);
			}else if(generalAI.direction == 3){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", -1);
			}
		}

		//Jump Back
		if(Player.isAttacking && Vector3.Distance(player.transform.position, transform.position) < 0.5f && !usedDodge){
			usedDodge = true;
			stopAttack();
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
			anim.SetBool ("isWalking", false);
			int playerDirection = Player.direction;
			if(playerDirection == 1){
				movementVector = new Vector2(0f, 3f);
			}else if(playerDirection == 2){
				movementVector = new Vector2(3f, 0f);
			}else if(playerDirection == 3){
				movementVector = new Vector2(0f, -(3f));
			}else if(playerDirection == 4){
				movementVector = new Vector2(-(3f), 0f);
			}
			Invoke ("stopMoving", 0.4f);
			Invoke ("resetDodge", 4f);
		}

		//Stop Attacking
		if(anim.GetBool("isAttacking")){
			canCharge = false;
			charging = false;
		}
	}
	
	void rest() {
		targetPos = transform.position;
		anim.SetBool ("isWalking", false);
		BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);
		charging = false;
		canPrepare = true;
		Invoke ("canChargeFunction", restTime);
	}
	
	void canChargeFunction(){canPrepare = false;canCharge = true;}
	
	void prepareCharge() {
		targetPos = player.transform.position;
		targetPos = targetPos - transform.position;
		targetPos = transform.position + targetPos.normalized * 100f;
		chargePlayer();
	}
	
	void chargePlayer() {
		charging = true;
	}
	
	void stopCharging() {
		rest ();
	}

	void resetDodge() {
		usedDodge = false;
	}

	void stopMoving() {
		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
		movementVector = Vector2.zero;
	}

	void activateBox(){
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				child.GetComponent<enemyAttackBox>().chargeDir(attackDirection);
			}
		}
	}

	void breakBox() {
		BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);
	}

	void stopAttack() {
		this.triggerOnce = false;
		charging = false;
		canCharge = false;
		anim.SetBool ("isAttacking", false);
		Invoke ("canChargeAgain", 2f);
	}

	void canChargeAgain(){
		canSwing = true;
		canCharge = true;
		this.triggerOnce = false;
	}

}
