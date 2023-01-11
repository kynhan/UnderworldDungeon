using UnityEngine;
using System.Collections;

public class ninjaBehavior : MonoBehaviour {

	public GameObject enemyScythe;
	GameObject attackObject;
	GameObject theScythe;
	BoxCollider2D boxColliderObject;
	generalEnemyAI generalAI;
	Animator anim;
	Rigidbody2D rbody;
	GameObject player;
	bool triggerShoot;
	bool moveScythe;
	Vector2 scytheTarget;
	Vector2 walkTarget;
	bool isReturning;
	bool isSlicing;
	bool isWalking;

	void Start () {
	
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				attackObject = child.gameObject;
			}
		}

	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;

		if(moveScythe && theScythe!=null){
			theScythe.transform.position = Vector2.MoveTowards(theScythe.transform.position, scytheTarget, 2.5f*Time.deltaTime);
		}

		//Walk
		if(isWalking){
			walkTarget = player.transform.position;
			transform.position = Vector2.MoveTowards(transform.position, walkTarget, 1.1f*Time.deltaTime);
		}

	}

	void Update () {
		
		//Check if player is VERY near
		if(Vector2.Distance(player.transform.position, transform.position) < 0.5f && !isReturning && !moveScythe && !isSlicing && !triggerShoot && !isWalking){
			if(player.transform.position.y > transform.position.y){
				anim.Play ("attack_up");
				createAttackBox("up");
			}else{
				anim.Play ("attack_down");
				createAttackBox("down");
			}
			Invoke ("doneSlicing", 2f);
			isSlicing = true;
		}
	
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f && Vector2.Distance(player.transform.position, transform.position) > 0.5f && !triggerShoot && !isSlicing && !isWalking){
			if(player.transform.position.y > transform.position.y){
				anim.Play ("shoot_up");
			}else{
				anim.Play ("shoot_down");
			}
			scytheTarget = player.transform.position;
			Invoke ("fireScythe", 0.5f);
			triggerShoot = true;
		}

		//Check if scythe returned
		if(isReturning && theScythe != null && theScythe.transform.position == transform.position){
			moveScythe = false;
			Destroy(theScythe);
			if(player.transform.position.y > transform.position.y){
				anim.Play ("walk_up");
			}else{
				anim.Play ("walk_down");
			}
			isWalking = true;
			Invoke ("stopWalking", 0.5f);
			isReturning = false;
			triggerShoot = false;
		}

		/*
		//Check if player above or below
		if(player.transform.position.y > transform.position.y){
			//Face Up
			GetComponent<SpriteRenderer>().sortingOrder = 12;
			anim.SetBool("facingDown", false);
		}else{
			//Face Down
			GetComponent<SpriteRenderer>().sortingOrder = 10;
			anim.SetBool("facingDown", true);
		}*/

		if(isReturning){
			scytheTarget = transform.position;
		}

	}

	void fireScythe() {
		theScythe = Instantiate(enemyScythe, transform.position, Quaternion.identity) as GameObject;
		theScythe.transform.parent = transform;
		Invoke ("scytheBack", 0.8f);
		moveScythe = true;
	}

	void scytheBack() {
		scytheTarget = transform.position;
		isReturning = true;
	}

	void createAttackBox(string upOrDown){

		if(upOrDown == "down"){
			Invoke ("swingBoxDown", 0.15f);
		}else if(upOrDown == "up"){
			Invoke ("swingBoxUp", 0.15f);
		}

	}

	void swingBoxUp() {
		boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
		boxColliderObject.size = new Vector2(0.34f, 0.12f);
		BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
		collider.offset = new Vector2(-0.03f, -0.06f);
		collider.isTrigger = true;
		Invoke ("removeAttackBox", 0.1f);
	}

	void swingBoxDown() {
		boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
		boxColliderObject.size = new Vector2(0.34f, 0.12f);
		BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
		collider.offset = new Vector2(-0.008f, -0.09f);
		collider.isTrigger = true;
		Invoke ("removeAttackBox", 0.1f);
	}

	void removeAttackBox() {
		if(boxColliderObject != null){Destroy (boxColliderObject);}
	}

	void doneSlicing() {
		isSlicing = false;
		anim.Play("idle_down");
	}

	void stopWalking() {
		isWalking = false;
		if(player.transform.position.y > transform.position.y){
			anim.Play ("idle_up");
		}else{
			anim.Play ("idle_down");
		}
	}

}
