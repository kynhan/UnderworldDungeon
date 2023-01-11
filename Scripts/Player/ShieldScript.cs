using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	Animator anim;
	SpriteRenderer myRenderer;
	BoxCollider2D boxColliderObject;
	GameObject playerTransform;

	public AudioClip block;
	public Vector2 horizontalSize;
	public Vector2 verticalSize;

	public Vector2 northOffset;
	public Vector2 eastOffset;
	public Vector2 southOffset;
	public Vector2 westOffset;

	Vector2 currentOffset;

	//Attributes
	public float damageReduction = 0.5f;
	public float knocbackStrength = 0.8f;
	public bool freezeEnemy = false;
	public bool isShell = false;

	bool canHit = true;

	void Start () {
		playerTransform = transform.parent.transform.parent.transform.parent.gameObject;
		anim = GetComponent<Animator> ();
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		
	}

	void OnTriggerStay2D(Collider2D collider) {
		if(canHit && !isShell){
			// && Mathf.Abs((playerTransform.transform.position.x+currentOffset.x) - collider.transform.position.x) < Mathf.Abs(playerTransform.transform.position.x - collider.transform.position.x) && Mathf.Abs((playerTransform.transform.position.y+currentOffset.y) - collider.transform.position.y) < Mathf.Abs(playerTransform.transform.position.y - collider.transform.position.y)
			if(collider.name == "attackBox" && Vector2.Distance(new Vector2(playerTransform.transform.position.x+currentOffset.x, playerTransform.transform.position.y+currentOffset.y), collider.transform.position) < Vector2.Distance(playerTransform.transform.position, collider.transform.position)){
				Player.blockSuccessful = true;

				if(freezeEnemy){
					collider.transform.parent.gameObject.GetComponent<generalEnemyAI>().freeze(2.5f);
				}

				AudioSource.PlayClipAtPoint(block, transform.position);
				playerTransform.GetComponent<Player>().takeDamage((int)Mathf.Round((float)(collider.transform.parent.GetComponent<generalEnemyAI>().damage)*(1f - damageReduction)));
				playerTransform.GetComponent<Player>().knockback(collider.transform.parent.gameObject.GetComponent<generalEnemyAI>().direction, knocbackStrength);
				collider.SendMessageUpwards("shieldCollide");
				canHit = false;
				Invoke ("reactivateHit", 0.5f);
				Player.stamina -= 100;
			}
			if(collider.tag == "enemyProjectile"){
				Player.blockSuccessful = true;
				AudioSource.PlayClipAtPoint(block, transform.position);
				int randomDir = Random.Range(1, 4);
				playerTransform.GetComponent<Player>().takeDamage((int)Mathf.Round((float)(collider.transform.GetComponent<generalProjectile>().projectileDamage)*(1f - damageReduction)));
				playerTransform.GetComponent<Player>().knockback(randomDir, knocbackStrength);
				canHit = false;
				Invoke ("reactivateHit", 0.5f);
				Player.stamina -= 100;
			}
		}
	}

	void reactivateHit() {
		canHit = true;
		Player.blockSuccessful = false;
	}
	
	void Update () {
		//Current Offset
		if(Player.isBlocking){
			if(Player.direction == 1){currentOffset = new Vector2(northOffset.x, northOffset.y + 0.1f);}else if(Player.direction == 2){currentOffset = eastOffset;}
			else if(Player.direction == 3){currentOffset = new Vector2(southOffset.x, southOffset.y - 0.1f);}else if(Player.direction == 4){currentOffset = westOffset;}
		}else{
			currentOffset = new Vector2(0f, 0f);
		}

		//Sorting Order
		if(Player.isBlocking){
			if(isShell){Player.isImmobilized = true;Player.isInvulnerable = true;Player.stamina-=3;}
			if(Player.direction == 1 && !isShell){
				myRenderer.sortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder - 3;
			}else{
				myRenderer.sortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder + 2;
			}
		}else{
			if(isShell){Player.isImmobilized = false;Player.isInvulnerable = false;}
			if(Player.direction == 1){
				myRenderer.sortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder + 2;
			}else{
				myRenderer.sortingOrder = playerTransform.GetComponent<SpriteRenderer>().sortingOrder - 2;
			}
		}
		//Play Animations
		if(!Player.isAttacking){

			if(Player.direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			else if(Player.direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);}
			else if(Player.direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
			else if(Player.direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);};

			if (Player.movement_vector != Vector2.zero) {
				anim.SetBool ("isWalking", true);

			} else {
				anim.SetBool ("isWalking", false);
			}
		}
		//anim.SetBool ("isAttacking", Player.isAttacking);
		anim.SetBool ("isBlocking", Player.isBlocking);

		//Offset
		if(boxColliderObject){
			if(Player.direction == 1){
				boxColliderObject.size = verticalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = northOffset;
			}else if(Player.direction == 2){
				boxColliderObject.size = horizontalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = eastOffset;
			}else if(Player.direction == 3){
				boxColliderObject.size = verticalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = southOffset;
			}else if(Player.direction == 4){
				boxColliderObject.size = horizontalSize;
				BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
				collider.offset = westOffset;
			}
		}

		//Right click to block
		if(Input.GetMouseButton(1)){
			if(Player.stamina > 100){
				if(!Player.isBlocking && ((isShell && Player.stamina >= Player.maxStamina/2) || !isShell)){
					Block ();
					Player.currentSpeed = Player.baseSpeed/1.3f;
					Player.isBlocking = true;
				}
			}
		}else{if(Player.isBlocking){DoneBlock();Player.isBlocking = false;Player.currentSpeed = Player.baseSpeed;}}
		if(Player.stamina < 100){DoneBlock();Player.isBlocking = false;Player.currentSpeed = Player.baseSpeed;}
	}

	void Block() {
		boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
		BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
		collider.isTrigger = true;
	}

	void DoneBlock() {
		Destroy(boxColliderObject);
	}
}
