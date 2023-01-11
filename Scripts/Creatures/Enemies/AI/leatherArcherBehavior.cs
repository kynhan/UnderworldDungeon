using UnityEngine;
using System.Collections;

public class leatherArcherBehavior : MonoBehaviour {

	public float speed = 0.8f;

	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	fireProjectile fireObj;
	generalEnemyAI generalAI;
	GameObject player;

	int setDirection;
	bool canAnimate = true;
	bool triggerOnce;
	bool shouldShoot = true;
	string chosenDirection = "null";

	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		fireObj = GetComponent<fireProjectile>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
		InvokeRepeating("movement", 0f, 2f);
	}

	void FixedUpdate(){
		if(Vector2.Distance(player.transform.position, transform.position) < 2f){
			if(!this.generalAI.isKnockingBack){rbody.MovePosition(rbody.position + movementVector * speed * Time.deltaTime);}
		}
	}

	void Update () {
	
		//Animations
		if(movementVector == Vector2.zero){
			anim.SetBool("isWalking", false);
		}else{
			anim.SetBool("isWalking", true);
			if(canAnimate){
				anim.SetFloat("inputX", movementVector.x);
				anim.SetFloat("inputY", movementVector.y);
			}
		}

		if(generalAI.edgeCollision != null){
			randomMove();
			triggerOnce = false;
			Invoke ("canChange", 0.1f);
		}

		if(shouldShoot){checkPlayer();}
	}

	void canChange() {triggerOnce = true;}

	void movement() {
		if(Mathf.Abs(player.transform.position.y - transform.position.y) > Mathf.Abs(player.transform.position.x - transform.position.x)){
			if(player.transform.position.y > transform.position.y){movementVector = new Vector2(0f, 1f);}
			else if(player.transform.position.y < transform.position.y){movementVector = new Vector2(0f, -1f);}
		}else{
			if(player.transform.position.x > transform.position.x){movementVector = new Vector2(1f, 0f);}
			else if(player.transform.position.x < transform.position.x){movementVector = new Vector2(-1f, 0f);}
		}
	}

	void randomMove() {
		int randNumber = Random.Range(1,5);
		if(randNumber == 1){chosenDirection="upDir";}else if(randNumber == 2){chosenDirection="rightDir";}
		else if(randNumber == 3){chosenDirection="downDir";}else if(randNumber == 4){chosenDirection="leftDir";}
		if(chosenDirection=="upDir"){movementVector = new Vector2(0f, 1f);}
		else if(chosenDirection=="rightDir"){movementVector = new Vector2(1f, 0f);}
		else if(chosenDirection=="downDir"){movementVector = new Vector2(0f, -1f);}
		else if(chosenDirection=="leftDir"){movementVector = new Vector2(-1f, 0f);}
	}

	void checkPlayer() {
		//Up and Down
		if(player.transform.position.x >= transform.position.x - 0.2f && player.transform.position.x <= transform.position.x + 0.2f && Mathf.Abs(player.transform.position.y - transform.position.y) < 1f){
			movementVector = Vector2.zero;
			shouldShoot = false;
			Invoke("doFire", 0.5f);
			if(player.transform.position.y >= transform.position.y){
				setDirection = 1;anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);
			}else{
				setDirection = 3;anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);
			}
			anim.SetBool("isAttacking", true);
			Invoke ("resetShot", 1f);
			Invoke ("setAnimate", 0.5f);
		}
		//Left and Right
		else if(player.transform.position.y >= transform.position.y - 0.2f && player.transform.position.y <= transform.position.y + 0.2f && Mathf.Abs(player.transform.position.x - transform.position.x) < 1f){
			movementVector = Vector2.zero;
			shouldShoot = false;
			Invoke("doFire", 0.5f);
			if(player.transform.position.x >= transform.position.x){
				setDirection = 2;anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);
			}else{
				setDirection = 4;anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);
			}
			anim.SetBool("isAttacking", true);
			Invoke ("resetShot", 4f);
			Invoke ("setAnimate", 0.5f);
		}
	}

	void doFire(){
		fireObj.fire(setDirection, 100f);
	}

	void setAnimate() {
		canAnimate = true;
		anim.SetBool("isAttacking", false);
	}

	void resetShot() {
		shouldShoot = true;
	}

}
