using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour {

	public string projectileType;
	public int staminaCost = 100;
	public float attackDuration = 0.2f;
	public float speed = 150f;
	private Inventory inv;
	public int damageMin = 7;
	public int damageMax = 10;
	public bool boomerang = false;
	public bool hasAnims = false;
	
	Animator anim;
	SpriteRenderer myRenderer;
	bool triggerOnce;
	GameObject projectileObj;

	void Start () {
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		inv = GameObject.Find ("Inventory").GetComponent<Inventory>();
		anim = GetComponent<Animator> ();
		Player.itemStaminaCost = staminaCost;
	}


	void Update () {
		if(Player.direction == 1 || Player.direction == 4){
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 1;
		}else{
			myRenderer.sortingOrder = transform.parent.transform.parent.transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 2;
		}
		Player.attackDuration = attackDuration;
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
				Invoke ("throwSpear", 0.3f);
				triggerOnce = false;
			}
		}else{
			triggerOnce = true;
		}
	}

	void throwSpear() {

		GameObject instance;
		instance = Resources.Load("Projectiles/"+projectileType) as GameObject;
		projectileObj = Instantiate(instance, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		projectileObj.GetComponent<playerProjectile>().damageMin = damageMin;
		projectileObj.GetComponent<playerProjectile>().damageMax = damageMax;
		projectileObj.GetComponent<playerProjectile>().enchantEffect = GetComponent<EnchantedItem>().enchantEffect;
		if(boomerang){projectileObj.GetComponent<playerProjectile>().enchantEffect = GetComponent<EnchantedItem>().enchantEffect;}
		else{projectileObj.GetComponent<dropSpear>().enchantEffect = GetComponent<EnchantedItem>().enchantEffect;}

		Vector3 theRotation = transform.rotation.eulerAngles;
		if(Player.direction == 1){
			if(hasAnims){projectileObj.GetComponent<Animator>().Play("right");}
			theRotation.z = -270f;
			projectileObj.transform.rotation = Quaternion.Euler(theRotation);
			projectileObj.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
		}else if(Player.direction == 2){
			if(hasAnims){projectileObj.GetComponent<Animator>().Play("right");}
			projectileObj.transform.rotation = Quaternion.Euler(theRotation);
			projectileObj.transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
		}else if(Player.direction == 3){
			if(hasAnims){projectileObj.GetComponent<Animator>().Play("right");}
			theRotation.z = -90f;
			projectileObj.transform.rotation = Quaternion.Euler(theRotation);
			projectileObj.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -(speed)));
		}else if(Player.direction == 4){
			if(hasAnims){projectileObj.GetComponent<Animator>().Play("left");}
			else{theRotation.z = -180f;}
			projectileObj.transform.rotation = Quaternion.Euler(theRotation);
			projectileObj.transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
			projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(speed), 0));
		}
		if(boomerang){projectileObj.transform.parent = transform.parent.transform.parent.transform.parent;}

		Player.stamina -= staminaCost;

		inv.RemoveSelectedItem();
		Destroy(inv.GetSelectedItemObj().transform.GetChild(0).gameObject);
	}

}
