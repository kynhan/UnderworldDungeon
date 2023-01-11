using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fireProjectile : MonoBehaviour {
	
	public string projectileType;
	public float fireLoop;
	public bool hasAnim = false;
	public bool isTurret = false;
	public bool isEnemy = false;
	public int direction; 
	public float fireDuration = 1.5f;
	public bool loadExplosion = true;
	public bool lookAtTarget = false;
	public bool fadeIn = false;
	public float customX = 0f;
	public float customY = 0f;

	Vector3 targetPos;
	float fadeIntensity;
	float theValue = 0f;
	float theSpeed;
	GameObject player;

	GameObject projectileObj;
	Animator anim;

	void Start () {
		if(hasAnim){anim = GetComponent<Animator>();}
		if(isTurret){InvokeRepeating("fireLooper", fireLoop, fireLoop);}
		player = GameObject.Find("Player");
	}
	
	void fireLooper(){
		fire (direction, 50f);
	}

	public void fire(int fireDirection, float speed) {
		if(this.enabled && Vector3.Distance(player.transform.position, transform.position) < 4f){
			direction = fireDirection;
			GameObject instance;
			instance = Resources.Load("Projectiles/"+projectileType) as GameObject;
			projectileObj = Instantiate(instance, new Vector3(transform.position.x + customX, transform.position.y + customY - 0.1f, 0), Quaternion.identity) as GameObject;
			projectileObj.transform.parent = transform;
			projectileObj.GetComponent<enemyProjectile>().deathTime = fireDuration;
			projectileObj.GetComponent<enemyProjectile>().loadExplosion = loadExplosion;

			projectileObj.GetComponent<enemyProjectile>().direction = direction;
			if(direction == 1){
				projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
			}else if(direction == 2){
				projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
			}else if(direction == 3){
				projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -(speed)));
			}else if(direction == 4){
				projectileObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(speed), 0));
			}
			if(isEnemy){projectileObj.GetComponent<enemyProjectile>().direction = direction;}
			if(hasAnim){}
		}
	}

	public void fireTrace(Vector3 theTarget, float speed) {
		if(this.enabled && Vector3.Distance(player.transform.position, transform.position) < 4f){
			GameObject instance;
			instance = Resources.Load("Projectiles/"+projectileType) as GameObject;
			projectileObj = Instantiate(instance, new Vector3(transform.position.x + customX, transform.position.y + customY - 0.1f, 0), Quaternion.identity) as GameObject;
			projectileObj.transform.parent = transform;
			projectileObj.GetComponent<enemyProjectile>().deathTime = fireDuration;
			projectileObj.GetComponent<enemyProjectile>().loadExplosion = loadExplosion;

			if(fadeIn){theValue=0f;fadeIntensity = projectileObj.GetComponent<SpriteRenderer>().color.a;
				projectileObj.GetComponent<SpriteRenderer>().color = new Color(projectileObj.GetComponent<SpriteRenderer>().color.r, projectileObj.GetComponent<SpriteRenderer>().color.g, projectileObj.GetComponent<SpriteRenderer>().color.b, 0f);}

			targetPos = theTarget;
			targetPos = targetPos - new Vector3(transform.position.x + customX, transform.position.y + customY - 0.1f, 0);
			targetPos = new Vector3(transform.position.x + customX, transform.position.y + customY - 0.1f, 0) + targetPos.normalized * 100f;

			if(lookAtTarget){
				Vector3 distance = targetPos - projectileObj.transform.position;
				projectileObj.transform.LookAt(projectileObj.transform.position + new Vector3(0,0,1), distance);
			}
			theSpeed = speed;
			projectileObj.GetComponent<Rigidbody2D>().AddForce(targetPos * speed);
		}
	}

	void Update () {
	
		if(fadeIn && projectileObj!=null){
			if(projectileObj.GetComponent<SpriteRenderer>().color.a <= fadeIntensity){
				theValue += 0.01f;
				projectileObj.GetComponent<SpriteRenderer>().color = new Color(projectileObj.GetComponent<SpriteRenderer>().color.r, projectileObj.GetComponent<SpriteRenderer>().color.g, projectileObj.GetComponent<SpriteRenderer>().color.b, theValue);
			}
		}

	}
}
