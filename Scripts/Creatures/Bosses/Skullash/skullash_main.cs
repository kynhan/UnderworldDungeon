using UnityEngine;
using System.Collections;

public class skullash_main : MonoBehaviour {

	public float speed = 1f;
	bool canAnimate = true;
	bool shouldCharge = true;
	bool attacking = false;
	string chosenDirection = "null";
	int attackDir = 1;

	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	Vector2 targetPos;
	generalEnemyAI generalAI;

	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
	}
	
	void FixedUpdate() {

		rbody.MovePosition(rbody.position);

	}

	void Update () {
		targetPos = player.transform.position;

		if(shouldCharge){
			anim.SetBool("isWalking", true);
		}else{
			anim.SetBool("isWalking", false);
		}
		//Animations
		if(shouldCharge){
			if(Mathf.Abs(transform.position.x - targetPos.x) > Mathf.Abs(transform.position.y - targetPos.y)){
				if(transform.position.x > targetPos.x){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);}
				else{anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);}
			}else{
				if(transform.position.y > targetPos.y){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
				else{anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			}
		}

		//Charge Player
		if(!generalAI.isKnockingBack && shouldCharge){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f*speed*Time.deltaTime);
		}

		//Near to Player
		if(Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f && Mathf.Abs(player.transform.position.y - transform.position.y) < 0.5f && !attacking){
			shouldCharge = false;
			attacking = true;
			attackDir = GetComponent<generalEnemyAI>().direction;
			anim.SetBool("isAttacking", true);
			Invoke ("attack", 0.2f);
		}

	}

	void attack() {
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				child.GetComponent<enemyAttackBox>().chargeDir(attackDir);
			}
		}
		Invoke ("idle", 0.5f);
	}

	void idle() {
		anim.SetBool("isAttacking", false);
		BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);
		Invoke ("canCharge", 2f);
	}

	void canCharge() {
		shouldCharge = true;
		attacking = false;
	}
}
