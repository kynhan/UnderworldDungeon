using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class playerProjectile : MonoBehaviour {
	
	public string projectileEffect = "null";
	public float effectTime = 2f;
	public bool piercing;
	public int damageMin = 1;
	public int damageMax = 10;
	public bool spin;
	public bool hasDirections;
	public int direction;
	public bool boomerang = false;
	public bool addPlayerDmg = false;
	public string enchantEffect = "Mundane";
	public bool loadExplosion = false;
	bool onReturn;
	int burnDamage = 5;

	Animator anim;
	List<GameObject> hitEnemies = new List<GameObject>();
	GameObject player;

	void Start () {
	
		if(hasDirections){
			anim = GetComponent<Animator>();
		}
		if(boomerang){
			player = GameObject.Find("Player");
			Invoke ("returnBack", 1f);
		}

		if(enchantEffect == "Ignited"){
			projectileEffect = "burn"; effectTime = 2f; burnDamage = 5;
		}else if(enchantEffect == "Magma"){
			projectileEffect = "burn"; effectTime = 2f; burnDamage = 7;
		}

	}

	void Update () {
	
		if(spin){
			transform.Rotate(0,0,200*Time.deltaTime);
		}
		if(hasDirections){
			if(direction == 1){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", 1f);}
			else if(direction == 2){anim.SetFloat("inputX", 1f);anim.SetFloat("inputY", 0f);}
			else if(direction == 3){anim.SetFloat("inputX", 0f);anim.SetFloat("inputY", -1f);}
			else if(direction == 4){anim.SetFloat("inputX", -1f);anim.SetFloat("inputY", 0f);}
			if(!GetComponent<SpriteRenderer>().enabled){
				GetComponent<SpriteRenderer>().enabled = true;
			}
		}
		if(boomerang){
			if(GetComponent<Rigidbody2D>().velocity.y > 0f || GetComponent<Rigidbody2D>().velocity.y < 0f){
				transform.position = new Vector2(player.transform.position.x, transform.position.y);
			}else if(GetComponent<Rigidbody2D>().velocity.x > 0f || GetComponent<Rigidbody2D>().velocity.x < 0f){
				transform.position = new Vector2(transform.position.x, player.transform.position.y);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if(collider.tag == "Enemy" || collider.tag == "Pet" || collider.tag == "Chest"){
			if(collider.transform.parent.GetComponent<generalEnemyAI>() != null){

				int damageRand = Random.Range(damageMin, damageMax);
				int currentDamage = damageRand;
				if(addPlayerDmg){currentDamage += Player.damage;}

				if(piercing){
					bool alreadyHit = false;
					for (int i = 0; i < this.hitEnemies.Count; i++) {
						if(this.hitEnemies[i] == collider.gameObject){
							alreadyHit = true;
						}
					}
					if(!alreadyHit){
						this.hitEnemies.Add(collider.gameObject);
						if(loadExplosion){
							GameObject instance = Resources.Load("Projectiles/arrowExplosion") as GameObject;
							Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
						}
						collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(currentDamage);
						if(projectileEffect == "burn"){
							collider.transform.parent.GetComponent<generalEnemyAI>().burn(effectTime, burnDamage);
						}
						if(projectileEffect == "freeze"){
							collider.transform.parent.GetComponent<generalEnemyAI>().freeze(effectTime);
						}
					}
				}else{
					collider.transform.parent.GetComponent<generalEnemyAI>().gotHit(currentDamage);
					if(projectileEffect == "burn"){
						collider.transform.parent.GetComponent<generalEnemyAI>().burn(effectTime, burnDamage);
					}
					if(projectileEffect == "freeze"){
						collider.transform.parent.GetComponent<generalEnemyAI>().freeze(effectTime);
					}
				}
			}else{
				collider.SendMessageUpwards("gotHit", SendMessageOptions.DontRequireReceiver);
			}

			if(!piercing){
				if(loadExplosion){
					GameObject instance = Resources.Load("Projectiles/arrowExplosion") as GameObject;
					Instantiate(instance, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
				}
				Destroy(gameObject);
			}
		}

		if(collider.tag == "Player" && onReturn){
			if(gameObject.name.Contains("player_hadesBidentProj")){
				GameObject.Find("Inventory").GetComponent<Inventory>().AddHotbarItem(56, enchantEffect, "sword", 0);
			}
			Destroy(gameObject);
		}

	}

	void returnBack() {
		if(GetComponent<Rigidbody2D>().velocity.y > 0f){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -300));
		}else if(GetComponent<Rigidbody2D>().velocity.x > 0f){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 0));
		}else if(GetComponent<Rigidbody2D>().velocity.y < 0f){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
		}else if(GetComponent<Rigidbody2D>().velocity.x < 0f){
			GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));
		}
		hitEnemies.Clear();
		onReturn = true;
	}

}
