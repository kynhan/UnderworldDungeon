using UnityEngine;
using System.Collections;

public class passiveBehavior : MonoBehaviour {

	public float speed = 0.5f;
	public float mainYOffset = 0f;
	float speedModifier;
	bool doRandomMove = true;

	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;

	void Start () {

		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		Invoke ("randomMove", 2f);

	}

	void randomMove() {
		if(doRandomMove){
			float randX;
			float randY;

			float randNum1 = Random.value;
			float randNum2 = Random.value;
			if(randNum1 < 0.5f){randX = -1f;}else{randX = 1f;}
			if(randNum2 < 0.5f){randY = -1f;}else{randY = 1f;}

			speedModifier = Random.Range(1f, 1.5f);
			movementVector = new Vector2(randX, randY);
			Invoke ("stopMoving", 1.5f);
			Invoke ("randomMove", 2f);
		}
	}

	void stopMoving() {
		movementVector = Vector2.zero;
	}

	void FixedUpdate(){
		rbody.MovePosition(rbody.position + movementVector * speed * speedModifier * Time.deltaTime);
	}

	void Update () {

		//Animations
		if(movementVector == Vector2.zero){
			anim.SetBool("isWalking", false);
		}else{
			anim.SetBool("isWalking", true);
			if(movementVector.x > 0){
				anim.SetFloat("inputX", 1f);
			}else{
				anim.SetFloat("inputX", -1f);
			}
		}

	}
}
