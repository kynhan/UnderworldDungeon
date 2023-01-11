using UnityEngine;
using System.Collections;

public class snowSpiderBehavior : MonoBehaviour {

	public float speed = 0.6f;
	public GameObject iceSpike;
	
	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	generalEnemyAI generalAI;
	GameObject player;
	GameObject lightObject;
	
	int setDirection;
	bool canAnimate = true;
	bool triggerOnce = true;
	bool triggerChance;
	string chosenDirection = "null";
	bool visible = false;
	
	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
		
		foreach(Transform child in transform){
			if(child.name == "point_light"){
				lightObject = child.gameObject;
			}
		}
		
		InvokeRepeating("randomMove", 0f, 2f);
		createSpike();
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

		if(movementVector.x > 0f){anim.SetFloat("inputX", -1f);}
		else{anim.SetFloat("inputX", 1f);}

		if(generalAI.edgeCollision != null && triggerOnce){
			randomMove();
			triggerOnce = false;
			Invoke ("canChange", 0.1f);
		}

	}
	
	void canChange() {triggerOnce = true;}

	void randomMove() {
		int randNumber = Random.Range(1,9);
		if(randNumber == 1){movementVector = new Vector2(0f, 1f);}
		else if(randNumber == 2){movementVector = new Vector2(1f, 0f);}
		else if(randNumber == 3){movementVector = new Vector2(0f, -1f);}
		else if(randNumber == 4){movementVector = new Vector2(-1f, 0f);}

		
		else if(randNumber == 5){movementVector = new Vector2(1f, 1f);}
		else if(randNumber == 6){movementVector = new Vector2(1f, -1f);}
		else if(randNumber == 7){movementVector = new Vector2(-1f, 1f);}
		else if(randNumber == 8){movementVector = new Vector2(-1f, -1f);}
	}
	
	void setAnimate() {
		canAnimate = true;
	}

	float randomTime = 1f;
	void createSpike() {
		if(generalAI.edgeCollision == null){
			GameObject theSpike = Instantiate(iceSpike, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), Quaternion.identity) as GameObject;
			theSpike.GetComponent<spike>().deathTimer = 2.5f;
		}
		randomTime = Random.Range(0.5f, 1f);
		Invoke ("createSpike", randomTime);
	}
}
