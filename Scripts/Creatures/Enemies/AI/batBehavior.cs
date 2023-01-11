using UnityEngine;
using System.Collections;

public class batBehavior : MonoBehaviour {

	bool followingPlayer = false;
	bool isAngry = false;

	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	GameObject theAttackBox;

	float randX;
	float randY;

	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		generalAI = GetComponent<generalEnemyAI>();
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				theAttackBox = child.gameObject;
			}
		}
		changeTarget();
		setRage();
	}

	void FixedUpdate() {
		if(followingPlayer){
			if(isAngry){
				transform.position = Vector3.MoveTowards(transform.position, targetPos, 1.2f*Time.deltaTime);
			}else{
				transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.9f*Time.deltaTime);
			}
		}
	}

	void Update () {

		if((Vector2.Distance(player.transform.position, transform.position) < 2f || generalAI.inMobRoom) && !followingPlayer){
			followingPlayer = true;
		}

		if(!isAngry){
			
			if(transform.position == targetPos && followingPlayer){
				changeTarget();
			}
		}

		if(isAngry && Vector2.Distance(player.transform.position, transform.position) < 2f){
			anim.SetBool("isAttacking", true);
			theAttackBox.GetComponent<BoxCollider2D>().enabled = true;
			targetPos = player.transform.position;

			if(transform.position.x > player.transform.position.x){
				anim.SetFloat("direction", -1f);
			}else{
				anim.SetFloat("direction", 1f);
			}

			if(theAttackBox.GetComponent<enemyAttackBox>().didHitPlayer){
				isAngry = false;
			}

		}else{
			anim.SetBool("isAttacking", false);
			theAttackBox.GetComponent<BoxCollider2D>().enabled = false;

			//anim.SetFloat("direction", randX);
			if(transform.position.x > player.transform.position.x){
				anim.SetFloat("direction", -1f);
			}else{
				anim.SetFloat("direction", 1f);
			}
		}


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


		targetPos = new Vector2(player.transform.position.x + randX, player.transform.position.y + randY);
	}

	void setRage() {
		int theRand = Random.Range (1, 2);
		if(theRand == 1 && !isAngry){
			this.isAngry = true;
			this.Invoke ("notAngry", 1.5f);
		}
		Invoke ("setRage", 2f);
	}

	void notAngry() {
		this.isAngry = false;
	}
}
