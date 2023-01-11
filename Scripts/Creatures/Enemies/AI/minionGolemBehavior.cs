using UnityEngine;
using System.Collections;

public class minionGolemBehavior : MonoBehaviour {
	
	GameObject player;
	GameObject attackObject;
	GameObject lightObject;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;

	bool canJump = true;
	bool canMove = false;
	bool canAttack = true;
	bool canUpdate = false;
	bool canShoot = false;
	float speed = 1.9f; 
	bool visible = false;

	void Start () {
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();

		foreach(Transform child in transform){
			if(child.name == "attackBox"){
				attackObject = child.gameObject;
			}
			if(child.name == "point_light"){
				lightObject = child.gameObject;
			}
		}
		Invoke("finishSpawn", 0.6f);
	}

	void OnBecameVisible() {visible = true;}void OnBecameInvisible(){visible = false;}

	void finishSpawn() {
		canUpdate = true;
	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;

		if(canMove){
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
		}
	}

	void Update () {
		if(canUpdate){
			//Check if player is near
			if(visible){
				if(canJump){
					//Check if close enough to attack
					if(canAttack && Vector3.Distance(player.transform.position, transform.position) < 1f){
						canJump = false;
						canMove = false;
						canAttack = false;
						warn ();
					}else{
						anim.Play("jump");
						Invoke ("doMove", 0.2f);
						Invoke ("stopMove", 0.45f);
						Invoke ("doneJump", 0.7f);
						canJump = false;
					}
				}
			}
		}

	}

	void doMove(){
		canMove = true;
	}

	void stopMove(){
		anim.Play("idle");
		canMove = false;
	}

	void doneJump(){
		canJump = true;
	}

	void doneAttack(){
		lightObject.GetComponent<Light>().enabled = false;
		attackObject.GetComponent<BoxCollider2D>().enabled = false;
		anim.Play("idle");
		canJump = true;
		Invoke ("rechargeBeam", 1f);
	}

	void rechargeBeam() {
		canAttack = true;
	}

	void warn(){
		lightObject.GetComponent<Light>().enabled = true;
		anim.Play("warn");
		Invoke ("doAttack", 0.3f);
	}

	void doAttack(){
		//attackObject.GetComponent<BoxCollider2D>().enabled = true;
		//anim.Play("attack");
		GameObject instance;
		instance = Resources.Load("Projectiles/Laserbeam") as GameObject;

		GameObject theObj = new GameObject();

		if(generalAI.direction == 1){
			anim.Play("up");
			theObj = Instantiate(instance, new Vector3(transform.position.x + 0.03f, transform.position.y + 0.4f, 0), Quaternion.Euler(0, 0, 180f)) as GameObject;
			theObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
		}else if(generalAI.direction == 2){
			theObj = Instantiate(instance, new Vector3(transform.position.x + 0.43f, transform.position.y - 0.06f, 0), Quaternion.Euler(0, 0, 90f)) as GameObject;
		}else if(generalAI.direction == 3){
			theObj = Instantiate(instance, new Vector3(transform.position.x, transform.position.y - 0.5f, 0), Quaternion.identity) as GameObject;
		}else if(generalAI.direction == 4){
			theObj = Instantiate(instance, new Vector3(transform.position.x - 0.4f, transform.position.y - 0.02f, 0), Quaternion.Euler(0, 0, -90f)) as GameObject;
		}
		theObj.transform.parent = transform;

		theObj.GetComponent<laserbeamAttack>().aliveTime = 1f;
		theObj.GetComponent<laserbeamAttack>().laserDamage = 50;
		theObj.transform.localScale = new Vector3(0.75f, 0.75f, 1f);

		Invoke ("doneAttack", 1f);
	}
}
