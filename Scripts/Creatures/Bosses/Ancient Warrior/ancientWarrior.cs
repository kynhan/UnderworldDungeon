using UnityEngine;
using System.Collections;

public class ancientWarrior : MonoBehaviour {

	//Set Region for Lightning to Spawn (must manually find positions for each boss room)
	public Vector2 lightningRegionX;
	public Vector2 lightningRegionY;

	Vector2 movementVector;
	Vector3 targetPos;
	GameObject player;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	PolygonCollider2D attackCollider;

	bool followPlayer;
	bool isCharging;
	bool isResting;
	bool isAttacking;

	float speed = 1f;
	int chargeTimes = 0;
	GameObject beamObj;

	//Lightning
	bool shouldLightning;
	bool triggerLightning;
	bool triggerOnce;
	bool lightningActive;
	bool triggerStrikes;

	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		InvokeRepeating("checkDirection", 0f, 0.1f);
		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				attackCollider = child.GetComponent<PolygonCollider2D>();
			}
			if(child.name == "beam"){
				beamObj = child.gameObject;
			}
		}
	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;

		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f && !shouldLightning){
			if(!followPlayer){
				speed = 1f;
				anim.Play("walk");
				followPlayer = true;
			}

			//Charge
			if(Vector2.Distance(player.transform.position, transform.position) < 1f){

				if(!triggerOnce && !lightningActive){
					float randNum = Random.value;
					if(randNum < 0.4f){
						shouldLightning = true;
					}
					triggerOnce = true;
				}

				if(shouldLightning && !triggerLightning){
					triggerLightning = true;
					doLightningFunc();
				}else{
					chargeFunctions();
					
					//Attack
					if(Vector2.Distance(player.transform.position, transform.position) < 0.6f){
						attackFunctions();
					}
				}
			}
		}

		//Charge
		targetPos = player.transform.position;
		if(followPlayer && !generalAI.isKnockingBack){
			transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
		}else{
			if(movementVector == Vector2.zero){rbody.velocity = Vector2.zero;}
		}

	}

	void doLightningFunc() {
		followPlayer = false;
		isCharging = false;
		isResting = false;
		anim.Play("strike");
		rbody.velocity = Vector2.zero;
		Invoke ("showBeam", 0.3f);
	}

	void showBeam(){beamObj.SetActive(true);Invoke ("removeBeam", 1.5f);CameraFollow.ShakeCamera(0.15f,1f);}
	void removeBeam(){beamObj.SetActive(false);isResting=true;lightningActive = true;rest();}

	void chargeFunctions() {
		if(!isCharging){
			chargeTimes++;
			anim.Play("charge");
			speed = 2f;
			isCharging = true;
		}
	}

	void attackFunctions() {
		if(!isResting){
			Invoke ("createAttack", 0.1f);
			Invoke ("checkCharges", 0.5f);
			anim.Play("attack");
			isAttacking = true;
			speed = 0f;
			isResting = true;
		}
	}

	void checkDirection() {
		if(!shouldLightning && !isAttacking){
			if(transform.position.x > targetPos.x){
				transform.localRotation = Quaternion.Euler(0,180,0);
			}else if(transform.position.x < targetPos.x){
				transform.localRotation = Quaternion.Euler(0,0,0);
			}
		}
	}

	void checkCharges() {
		if(chargeTimes < 2){
			followPlayer = false;
			isCharging = false;
			isResting = false;
			isAttacking = false;
		}else if(chargeTimes > 1){
			speed = 0f;
			Invoke ("rest", 0.2f);
			isAttacking = false;
			isResting = true;
		}
	}

	void rest() {
		anim.Play ("idle");
		Invoke ("resetCharge", 1f);
	}

	void resetCharge() {
		followPlayer = false;
		isCharging = false;
		isResting = false;
		shouldLightning = false;
		triggerOnce = false;
		triggerLightning = false;
		chargeTimes = 0;
	}

	void createAttack () {
		attackCollider.enabled = true;
		Invoke ("destroyAttack", 0.1f);
	}

	void destroyAttack() {
		attackCollider.enabled = false;
	}

	void Update () {
		//Debug.Log (Vector2.Distance(player.transform.position, transform.position));
		transform.position = new Vector3(transform.position.x, transform.position.y, 1f);

		if(lightningActive && !triggerStrikes){
			InvokeRepeating("doLightningStrikes", 0f, 3f);
			Invoke ("cancelLightningStrikes", 12f);
			triggerStrikes = true;
		}
	}

	void doLightningStrikes() {
		int randNumber = Random.Range(2,5);
		for(int i=0;i<randNumber;i++){
			Vector2 spawnPosition;
			spawnPosition.x = Random.Range(lightningRegionX.x, lightningRegionX.y);
			spawnPosition.y = Random.Range(lightningRegionY.x, lightningRegionY.y);

			GameObject instance = Resources.Load("Projectiles/Boss/boss_lightning") as GameObject;
			Instantiate(instance, spawnPosition, Quaternion.identity);
		}
	}

	void cancelLightningStrikes() {
		lightningActive = false;
		triggerStrikes = false;
		CancelInvoke("doLightningStrikes");
	}
}
