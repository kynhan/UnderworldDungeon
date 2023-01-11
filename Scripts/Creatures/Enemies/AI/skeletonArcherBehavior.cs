using UnityEngine;
using System.Collections;

public class skeletonArcherBehavior : MonoBehaviour {

	public float speed = 1.2f;
	public LayerMask terrainLayer;
	bool canAnimate = true;
	bool shouldShoot = true;
	bool doRandomMove = true;
	int setDirection;
	int timesCanShoot = 0;

	Animator anim;
	Rigidbody2D rbody;
	Vector2 movementVector;
	fireProjectile fireObj;
	generalEnemyAI generalAI;
	GameObject player;
	string chosenDirection = "null";
	public float currentPathDistance = 10f;
	bool visible = false;
	bool triggerOnce = true;

	void Start () {
		anim = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
		fireObj = GetComponent<fireProjectile>();
		generalAI = GetComponent<generalEnemyAI>();
		player = GameObject.Find("Player");
		Invoke ("randomMove", 1f);
	}

	void OnBecameVisible() {visible = true;}void OnBecameInvisible(){visible = false;}

	void randomMove() {
		anim.SetBool("isAttacking", false);
		anim.SetBool("isWalking", true);
		int randNumber = Random.Range(1,5);
		if(randNumber == 1){chosenDirection="upDir";}else if(randNumber == 2){chosenDirection="rightDir";}
		else if(randNumber == 3){chosenDirection="downDir";}else if(randNumber == 4){chosenDirection="leftDir";}
		if(chosenDirection=="upDir"){movementVector = new Vector2(0f, 1f);}
		else if(chosenDirection=="rightDir"){movementVector = new Vector2(1f, 0f);}
		else if(chosenDirection=="downDir"){movementVector = new Vector2(0f, -1f);}
		else if(chosenDirection=="leftDir"){movementVector = new Vector2(-1f, 0f);}
	}

	void FixedUpdate(){
		if(visible){
			if(!this.generalAI.isKnockingBack){rbody.MovePosition(rbody.position + movementVector * speed * Time.deltaTime);}
		}
		/*//Raycast Check
		if(chosenDirection == "upDir"){
			RaycastHit2D uphit = Physics2D.BoxCast(new Vector2(transform.position.x - 0.01f, transform.position.y - 0.09f), new Vector2(0.05f, 0.03f), 0f, Vector2.up, 5f, terrainLayer.value);
			if (uphit.collider != null) {currentPathDistance = Mathf.Abs(uphit.point.y - transform.position.y);}
		}else if(chosenDirection == "rightDir"){
			RaycastHit2D righthit = Physics2D.BoxCast(new Vector2(transform.position.x - 0.01f, transform.position.y - 0.09f), new Vector2(0.05f, 0.03f), 0f, Vector2.right, 5f, terrainLayer.value);
			if (righthit.collider != null) {currentPathDistance = Mathf.Abs(righthit.point.x - transform.position.x);}
		}else if(chosenDirection == "downDir"){
			RaycastHit2D downhit = Physics2D.BoxCast(new Vector2(transform.position.x - 0.01f, transform.position.y - 0.09f), new Vector2(0.05f, 0.03f), 0f, -Vector2.up, 5f, terrainLayer.value);
			if (downhit.collider != null) {currentPathDistance = Mathf.Abs(downhit.point.y - transform.position.y) - 0.23f;}
		}else if(chosenDirection == "leftDir"){
			RaycastHit2D lefthit = Physics2D.BoxCast(new Vector2(transform.position.x - 0.01f, transform.position.y - 0.09f), new Vector2(0.05f, 0.03f), 0f, -Vector2.right, 5f, terrainLayer.value);
			if (lefthit.collider != null) {currentPathDistance = Mathf.Abs(lefthit.point.x - transform.position.x);}
		}*/
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
		/*if(currentPathDistance<=0.1f && triggerOnce){
			if(doRandomMove){randomMove();}
			triggerOnce = false;
			Invoke ("canChange", 0.1f);
		}*/
		if(generalAI.edgeCollision != null && triggerOnce){
			if(doRandomMove){randomMove();}
			triggerOnce = false;
			Invoke ("canChange", 0.1f);
		}
		if(shouldShoot){checkPlayer();}
	}
	void canChange() {
		triggerOnce = true;
	}

	void checkPlayer() {
		//Up and Down
		if(player.transform.position.x >= transform.position.x - 0.4f && player.transform.position.x <= transform.position.x + 0.4f && Mathf.Abs(player.transform.position.y - transform.position.y) < 2f){
			doRandomMove = false;
			shouldShoot = false;
			movementVector = Vector2.zero;
			Invoke("doFire", 0.5f);
			canAnimate = false;
			if(player.transform.position.y >= transform.position.y){
				setDirection = 1;anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);
			}else{
				setDirection = 3;anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);
			}
			if(timesCanShoot<4){anim.SetBool("isAttacking", true);}
			Invoke ("setAnimate", 1f);
		}
		//Left and Right
		else if(player.transform.position.y >= transform.position.y - 0.4f && player.transform.position.y <= transform.position.y + 0.4f && Mathf.Abs(player.transform.position.x - transform.position.x) < 2f){
			doRandomMove = false;
			shouldShoot = false;
			movementVector = Vector2.zero;
			Invoke("doFire", 0.5f);
			canAnimate = false;
			if(player.transform.position.x >= transform.position.x){
				setDirection = 2;anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);
			}else{
				setDirection = 4;anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);
			}
			if(timesCanShoot<4){anim.SetBool("isAttacking", true);}
			Invoke ("setAnimate", 1f);
		}
	}

	void setAnimate() {
		canAnimate = true;
		doRandomMove = true;
		anim.SetBool("isAttacking", false);
	}

	void doFire(){
		if(timesCanShoot<2){
			fireObj.fire(setDirection, 100f);
			timesCanShoot++;
			Invoke ("doFire", 0.5f);
		}else{
			Invoke ("canShoot", 1f);
			Invoke ("resetShots", 1f);
			randomMove();
			doRandomMove = true;
		}
	}
	void canShoot(){shouldShoot = true;}
	void resetShots(){timesCanShoot=0;}

}
