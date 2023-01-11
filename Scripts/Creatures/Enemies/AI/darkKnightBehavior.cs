using UnityEngine;
using System.Collections;

public class darkKnightBehavior : MonoBehaviour {

	public static darkKnightBehavior control;

	public float restTime = 1f;
	public float speed = 2f;

	bool canCharge;
	bool charging;
	bool canPrepare;
	generalEnemyAI generalScript;

	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;

	//Attributes


	void Start () {
		control = this;
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		generalScript = GetComponent<generalEnemyAI>();
		canCharge = true;
	}

	void FixedUpdate() {
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f){
			if(canCharge){
				anim.SetBool ("isWalking", true);
				prepareCharge();
				canCharge = false;
			}
		}
		//Charge
		targetPos = player.transform.position;
		if(charging && !generalAI.isKnockingBack){
			transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
		}else{
			if(movementVector == Vector2.zero){rbody.velocity = Vector2.zero;}
		}
	}

	void Update () {

		//Animations
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
		Invoke ("chargePlayer", 0.5f);
	}

	void chargePlayer() {
		charging = true;
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				child.GetComponent<enemyAttackBox>().chargeDir(GetComponent<generalEnemyAI>().direction);
			}
		}
		Invoke ("stopCharging", 0.6f);
	}

	void stopCharging() {
		rest ();
	}

}
