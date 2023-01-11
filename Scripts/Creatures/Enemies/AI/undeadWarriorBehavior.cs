using UnityEngine;
using System.Collections;

public class undeadWarriorBehavior : MonoBehaviour {

	generalEnemyAI generalScript;
	public LayerMask terrainLayer;
	
	Vector2 movementVector;
	Vector2 targetPos;
	Vector2 playerPos;
	GameObject player;
	GameObject attackObject;
	Animator anim;
	Rigidbody2D rbody;
	generalEnemyAI generalAI;
	BoxCollider2D damageCollider;
	BoxCollider2D boxColliderObject;
	RaycastHit2D hit;

	bool canCharge;
	bool triggerNear;
	bool triggerMove;
	bool triggerAttack;
	bool pathBlocked;
	
	//Attributes
	
	
	void Start () {
		pathBlocked = true;
		player = GameObject.Find("Player");
		anim = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody2D> ();
		generalAI = GetComponent<generalEnemyAI>();
		generalScript = GetComponent<generalEnemyAI>();
		triggerNear = true;
		triggerMove = false;
		triggerAttack = true;

		foreach(Transform child in transform){
			if(child.name == "damageBox"){
				damageCollider = child.GetComponent<BoxCollider2D>();
			}
			if(child.name == "attackBox"){
				attackObject = child.gameObject;
			}
		}
	}

	void FixedUpdate() {
		rbody.velocity = Vector2.zero;

		if(Vector2.Distance(player.transform.position, transform.position) < 5f){
			this.hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - 0.28f), new Vector2(0.1f, 0.1f), 0f, new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y), Vector2.Distance(playerPos, transform.position) + 0.1f, terrainLayer.value);

			if(this.hit.collider != null){ 
				if (this.hit.collider.tag == "TerrainObject" || this.hit.collider.name == "RoomEdge"){
					this.pathBlocked = true;
				}else{
					this.pathBlocked = false;
				}
			}else{
				this.pathBlocked = false;
			}

		}
		if(this.canCharge){
			transform.position = Vector2.MoveTowards(transform.position, targetPos, 3f*Time.deltaTime);
			damageCollider.isTrigger = true;
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.2f);
		}
	}
	
	void Update () {
		//Check if player is near
		if(Vector2.Distance(player.transform.position, transform.position) < 2f && triggerNear && !pathBlocked){
			anim.SetBool("isJumping", true);
			this.triggerMove = true;
			this.triggerNear = false;
		}

		//Up Down Animations
		if(transform.position.y - 0.2f < player.transform.position.y && anim.GetBool("isJumping")){
			anim.SetBool("UpTrueDownFalse", true);
		}else{
			anim.SetBool("UpTrueDownFalse", false);
		}

		//Charge
		if(triggerMove){
			targetPos = playerPos;
			this.canCharge = true;
			this.triggerMove = false;
		}

		//At Target
		if((Vector2)transform.position == targetPos && triggerAttack){
			this.triggerAttack = false;
			this.canCharge = false;
			anim.SetBool("isJumping", false);
			damageCollider.isTrigger = true;
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			if(transform.position.y < player.transform.position.y){
				Invoke ("up_swingAttack", 0.2f);
			}else{
				Invoke ("down_swingAttack", 0.2f);
			}
		}

		//Player Positions
		if(transform.position.y - 0.28f < player.transform.position.y - 0.2f){
			playerPos = new Vector2(player.transform.position.x, player.transform.position.y - 0.2f);
		}else{
			playerPos = new Vector2(player.transform.position.x, player.transform.position.y + 0.6f);
		}

	}
	
	//Swing

		void down_swingAttack() {
			anim.Play("down-attackDown");
			Invoke ("down_attackBox_down", 0.1f);
			Invoke ("down_swingLeft", 1f);
		}

		void down_swingLeft() {
			anim.Play("down-attackLeft");
			Invoke ("down_attackBox_left", 0.1f);
			Invoke ("down_swingRight", 1f);
		}

		void down_swingRight() {
			anim.Play("down-attackRight");
			Invoke ("down_attackBox_right", 0.1f);
			Invoke ("rest", 0.8f);
		}

		void up_swingAttack() {
			anim.Play("up-attackDown");
			Invoke ("up_attackBox_down", 0.1f);
			Invoke ("up_swingRight", 1f);
		}
		
		void up_swingRight() {
			anim.Play("up-attackRight");
			Invoke ("up_attackBox_right", 0.1f);
			Invoke ("up_swingLeft", 1f);
		}
		
		void up_swingLeft() {
			anim.Play("up-attackLeft");
			Invoke ("up_attackBox_left", 0.1f);
			Invoke ("rest", 0.8f);
		}


	//Swing Attack Boxes

		void down_attackBox_down(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.06f, 0.18f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(-0.03f, -0.2f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}
		
		void down_attackBox_left(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.19f, 0.2f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(-0.16f, -0.13f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}

		void down_attackBox_right(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.45f, 0.12f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(0.02f, -0.15f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}

		void up_attackBox_down(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.13f, 0.1f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(0f, 0.06f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}
		
		void up_attackBox_right(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.32f, 0.1f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(0.13f, 0.06f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}
		
		void up_attackBox_left(){
			boxColliderObject = attackObject.AddComponent<BoxCollider2D>();
			boxColliderObject.size = new Vector2(0.6f, 0.1f);
			BoxCollider2D collider = attackObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.offset = new Vector2(0f, 0.06f);
			collider.isTrigger = true;
			Invoke ("removeAttackBox", 0.15f);
		}

	
	void removeAttackBox() {
		if(boxColliderObject != null){Destroy (boxColliderObject);}
	}

	void rest() {
		anim.Play("down-idle");
		Invoke ("reset", 0.5f);
	}

	void reset() {
		this.canCharge = false;
		this.triggerNear = true;
		this.triggerMove = false;
		this.triggerAttack = true;
		if(transform.position.y < player.transform.position.y){
			targetPos = new Vector2(player.transform.position.x, player.transform.position.y - 0.3f);
		}else{
			targetPos = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
		}
	}
}
