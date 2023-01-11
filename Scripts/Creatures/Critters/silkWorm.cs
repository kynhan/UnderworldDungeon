using UnityEngine;
using System.Collections;

public class silkWorm : MonoBehaviour {

	Animator anim;
	Vector2 movementVector;
	float rotation;
	Rigidbody2D rbody;
	float speed = 0.1f;
	bool canMove = true;
	float direction = 1f;
	public bool tutorialMode = false;

	void Start () {
		float randScale = Random.Range(1f,2f);
		if(tutorialMode){
			randScale = Random.Range(0.1f, 1f);
		}
		transform.localScale = new Vector3(randScale, randScale, 1);
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		rotation = (float)Random.Range(5,25);
		transform.eulerAngles = new Vector3(0,0,rotation);
		movementVector = new Vector2(1f,0);
		float random = Random.value;
		if(random < 0.5f){direction = 1f;}
		else{direction = -1f;}
		InvokeRepeating("stop", 3f, 3f);

	}

	void stop() {
		anim.SetBool("isCrawling", false);
		canMove = false;
		rotation = (float)Random.Range(5,25);
		float random = Random.value;
		if(random < 0.5f){direction = 1f;}
		else{direction = -1f;} 
		Invoke ("move", 1f);
	}

	void move() {
		anim.SetBool("isCrawling", true);
		canMove = true;
	}

	void Update () {

		if(transform.eulerAngles.z > rotation){
			transform.eulerAngles-=new Vector3(0, 0, 0.1f);
		}else{
			transform.eulerAngles+=new Vector3(0, 0, 0.1f);
		}
		if(canMove){
			if(direction == 1f){
				transform.position +=  transform.right * Time.deltaTime * speed;
			}else{
				transform.position -=  transform.right * Time.deltaTime * speed;
			}
		}
	}
}
