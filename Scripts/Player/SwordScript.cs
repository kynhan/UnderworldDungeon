using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

	//Defining variables
	public int swordDamage;
	public int damageMax = 10;
	public int durabilityLoss = 1;
	public int staminaCost = 100;
	public float hitBoxWidth;
	public float hitBoxHeight;
	public float attackDuration = 0.35f;
	public float attackDelay = 0.2f;
	public int aresBonus = 0;
	public Vector2 northOffset;
	public Vector2 eastOffset;
	public Vector2 southOffset;
	public Vector2 westOffset;

	private Inventory inv;
	Animator anim;
	SpriteRenderer myRenderer;
	BoxCollider2D boxColliderObject;
	public AudioClip swordSwing;
	bool canHit;
	bool triggerOnce;
	bool isBehindHead = false;
	
	void Start () {
		triggerOnce = true;
		canHit = true;
		anim = GetComponent<Animator> ();
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		aresBonus = 0;
		Player.itemStaminaCost = staminaCost;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "Pet"){
			collider.SendMessageUpwards("gotHit", SendMessageOptions.DontRequireReceiver);
		}
		if(collider.tag == "Chest" || collider.tag == "Enemy" && canHit){
			if(collider.transform.parent != null){

				int damageRand = Random.Range((swordDamage+aresBonus), (damageMax+aresBonus));
				int currentDamage = damageRand + Player.damage;

				if(collider.transform.parent.GetComponent<generalEnemyAI>() != null){
					Player.hitEnemy = true;
					Player.enemyThatGotHit = collider.transform.parent.gameObject;
					collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(currentDamage);

					if(GetComponent<EnchantedItem>().enchantEffect == "Ignited"){collider.transform.parent.GetComponent<generalEnemyAI>().burn(2f, 5);}
					if(GetComponent<EnchantedItem>().enchantEffect == "Magma"){collider.transform.parent.GetComponent<generalEnemyAI>().burn(2f, 7);}
				}else{
					collider.SendMessageUpwards("gotHit", SendMessageOptions.DontRequireReceiver);
				}
				canHit = false;
				Invoke ("setCanHit", 0.5f);
			}else{
				collider.SendMessage("gotHit", SendMessageOptions.DontRequireReceiver);
			}

		}
	}

	void setCanHit() {
		canHit = true;
	}
	
	void Update () {
		if(Player.direction == 1 || Player.direction == 4 || isBehindHead){
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 1;
		}else{
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 2;
		}
		Player.attackDuration = attackDuration;
		//Play Animations
		//if(Player.direction == 1){myRenderer.sortingOrder=1;}else{myRenderer.sortingOrder=4;}
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
		anim.SetBool ("isAttacking", Player.isAttacking);

		if(Player.isAttacking && Player.stamina >= staminaCost){
			if(triggerOnce){
				//inv.GetSelectedItemObj().GetComponent<ItemData>().itemDurability -= durabilityLoss;
				Player.stamina -= staminaCost;
				Invoke("SwordAttack", attackDelay);
				Invoke("DoneAttack", attackDuration);
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}
	}

	void SwordAttack() {
		AudioSource.PlayClipAtPoint(swordSwing, transform.position);
		if(Player.direction == 1){
			boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
			BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.isTrigger = true;
			boxColliderObject.size = new Vector3(hitBoxHeight,hitBoxWidth,0);
			collider.offset = northOffset;
		}else if(Player.direction == 2){
			boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
			BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.isTrigger = true;
			boxColliderObject.size = new Vector3(hitBoxWidth,hitBoxHeight,0);
			collider.offset = eastOffset;
		}else if(Player.direction == 3){
			boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
			BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.isTrigger = true;
			boxColliderObject.size = new Vector3(hitBoxHeight,hitBoxWidth,0);
			collider.offset = southOffset;
		}else if(Player.direction == 4){
			boxColliderObject = gameObject.AddComponent<BoxCollider2D>();
			BoxCollider2D collider = GetComponent<BoxCollider2D>() as BoxCollider2D;
			collider.isTrigger = true;
			boxColliderObject.size = new Vector3(hitBoxWidth,hitBoxHeight,0);
			collider.offset = westOffset;
		}
	}

	void DoneAttack() {
		Player.hitEnemy = false;
		Player.enemyThatGotHit = null;
		Destroy(boxColliderObject);
	}

	void behindHead(){isBehindHead = true;}
	void notBehindHead(){isBehindHead = false;}

}
