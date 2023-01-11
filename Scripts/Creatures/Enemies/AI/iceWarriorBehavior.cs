using UnityEngine;
using System.Collections;

public class iceWarriorBehavior : MonoBehaviour {

	public GameObject iceSpear;
	GameObject theSpear;
	BoxCollider2D boxColliderObject;
	generalEnemyAI generalAI;
	Animator anim;
	Rigidbody2D rbody;
	GameObject player;
	bool triggerShoot;
	Vector2 scytheTarget;
	Vector2 walkTarget;
	bool isWalking;

	void Start () {
	
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();

	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;

		//Walk
		if(isWalking && !triggerShoot){
			walkTarget = player.transform.position;
			transform.position = Vector2.MoveTowards(transform.position, walkTarget, 1.1f*Time.deltaTime);
		}

	}

	void Update () {
	
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f && !isWalking  && !triggerShoot){
			isWalking = true;
		}

		if(Vector2.Distance(player.transform.position, transform.position) < 2f && Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f  && !triggerShoot){
			//Shoot
			anim.SetBool("isShooting", true);
			scytheTarget = player.transform.position;
			Invoke ("fireSpear", 0.75f);
			triggerShoot = true;
		}

		//Animations
		anim.SetBool("isWalking", isWalking);
		if(!triggerShoot){
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
	}

	void fireSpear() {
		if(generalAI.direction == 2){
			anim.SetFloat("inputX", 1);anim.SetFloat("inputY", 0);
		}else if(generalAI.direction == 4){
			anim.SetFloat("inputX", -1);anim.SetFloat("inputY", 0);
		}else if(generalAI.direction == 1){
			anim.SetFloat("inputX", 0);anim.SetFloat("inputY", 1);
		}else if(generalAI.direction == 3){
			anim.SetFloat("inputX", 0);anim.SetFloat("inputY", -1);
		}
		theSpear = Instantiate(iceSpear, transform.position, Quaternion.identity) as GameObject;
		theSpear.transform.parent = transform;
		if(generalAI.direction == 2){
			theSpear.GetComponent<Animator>().Play("right");
			theSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(70, 0f));
		}else if(generalAI.direction == 4){
			theSpear.GetComponent<Animator>().Play("left");
			theSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(-70, 0f));
		}else if(generalAI.direction == 1){
			theSpear.GetComponent<Animator>().Play("up");
			theSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 70f));
		}else if(generalAI.direction == 3){
			theSpear.GetComponent<Animator>().Play("down");
			theSpear.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -70f));
		}
		Invoke ("doneThrow", 2f);
	}

	void doneThrow() {
		anim.SetBool("isShooting", false);
		triggerShoot = false;
		isWalking = false;
	}

}
