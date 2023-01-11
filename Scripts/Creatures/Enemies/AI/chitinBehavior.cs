using UnityEngine;
using System.Collections;

public class chitinBehavior : MonoBehaviour {

	public float speed = 0.6f;
	
	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	fireProjectile fireObj;
	generalEnemyAI generalAI;
	GameObject player;
	GameObject lightObject;
	
	int setDirection;
	bool canAnimate = true;
	bool triggerOnce;
	bool triggerChance;
	bool shouldShoot = true;
	string chosenDirection = "null";
	bool visible = false;
	
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		fireObj = GetComponent<fireProjectile>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");

		foreach(Transform child in transform){
			if(child.name == "point_light"){
				lightObject = child.gameObject;
			}
		}

		InvokeRepeating("movement", 0f, 2f);
	}

	void OnBecameVisible() {visible = true;}void OnBecameInvisible(){visible = false;}
	
	void FixedUpdate(){
		if(visible){
			if(!this.generalAI.isKnockingBack){rbody.MovePosition(rbody.position + movementVector * speed * Time.deltaTime);}
		}
	}
	
	void Update () {
		
		//Animations
		if(movementVector == Vector2.zero){
			anim.SetBool("isWalking", false);
		}else{
			anim.SetBool("isWalking", true);
		}
		
		if(generalAI.edgeCollision != null){
			randomMove();
			triggerOnce = false;
			Invoke ("canChange", 0.1f);
		}

		if(shouldShoot && !triggerChance && Vector3.Distance(player.transform.position, transform.position) < 2f){
			float randChance = Random.Range(1f,1.8f);
			Invoke ("checkIfShoot", randChance);
			triggerChance = true;
		}

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
	
	void checkIfShoot() {
		movementVector = Vector2.zero;
		shouldShoot = false;
		triggerChance = false;
		lightObject.GetComponent<Light>().enabled = true;
		Invoke("doFire", 0.5f);
		Invoke ("resetShot", 1f);
		Invoke ("setAnimate", 0.5f);
	}
	
	void doFire(){
		anim.SetBool("isWalking", false);
		fireObj.fireTrace(player.transform.position, 1f);
		lightObject.GetComponent<Light>().enabled = false;
	}
	
	void setAnimate() {
		canAnimate = true;
	}
	
	void resetShot() {
		shouldShoot = true;
	}

}
