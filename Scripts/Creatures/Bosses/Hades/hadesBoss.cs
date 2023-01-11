using UnityEngine;
using System.Collections;

public class hadesBoss : MonoBehaviour {

	public GameObject theBident;
	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	generalEnemyAI generalAI;
	GameObject player;
	GameObject bidentObj;
	GameObject attackBox;
	Vector2 bidentTarget;
	Vector3 targetPos;
	float randomX;
	float randomY;
	float randomTime = 1f;
	
	bool isAwake = false;
	bool followingPlayer = true;
	bool moveBident = false;
	GameObject lightObject;
	GameObject healthBar;
	GameObject minionsObj;
	bool triggerOnce;
	bool attacking = false;
	bool fadeOut = false;
	bool fadeIn = false;
	bool bidentReturning = false;
	float opacity = 1f;
	bool canSwipe = true;
	
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
		foreach(Transform child in transform){
			if(child.name == "weaponLight"){
				lightObject = child.gameObject;
			}
			if(child.name == "EnemyHealth"){
				healthBar = child.gameObject;
			}
			if(child.name == "attackBox"){
				attackBox = child.gameObject;
			}
		}
		foreach(Transform child in transform.parent.transform){
			if(child.name == "minionObjects"){
				minionsObj = child.gameObject;
			}
		}
	}
	
	void FixedUpdate() {
		if(followingPlayer && isAwake && !attacking){
			transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f*Time.deltaTime);
		}
		if(moveBident && bidentObj!=null){
			bidentObj.transform.position = Vector2.MoveTowards(bidentObj.transform.position, bidentTarget, 4f*Time.deltaTime);
		}
		rbody.velocity = Vector2.zero;
	}
	
	
	void Update () {
		//Opacity
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, opacity);

		if(Vector3.Distance(player.transform.position, transform.position) < 1.5f && !isAwake){
			if(!triggerOnce){
				Invoke ("wakeUp", 0.5f);
				triggerOnce = true;
			}
		}

		if(Vector3.Distance(player.transform.position, transform.position) < 1.5f && isAwake && !attacking && canSwipe){
			if(player.transform.position.y < transform.position.y){
				anim.Play("swipe_down");
				Invoke ("createSwipeBoxDown", 0.3f);
			}
			Invoke ("destroyBox", 0.4f);
			Invoke ("doneSwipe", 0.6f);
			Invoke ("reloadSwipe", 2f);
			canSwipe = false;
		}
		
		if(Vector3.Distance(player.transform.position, transform.position) < 4f && isAwake){
			if(transform.position == targetPos && followingPlayer && !attacking){
				changeTarget();
			}
		}
		if(fadeIn){
			if(opacity <= 1f){opacity += 0.5f;}else{fadeIn=false;}
		}
		if(fadeOut){
			if(opacity >= 0f){opacity -= 0.5f;}else{fadeOut=false;}
		}
		if(bidentReturning && bidentObj!=null && Vector2.Distance(bidentObj.transform.position, transform.position) < 0.25f){
			attacking = false;
			moveBident = false;
			bidentReturning = false;
			anim.Play("idle");
			changeTarget();
			Destroy(bidentObj);
		}
	}
	
	void changeTarget() {
		int randAttack = Random.Range(1,4);
		if(randAttack == 1){
			attack();
		}else{
			//Set Position
			int rand = Random.Range(1,3);
			
			if(rand == 1){
				randomX = -Random.value - 0.3f;
				randomY = Random.value + 0.3f;
			}else{
				randomX = Random.value + 0.3f;
				randomY = Random.value + 0.3f;
			}
			targetPos = new Vector2(player.transform.position.x + randomX, player.transform.position.y + randomY);
		}
	}

	void attack() {
		attacking = true;
		fadeOut = true;
		GetComponent<generalEnemyAI>().isInvincible = true;
		healthBar.GetComponent<Canvas>().enabled=false;
		Invoke ("teleport", 0.5f);
	}

	void teleport() {
		float randOffsetX = Random.value; float theOffsetX = 0f;
		if(randOffsetX>=0f && randOffsetX<0.3f){theOffsetX = 0.17f;}else if(randOffsetX>=0.3f && randOffsetX<0.6f){theOffsetX = 0.15f;}else if(randOffsetX>=0.6f && randOffsetX<=1f){theOffsetX = 0.19f;}
		float randOffsetY = Random.value; float theOffsetY = 0f;
		if(randOffsetY>=0f && randOffsetY<0.3f){theOffsetY = 1.7f;}else if(randOffsetY>=0.3f && randOffsetY<0.6f){theOffsetY = 1.8f;}else if(randOffsetY>=0.6f && randOffsetY<=1f){theOffsetY = 1.9f;}

		transform.position = new Vector2(player.transform.position.x + theOffsetX, player.transform.position.y + theOffsetY);
		fadeIn = true;
		GetComponent<generalEnemyAI>().isInvincible = false;
		healthBar.GetComponent<Canvas>().enabled=true;
		anim.Play("attack");
		Invoke ("fireBident", 0.2f);
	}

	void fireBident() {
		anim.Play("idle_weaponless");
		bidentTarget = new Vector2(player.transform.position.x + 0.1f, player.transform.position.y - 5f);
		bidentObj = Instantiate(theBident, transform.position, Quaternion.identity) as GameObject;
		bidentObj.transform.parent = transform;
		moveBident = true;
		Invoke ("bidentBack", 0.8f);
	}

	void bidentBack() {
		bidentTarget = transform.position;
		bidentReturning = true;
	}
	
	void wakeUp() {
		isAwake = true;
		InvokeRepeating("checkSpawnMinion", 0f, 3.5f);
		changeTarget();
	}

	void createSwipeBoxDown() {
		attackBox.GetComponent<enemyAttackBox>().chargeDir(3);
	}
	void createSwipeBoxUp() {
		attackBox.GetComponent<enemyAttackBox>().chargeDir(1);
	}

	void destroyBox() {
		BroadcastMessage("stopChargingEdge",SendMessageOptions.DontRequireReceiver);
	}

	void doneSwipe() {
		anim.Play("idle");
	}

	void reloadSwipe() {
		canSwipe = true;
	}

	void checkSpawnMinion() {
		//Spawn minion
		int randMinion = Random.Range(1,3);
		if(randMinion == 1 && minionsObj.transform.childCount<=3){
			minionsObj.GetComponent<hadesMinions>().initiateSpawn();
		}
	}
}
