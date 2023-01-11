using UnityEngine;
using System.Collections;

public class ghostKnightBehavior : MonoBehaviour {

	bool canCharge;
	bool charging;
	bool shouldWander;
	bool triggerOnce;
	generalEnemyAI generalScript;
	int fireDirection;
	
	Vector2 movementVector;
	Vector3 targetPos;
	Vector3 wanderPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	fireProjectile projectileObj;
	GameObject theDagger;

	float randX;
	float randY;

	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		generalScript = GetComponent<generalEnemyAI>();
		projectileObj = GetComponent<fireProjectile>();
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

		if(shouldWander){
			transform.position = Vector3.MoveTowards(transform.position, wanderPos, 0.7f*Time.deltaTime);
		}
	}
	
	void Update () {
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f || generalAI.inMobRoom){
			if(canCharge){
				anim.SetBool ("isWalking", true);
				prepareCharge();
				canCharge = false;
			}
		}
		
		//Check if player is close
		if(Vector3.Distance(player.transform.position, transform.position) < 0.7f){
			attack();
		}

		if(shouldWander){
			if(transform.position == wanderPos){
				changeTarget();
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
			fireDirection = generalScript.direction;
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
			if(generalScript.direction == 2){
				anim.SetFloat("inputX", 1);anim.SetFloat("inputY", 0);
			}else if(generalScript.direction == 4){
				anim.SetFloat("inputX", -1);anim.SetFloat("inputY", 0);
			}else if(generalScript.direction == 1){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", 1);
			}else if(generalScript.direction == 3){
				anim.SetFloat("inputX", 0);anim.SetFloat("inputY", -1);
			}
			fireDirection = generalScript.direction;
			this.triggerOnce = true;
			charging = false;
			anim.SetBool ("isAttacking", true);
			Invoke ("shootDagger", 0.5f);
			Invoke ("changeAnim", 0.75f);
		}
	}
	
	void shootDagger() {
		shouldWander = true;
		projectileObj.fire(fireDirection, 100f);
		foreach(Transform child in transform){
			if(child.name.Contains("ghostBlade")){
				theDagger = child.gameObject;
			}
		}
		float randTime = Random.Range(1f, 2f);
		Invoke ("returnProj", randTime);
		Invoke ("killProj", randTime*1.5f);
	}

	void returnProj() {
		if(theDagger != null){
			theDagger.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			if(fireDirection == 1){
				theDagger.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -200f));
			}else if(fireDirection == 2){
				theDagger.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200f, 0));
			}else if(fireDirection == 3){
				theDagger.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200f));
			}else if(fireDirection == 4){
				theDagger.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 0));
			}
		}
	}

	void changeAnim() {
		anim.SetBool ("isWalking", false);
		anim.SetBool ("isAttacking", false);
	}
	
	void stopAttack() {
		changeTarget();
		canCharge = true;
		shouldWander = false;
		this.triggerOnce = false;
		anim.SetBool ("isWalking", true);
	}

	void killProj() {
		Destroy (theDagger);
		stopAttack();
	}

	void changeTarget() {
		int rand = Random.Range(1,3);
		
		if(rand == 1){
			randX = -Random.value;
			randY = -Random.value;
		}else{
			randX = Random.value;
			randY = Random.value;
		}

		wanderPos = new Vector2(player.transform.position.x + randX, player.transform.position.y + randY);
	}

}
